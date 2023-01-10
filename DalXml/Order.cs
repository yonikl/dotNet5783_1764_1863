using DalApi;
using System.Dynamic;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{
    private XElement ordersRoot, config;
    private readonly string pathToOrders = @"..\xml\Order.xml", configPath = @"..\xml\config.xml";

    /// <summary>
    /// The constructor Load the date from the file
    /// </summary>
    public Order()
    {
        if (!File.Exists(pathToOrders))
            CreateFiles();
        else
            LoadData();

    }

    /// <summary>
    /// the root alement poing to the root of the file 
    /// </summary>
    private void CreateFiles()
    {
        ordersRoot = new XElement("Orders");
        ordersRoot.Save(pathToOrders);
    }

    /// <summary>
    /// Add new order to the file
    /// </summary>
    /// <param name="entity">new order to add</param>
    /// <returns></returns>
    public int Add(DO.Order entity)
    {
        LoadData();
        if (entity.Id == 0)//if we wont to add new order to  file get the next id for the order
        {
            entity.OrderDate = DateTime.Now;
            entity.Id = GetNextID();
        }
        ordersRoot.Add(new XElement("order", new XElement("id", entity.Id), new XElement("customer-name", entity.CustomerName), new XElement("customer-address", entity.CustomerAddress), new XElement("customer-email", entity.CustomerEmail), new XElement("order-date", entity.OrderDate), new XElement("ship-date", entity.ShipDate), new XElement("delivery-date", entity.DeliveryDate)));//adding the new order
        ordersRoot.Save(pathToOrders);//save the file
        return entity.Id;//return the order id
    }

    /// <summary>
    /// Delete order from the xml file
    /// </summary>
    /// <param name="ID">product id to delete</param>
    public void Delete(int ID)
    {
        XElement orderElement = GetXElement(ID);//get the element
        orderElement.Remove();//delete from the file
        ordersRoot.Save(pathToOrders);//save the file

    }
    /// <summary>
    /// Get the order by id
    /// </summary>
    /// <param name="ID">id to get</param>
    /// <returns></returns>

    public DO.Order Get(int ID)
    {
        LoadData();
        var order = GetXElement(ID);//get the order
        return XElementToDoOrder(order);//convert from string to DO order
    }


    /// <summary>
    /// Get the element order from the file 
    /// </summary>
    /// <param name="id">using id to search in the file</param>
    /// <returns></returns>
    /// <exception cref="DO.DalItemNotFoundException">throw exception if the order not fount</exception>
    private XElement GetXElement(int id)
    {
        return (from i in ordersRoot.Elements() where Convert.ToInt32(i.Element("id")!.Value) == id select i).FirstOrDefault() ?? throw new DO.DalItemNotFoundException();
    }

    /// <summary>
    /// Return all the orders from the file
    /// </summary>
    /// <param name="func">get func to filter the orders</param>
    /// <returns></returns>
    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        LoadData();
        if (func == null)//if func equal to return all the the orders
        {
            return from i in ordersRoot.Elements()
                   select XElementToDoOrder(i);
        }
        //return the filtered orders
        return from i in ordersRoot.Elements()
               let j = XElementToDoOrder(i)
               where func(j)
               select j;
    }

    /// <summary>
    /// Return order by condition
    /// </summary>
    /// <param name="func">Get func to filter</param>
    /// <returns></returns>
    /// <exception cref="DO.DalItemNotFoundException">throw exception if it didnt find the order</exception>
    public DO.Order GetByCondition(Func<DO.Order, bool> func)
    {
        LoadData();
        //check if the order exist in the list if it dose return it if not throw exception
        return (from i in ordersRoot.Elements()
                select XElementToDoOrder(i)).Any() ? (from i in ordersRoot.Elements()
                                                      select XElementToDoOrder(i)).First() : throw new DO.DalItemNotFoundException();

    }
    /// <summary>
    /// update order in the file
    /// </summary>
    /// <param name="o">order to update</param>
    public void Update(DO.Order o)
    {
        LoadData();
        Delete(o.Id);
        Add(o);
    }

    /// <summary>
    /// The function load date from the xml file 
    /// </summary>
    /// <exception cref="NullReferenceException"></exception>
    private void LoadData()
    {
        try
        {
            ordersRoot = XElement.Load(pathToOrders);
            config = XElement.Load(configPath);
        }
        catch
        {
            throw new NullReferenceException();
        }
    }

    /// <summary>
    /// The function Get the next id for the order
    /// </summary>
    /// <returns></returns>
    private int GetNextID()
    {
        LoadData();
        var id = Convert.ToInt32(config.Element("orders-id")!.Value);//get the next id
        id++;
        config.Element("orders-id")!.Value = id.ToString();//write back to the file the id + 1
        config.Save(configPath);//save the file
        return --id;//return the id
    }

    /// <summary>
    /// Comvart from xelement to Do order
    /// </summary>
    /// <param name="i">the xelement to convert</param>
    /// <returns></returns>
    private DO.Order XElementToDoOrder(XElement i)
    {
        return new DO.Order()
        {
            Id = Convert.ToInt32(i.Element("id")!.Value),
            CustomerName = i.Element("customer-name")!.Value,
            CustomerAddress = i.Element("customer-address")!.Value,
            CustomerEmail = i.Element("customer-email")!.Value,
            OrderDate = i.Element("order-date")!.IsEmpty ? null : DateTime.Parse(i.Element("order-date")!.Value),
            ShipDate = i.Element("ship-date")!.IsEmpty ? null : DateTime.Parse(i.Element("ship-date")!.Value),
            DeliveryDate = i.Element("delivery-date")!.IsEmpty ? null : DateTime.Parse(i.Element("delivery-date")!.Value)

        };
    }
}
