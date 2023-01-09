using DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class OrderItem : IOrderItem
{
    private readonly string pathToOrderItems = @"..\xml\OrderItem.xml", configPath = @"..\xml\config.xml";
    public int Add(DO.OrderItem entity)
    {
        entity.Id = GetNextID();
        var orderItems = ReadFromXml();
        orderItems.Add(entity);
        WriteToXml(orderItems);
        return entity.Id;
    }

    public void Delete(int ID)
    {
        var orderItems = ReadFromXml();
        orderItems.RemoveAll(x => x.Id == ID);
        WriteToXml(orderItems);
    }

    public void Update(DO.OrderItem o)
    {
        Delete(o.Id);
        Add(o);
    }
    public DO.OrderItem Get(int ID)
    {
       return GetByCondition(x => x.Id == ID);
    }

    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        var orderItems = ReadFromXml();
        if (func == null)
        {
            return orderItems;
        }
        return from i in orderItems where func(i) select i;
    }

    public DO.OrderItem GetByCondition(Func<DO.OrderItem, bool> func)
    {
        return GetAll(func).Any() ? GetAll(func).First() : throw new DO.DalItemNotFoundException();
    }

    public IEnumerable<DO.OrderItem> GetOrderItemsInSpecificOrder(int orderId)
    {
        return GetAll(x => x.OrderID == orderId);
    }

    private void WriteToXml(List<DO.OrderItem> list)
    {
        var xmlS = new XmlSerializer(typeof(List<DO.OrderItem>));
        using (var writer = new StreamWriter(pathToOrderItems))
        {
            xmlS.Serialize(writer, list);
        }
    }

    private List<DO.OrderItem> ReadFromXml()
    {
        List<DO.OrderItem> orderItems = new List<DO.OrderItem>();
        var xmlS = new XmlSerializer(typeof(List<DO.OrderItem>));
        using (var reader = new StreamReader(pathToOrderItems))
        {
            try
            {
                orderItems = (List<DO.OrderItem>)xmlS.Deserialize(reader)!;
            }
            catch(InvalidOperationException)
            {
                return orderItems;
            }
        }
        return orderItems;
    }

    private int GetNextID()
    {
        var config = XElement.Load(configPath);
        var id = Convert.ToInt32(config.Element("orders-id")!.Value);
        id++;
        config.Element("orders-id")!.Value = id.ToString();
        config.Save(configPath);
        return --id;
    }
}
