using BO;
using Dal;
using DalApi;
using IOrder = BlApi.IOrder;

namespace BlImplementation;
internal class Order : IOrder
{
    private IDal dal = new DalList();
    public IEnumerable<OrderForList> GetAllOrders()
    {
        IEnumerable<DO.Order> orders = dal.Order.GetAll();
        List<BO.OrderForList> list = new List<OrderForList>();
        foreach (var i in orders)
        {
            BO.OrderForList order = new BO.OrderForList()
            {
                CustomerName = i.CustomerName,
                ID = i.Id,

            };
            list.Add(order);
        }

        return list;
    }

    public BO.Order GetOrder(int id)
    {
        if (id > 0)
        {
            DO.Order order = dal.Order.Get(id);
            IEnumerable<DO.OrderItem> orderItems = dal.OrderItem.GetOrderItemsInSpecificOrder(id);
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
            if (order.DeliveryDate != DateTime.MinValue)
                orderBo.Status = Enums.OrderStatus.Delivered;
            else if (order.ShipDate != DateTime.MinValue)
                orderBo.Status = Enums.OrderStatus.shipped;
            else
                orderBo.Status = Enums.OrderStatus.InProcess;
            double sum = 0;
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
                sum += i.Price * i.Amount;
            }

            orderBo.TotalPrice = sum;
          
            return orderBo;
        }

        throw new NotImplementedException();
    }

    public BO.Order UpdateShipping(int id)
    {
        if (dal.Order.Get(id).DeliveryDate == DateTime.MinValue)
        {
           
        }
    }

    public BO.Order UpdateDelivery(int id)
    {
        throw new NotImplementedException();
    }

    public OrderTracking TrackOrder(int id)
    {
        throw new NotImplementedException();
    }
}

