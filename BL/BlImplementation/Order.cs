
using Dal;
using BlApi;

namespace BlImplementation;
internal class Order : IOrder
{
    private DalApi.IDal dal = new DalList();
    public IEnumerable<BO.OrderForList> GetAllOrders()
    {
        IEnumerable<DO.Order> orders = dal.Order.GetAll();
        List<BO.OrderForList> list = new List<BO.OrderForList>();
        foreach (var i in orders)
        {
            BO.OrderForList order = new BO.OrderForList()
            {
                CustomerName = i.CustomerName,
                ID = i.Id,
            };
            if (i.DeliveryDate != DateTime.MinValue)
                order.Status = BO.Enums.OrderStatus.Delivered;
            else if (i.ShipDate != DateTime.MinValue)
                order.Status =BO.Enums.OrderStatus.shipped;
            else
                order.Status = BO.Enums.OrderStatus.InProcess;
            order.AmountOfItems = 0;
            order.TotalPrice = 0;
            IEnumerable<DO.OrderItem> orderItems = dal.OrderItem.GetOrderItemsInSpecificOrder(i.Id);
            foreach (var j in orderItems)
            {
                order.AmountOfItems += j.Amount;
                order.TotalPrice += j.Amount * j.Price;
            }
            list.Add(order);
        }

        return list;
    }

    public BO.Order GetOrder(int id)
    {
        if (id > 0)
        {
            DO.Order order = dal.Order.Get(id);
            BO.Order orderBo = new BO.Order()
            {
                CustomerAddress = order.CustomerAddress,
                ID = order.Id,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                DeliveryrDate = order.DeliveryDate,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
            };
            orderBo.Status = getOrderStatus(order);
            orderBo.TotalPrice = 0;
            orderBo.Items = new List<BO.OrderItem>();
            orderBo = setOrderItemsAndTotalPrice(orderBo);

            return orderBo;
        }
        else
        {
            throw new Exception();
        }
    }

    public BO.Order UpdateShipping(int id)
    {
        DO.Order order;
        try
        {
            order = dal.Order.Get(id);
        }
        catch (DalApi.DalItemNotFound e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (order.ShipDate != DateTime.MinValue)
        {
            throw new Exception();
        }
        order.ShipDate = DateTime.Now;
        try
        {
            dal.Order.Update(order);
        }
        catch (DalApi.DalItemNotFound e)
        {
            Console.WriteLine(e);
            throw;
        }

        IEnumerable<DO.OrderItem> orderItems;
        try
        {
            orderItems = dal.OrderItem.GetOrderItemsInSpecificOrder(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        BO.Order orderBo = new BO.Order()
        {
            
            CustomerEmail = order.CustomerEmail,
            CustomerAddress = order.CustomerAddress, 
            CustomerName = order.CustomerName,
            DeliveryrDate = order.DeliveryDate,
            ID = order.Id
        };
        orderBo.Status = getOrderStatus(order);
        foreach (var i in orderItems)
        {
            orderBo.Items.Add(new BO.OrderItem()
            {
                Amount = i.Amount,
                ID = i.Id,
                Name = dal.Product.Get(i.ProductID).Name,
                Price = i.Price,
                ProductID = i.ProductID,
                TotalPrice = i.Price * i.Amount
            });
            orderBo.TotalPrice += i.Price * i.Amount;
        }

        return orderBo;
    }

    public BO.Order UpdateDelivery(int id)
    {
        throw new NotImplementedException();
    }

    public BO.OrderTracking TrackOrder(int id)
    {
        throw new NotImplementedException();
    }

    private BO.Enums.OrderStatus getOrderStatus(DO.Order order)
    {
        if (order.DeliveryDate != DateTime.MinValue)
            return BO.Enums.OrderStatus.Delivered;
        if (order.ShipDate != DateTime.MinValue)
            return BO.Enums.OrderStatus.shipped;
        return BO.Enums.OrderStatus.InProcess;
    }

    private BO.Order setOrderItemsAndTotalPrice(BO.Order order)
    {
        IEnumerable<DO.OrderItem> orderItems;
        try
        {
            orderItems = dal.OrderItem.GetOrderItemsInSpecificOrder(order.ID);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        order.TotalPrice = 0;
        foreach (var i in orderItems)
        {
            order.Items.Add(new BO.OrderItem()
            {
                Amount = i.Amount,
                ID = i.Id,
                Name = dal.Product.Get(i.ProductID).Name,
                Price = i.Price,
                ProductID = i.ProductID,
                TotalPrice = i.Price * i.Amount
            });
            order.TotalPrice += i.Price * i.Amount;
        }
        return order;
    }
}

