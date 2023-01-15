using BlApi;


namespace BlImplementation;
internal class Order : IOrder
{
    private DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// get all orders from dalorder
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlEmptyOrderExistsException">if the list is empty throw exception</exception>
    public IEnumerable<BO.OrderForList> GetAllOrders()
    {
        
        IEnumerable<DO.Order> orders = dal?.Order.GetAll() ?? throw new NullReferenceException();//get all order
        List <BO.OrderForList> list = new List<BO.OrderForList>();//create new list
        foreach (var i in orders)//copy and convert from DO orders to BO orders
        {
            BO.OrderForList order = new BO.OrderForList
            {
                CustomerName = i.CustomerName,
                ID = i.Id,
                Status = getOrderStatus(i),

                AmountOfItems = 0,
                TotalPrice = 0
            };
            IEnumerable<DO.OrderItem> orderItems = dal.OrderItem.GetAll(x => x.OrderID == i.Id);
            if (!orderItems?.Any() ?? throw new NullReferenceException()) throw new BO.BlEmptyOrderExistsException();

            foreach (var j in orderItems ?? throw new NullReferenceException())//copy the amount and the total price
            {
                order.AmountOfItems += j.Amount;
                order.TotalPrice += j.Amount * j.Price;
            }
            list.Add(order);//add to the list
        }

        return list;
    }


    /// <summary>
    /// get order using id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlIDNotValidException">throw exception if the id is smaller then 0</exception>
    /// <exception cref="BO.BlItemNotFoundException">throw exception if the id not found</exception>
    public BO.Order GetOrder(int id)
    {
        if (id <= 0)//check if the id is smaller then 0
        {
            throw new BO.BlIDNotValidException();
        }

        DO.Order order;//create new order
        try//get the id from dal throw exception if the id not found 
        {
            order = dal?.Order.Get(id) ?? throw new NullReferenceException();
        }
        catch (DO.DalItemNotFoundException ex)
        {

            throw new BO.BlItemNotFoundException("", ex);
        }

        BO.Order orderBo = new BO.Order//convert from DO to BO
        {
            CustomerAddress = order.CustomerAddress,
            ID = order.Id,
            CustomerEmail = order.CustomerEmail,
            CustomerName = order.CustomerName,
            DeliveryDate = order.DeliveryDate,
            OrderDate = order.OrderDate,
            ShipDate = order.ShipDate,
            Status = getOrderStatus(order)
        };
        orderBo = setOrderItemsAndTotalPrice(orderBo);

        return orderBo;//return the order
    }
    /// <summary>
    /// the admin update shipping for the user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlItemNotFoundException">throw exception if the id not found</exception>
    /// <exception cref="BO.BlOrderAlreadyShippedException"></exception>
    public BO.Order UpdateShipping(int id)
    {
        DO.Order doOrder;//create new order
        try//get the order by id throw exception if the order id not found
        {
            doOrder = dal?.Order.Get(id) ?? throw new NullReferenceException();
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        if (doOrder.ShipDate != null)//throw exception if the ship date is already update
        {
            throw new BO.BlOrderAlreadyShippedException();
        }
        doOrder.ShipDate = DateTime.Now;//update the ship date
        try//try to update the order
        {
            dal.Order.Update(doOrder);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        BO.Order boOrder = new BO.Order//convert from DO to BO 
        {
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAddress = doOrder.CustomerAddress,
            CustomerName = doOrder.CustomerName,
            DeliveryDate = doOrder.DeliveryDate,
            ID = doOrder.Id,
            OrderDate = doOrder.OrderDate,
            ShipDate = doOrder.ShipDate,
            Status = getOrderStatus(doOrder)//get the status order
        };
        boOrder = setOrderItemsAndTotalPrice(boOrder);//get order items and total price

        return boOrder;//return the order from kind BO
    }
    /// <summary>
    /// update delivery for the admin
    /// </summary>
    /// <param name="id">get the id to look for the product</param>
    /// <returns></returns>
    /// <exception cref="BO.BlItemNotFoundException">if the id not found</exception>
    /// <exception cref="BO.BlOrderDoesNotShippedException">if the ship date not update</exception>
    /// <exception cref="BO.BlOrderAlreadyDeliveredException">if the delivery date is update</exception>
    public BO.Order UpdateDelivery(int id)
    {
        DO.Order doOrder;
        try
        {
            doOrder = dal?.Order.Get(id) ?? throw new NullReferenceException();
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }
        if (doOrder.ShipDate == null)
        {
            throw new BO.BlOrderDoesNotShippedException();
        }
        if (doOrder.DeliveryDate != null)
        {
            throw new BO.BlOrderAlreadyDeliveredException();
        }

        doOrder.DeliveryDate = DateTime.Now;//update the delivery date

        try//try to update the product
        {
            dal.Order.Update(doOrder);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        BO.Order boOrder = new BO.Order//convert from DO to BO
        {
            CustomerEmail = doOrder.CustomerEmail,
            CustomerAddress = doOrder.CustomerAddress,
            CustomerName = doOrder.CustomerName,
            DeliveryDate = doOrder.DeliveryDate,
            ID = doOrder.Id,
            OrderDate = doOrder.OrderDate,
            ShipDate = doOrder.ShipDate,
            Status = getOrderStatus(doOrder)//get order status
        };
        boOrder = setOrderItemsAndTotalPrice(boOrder);//set the order items and total price

        return boOrder;

    }
    /// <summary>
    /// track order for the user
    /// </summary>
    /// <param name="id">to find the order</param>
    /// <returns></returns>
    /// <exception cref="BO.BlItemNotFoundException">if the order not found</exception>
    public BO.OrderTracking TrackOrder(int id)
    {
        DO.Order doOrder;
        try//try to get the order
        {
            doOrder = dal?.Order.Get(id) ?? throw new NullReferenceException();
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }
        BO.OrderTracking boTracking = new BO.OrderTracking//create new order tracking
        {
            ID = doOrder.Id,
            Status = getOrderStatus(doOrder),
            OrderTimeLine = new List<Tuple<DateTime?, string?>?>()//create list tuple from king of date time and string 
        };
        boTracking.OrderTimeLine.Add(new Tuple<DateTime?, string?>(doOrder.OrderDate,"Ordered"));
        //update the list
        if (doOrder.ShipDate != null) boTracking.OrderTimeLine.Add(new Tuple<DateTime?, string?>(doOrder.ShipDate, "Order shipped"));
        if (doOrder.DeliveryDate != null) boTracking.OrderTimeLine.Add(new Tuple<DateTime?, string?>(doOrder.DeliveryDate, "Order delivered"));
        return boTracking;

    }

    /// <summary>
    /// get DO order and return the status order from kind of BO
    /// </summary>
    /// <param name="order">get DO order</param>
    /// <returns></returns>
    private BO.Enums.OrderStatus getOrderStatus(DO.Order order)
    {
        if (order.DeliveryDate != null)
            return BO.Enums.OrderStatus.Delivered;
        if (order.ShipDate != null)
            return BO.Enums.OrderStatus.shipped;
        return BO.Enums.OrderStatus.InProcess;
    }
    /// <summary>
    /// get the list of items and price and return update order BO 
    /// </summary>
    /// <param name="order">get BO order</param>
    /// <returns></returns>
    /// <exception cref="BO.BlEmptyOrderExistsException">if the list is empty</exception>
    private BO.Order setOrderItemsAndTotalPrice(BO.Order order)
    {
        IEnumerable<DO.OrderItem> orderItems = dal?.OrderItem.GetAll(x => x.OrderID == order.ID);//get the items using id
        if (!orderItems!.Any()) //if the list is empty
            throw new BO.BlEmptyOrderExistsException();

        order.TotalPrice += orderItems!.Sum(x =>  x.Price * x.Amount);
        order.Items = ((IEnumerable<BO.OrderItem?>)(from i in orderItems
            select new BO.OrderItem()
            {
                Amount = i.Amount,
                ID = i.Id,
                Name = dal!.Product.Get(i.ProductID).Name,
                Price = i.Price,
                ProductID = i.ProductID,
                TotalPrice = i.Price * i.Amount
            })).ToList();
        return order;
    }

    public int? GetNextOrderToHandle()
    {
        var orders = dal?.Order.GetAll(x => getOrderStatus(x) != BO.Enums.OrderStatus.Delivered);
        if (!orders!.Any()) return null;
        orders!.OrderByDescending(x => x.ShipDate ?? x.OrderDate);
        return orders!.First().Id;
    }

    private DateTime? getPriority(DO.Order order)
    {
        return order.ShipDate ?? order.OrderDate;
    }
}

