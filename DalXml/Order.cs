using DalApi;
using System.Dynamic;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{
    private XElement ordersRoot, config;
    private readonly string pathToOrders = @"..\xml\Order.xml", configPath = @"..\xml\config.xml";

    public Order()
    {
        if (!File.Exists(pathToOrders))
            CreateFiles();
        else
            LoadData();

    }

    private void CreateFiles()
    {
        ordersRoot = new XElement("Orders");
        ordersRoot.Save(pathToOrders);
    }

    public int Add(DO.Order entity)
    {
        LoadData();
        int id = entity.Id;
        if (entity.Id == 0)
        {
            entity.OrderDate = DateTime.Now;
            id = GetNextID();
        }
        ordersRoot.Add(new XElement("order", new XElement("id", id), new XElement("customer-name", entity.CustomerName), new XElement("customer-address", entity.CustomerAddress), new XElement("customer-email", entity.CustomerEmail), new XElement("order-date", entity.OrderDate), new XElement("ship-date", entity.ShipDate), new XElement("delivery-date", entity.DeliveryDate)));
        ordersRoot.Save(pathToOrders);
        return entity.Id;
    }

    public void Delete(int ID)
    {
        XElement orderElement = GetXElement(ID);
        orderElement.Remove();
        ordersRoot.Save(pathToOrders);

    }

    public DO.Order Get(int ID)
    {
        LoadData();
        var order = GetXElement(ID);

        return new DO.Order()
        {
            Id = Convert.ToInt32(order.Element("id")!.Value),
            CustomerName = order.Element("customer-name")!.Value,
            CustomerAddress = order.Element("customer-address")!.Value,
            CustomerEmail = order.Element("customer-email")!.Value,
            OrderDate = order.Element("order-date")!.IsEmpty ? null : DateTime.Parse(order.Element("order-date")!.Value),
            ShipDate = order.Element("ship-date")!.IsEmpty ? null : DateTime.Parse(order.Element("ship-date")!.Value),
            DeliveryDate = order.Element("delivery-date")!.IsEmpty ? null : DateTime.Parse(order.Element("delivery-date")!.Value)

        };

    }

    private XElement GetXElement(int id)
    {
        return (from i in ordersRoot.Elements() where Convert.ToInt32(i.Element("id")!.Value) == id select i).FirstOrDefault() ?? throw new DO.DalItemNotFoundException();
    }

    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        LoadData();
        if (func == null)
            return from i in ordersRoot.Elements()
                   select XElementToDoOrder(i);
        else
            return from i in ordersRoot.Elements()
                   let j = XElementToDoOrder(i)
                   where func(j)
                   select j;
    }

    public DO.Order GetByCondition(Func<DO.Order, bool> func)
    {
        LoadData();
        return (from i in ordersRoot.Elements()
                select XElementToDoOrder(i)).Any() ? (from i in ordersRoot.Elements()
                                                      select XElementToDoOrder(i)).First() : throw new DO.DalItemNotFoundException();

    }

    public void Update(DO.Order o)
    {
        LoadData();
        Delete(o.Id);
        Add(o);
    }

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

    private int GetNextID()
    {
        LoadData();
        var id = Convert.ToInt32(config.Element("orders-id")!.Value);
        id++;
        config.Element("orders-id")!.Value = id.ToString();
        config.Save(configPath);
        return --id;
    }

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
