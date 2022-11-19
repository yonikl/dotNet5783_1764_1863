using BlApi;
using BO;

namespace BlImplementation;
internal class Order : IOrder
{
    public IEnumerable<OrderForList> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public BO.Order GetOrder(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateShipping(int id)
    {
        throw new NotImplementedException();
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

