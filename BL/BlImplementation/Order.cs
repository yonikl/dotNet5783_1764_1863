
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
            
            order.Status = getOrderStatus(i);
                
            order.AmountOfItems = 0;
            order.TotalPrice = 0;
            IEnumerable<DO.OrderItem> orderItems = dal.OrderItem.GetOrderItemsInSpecificOrder(i.Id);
            if (!orderItems.Any()) throw new BO.BlEmptyOrderExistsException();

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
        if (id <= 0)
        {
            throw new BO.BlIDNotValidException();
        }

        DO.Order order;
        try
        {
            order = dal.Order.Get(id);
        }
        catch (DO.DalItemNotFoundException ex)
        {

            throw new BO.BlItemNotFoundException("", ex);
        }

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
        orderBo = setOrderItemsAndTotalPrice(orderBo);

        return orderBo;
    }

    public BO.Order UpdateShipping(int id)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.Get(id);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        if (doOrder.ShipDate != DateTime.MinValue)
        {
            throw new BO.BlOrderAlreadyShippedException();
        }
        doOrder.ShipDate = DateTime.Now;
        try
        {
            dal.Order.Update(doOrder);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        BO.Order boOrder = new BO.Order()
        {
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAddress = doOrder.CustomerAddress, 
            CustomerName = doOrder.CustomerName,
            DeliveryrDate = doOrder.DeliveryDate,
            ID = doOrder.Id
        };
        boOrder.Status = getOrderStatus(doOrder);
        boOrder = setOrderItemsAndTotalPrice(boOrder);

        return boOrder;
    }

    public BO.Order UpdateDelivery(int id)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.Get(id);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }
        if (doOrder.ShipDate == DateTime.MinValue)
        {
            throw new BO.BlOrderDoesNotShippedException();
        }
        if (doOrder.DeliveryDate != DateTime.MinValue)
        {
            throw new BO.BlOrderAlreadyDeliveredException();
        }

        doOrder.DeliveryDate = DateTime.Now;

        try
        {
            dal.Order.Update(doOrder);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }
        
        BO.Order boOrder = new BO.Order()
        {
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAddress = doOrder.CustomerAddress,
            CustomerName = doOrder.CustomerName,
            DeliveryrDate = doOrder.DeliveryDate,
            ID = doOrder.Id
        };
        boOrder.Status = getOrderStatus(doOrder);
        boOrder = setOrderItemsAndTotalPrice(boOrder);

        return boOrder;

    }

    public BO.OrderTracking TrackOrder(int id)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal.Order.Get(id);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }
        BO.OrderTracking boTracking = new BO.OrderTracking()
        {
            ID = doOrder.Id,
            Status = getOrderStatus(doOrder)
        };
        boTracking.OrderTimeLine = new List<Tuple<DateTime, string>>();
        boTracking.OrderTimeLine.Add(new Tuple<DateTime, string>(doOrder.OrderDate,"Ordered"));
        if (doOrder.ShipDate != DateTime.MinValue) boTracking.OrderTimeLine.Add(new Tuple<DateTime, string>(doOrder.ShipDate, "Order shipped"));
        if (doOrder.DeliveryDate != DateTime.MinValue) boTracking.OrderTimeLine.Add(new Tuple<DateTime, string>(doOrder.DeliveryDate, "Order delivered"));
        return boTracking;

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
        order.TotalPrice = 0;
        order.Items = new List<BO.OrderItem>();
        IEnumerable<DO.OrderItem> orderItems = dal.OrderItem.GetOrderItemsInSpecificOrder(order.ID);
        if (!orderItems.Any()) throw new BO.BlEmptyOrderExistsException();

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

