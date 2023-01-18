using DalApi;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class OrderItem : IOrderItem
{
    private readonly string pathToOrderItems = @"..\xml\OrderItem.xml", configPath = @"..\xml\config.xml";

    /// <summary>
    /// Add new order item to the xml file
    /// </summary>
    /// <param name="entity">The order item to add</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem entity)
    {
        entity.Id = GetNextID();//get the next id
        var orderItems = ReadFromXml();//read all the order items from xml
        orderItems.Add(entity);//add the new order to the list
        WriteToXml(orderItems);//write back to xml
        return entity.Id;
    }

    /// <summary>
    /// Delete order item from xml
    /// </summary>
    /// <param name="ID">the order item id to delete</param>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int ID)
    {
        var orderItems = ReadFromXml();//read all the order items from xml
        orderItems.RemoveAll(x => x.Id == ID);//remove the order item
        WriteToXml(orderItems);//write back to xml
    }
    /// <summary>
    /// update order item in the xml file
    /// </summary>
    /// <param name="o">order item to update</param>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem o)
    {
        Delete(o.Id);
        Add(o);
    }

    /// <summary>
    /// Get the order item
    /// </summary>
    /// <param name="ID">using id to get it</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Get(int ID)
    {
       return GetByCondition(x => x.Id == ID);
    }

    /// <summary>
    /// Get the all order items 
    /// </summary>
    /// <param name="func">filter order items using func</param>
    /// <returns></returns>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        var orderItems = ReadFromXml();//read from xml all the order items
        if (func == null)//return all the order items
        {
            return orderItems;
        }
        return from i in orderItems where func(i) select i;//return filtered order items
    }
    /// <summary>
    /// Get order item by condition
    /// </summary>
    /// <param name="func">func to filter</param>
    /// <returns></returns>
    /// <exception cref="DO.DalItemNotFoundException">throw exception if the order item not found</exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem GetByCondition(Func<DO.OrderItem, bool> func)
    {
        return GetAll(func).Any() ? GetAll(func).First() : throw new DO.DalItemNotFoundException();
    }
    /// <summary>
    /// Get order id and return the order items in specific order
    /// </summary>
    /// <param name="orderId">the order id to return</param>
    /// <returns></returns>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem> GetOrderItemsInSpecificOrder(int orderId)
    {
        return GetAll(x => x.OrderID == orderId);
    }

    /// <summary>
    /// the function write to xml
    /// </summary>
    /// <param name="list">Get the list of order item to write</param>
    private void WriteToXml(List<DO.OrderItem> list)
    {
        ///using serializer to write
        var xmlS = new XmlSerializer(typeof(List<DO.OrderItem>));
        using (var writer = new StreamWriter(pathToOrderItems))
        {
            xmlS.Serialize(writer, list);
        }
    }
    /// <summary>
    /// The function read the all order items from xml
    /// </summary>
    /// <returns></returns>
    private List<DO.OrderItem> ReadFromXml()
    {
        List<DO.OrderItem> orderItems = new List<DO.OrderItem>();
        ///using serializer to read
        var xmlS = new XmlSerializer(typeof(List<DO.OrderItem>));
        using (var reader = new StreamReader(pathToOrderItems))
        {
            try
            {
                orderItems = (List<DO.OrderItem>)xmlS.Deserialize(reader)!;
            }
            catch(InvalidOperationException)//if we trying to read empty file
            {
                return orderItems;
            }
        }
        return orderItems;
    }

    /// <summary>
    /// Get the next id for order item
    /// </summary>
    /// <returns></returns>
    private int GetNextID()
    {
        var config = XElement.Load(configPath);//get the next id
        var id = Convert.ToInt32(config.Element("order-items-id")!.Value);//write back to the file the id + 1
        id++;
        config.Element("order-items-id")!.Value = id.ToString();//write back to the file the id + 1
        config.Save(configPath);
        return --id;//return the id
    }
}
