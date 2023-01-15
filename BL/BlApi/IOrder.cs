using BO;
namespace BlApi;

/// <summary>
/// Interface for Order
/// </summary>
public interface IOrder
{
    public IEnumerable<OrderForList?> GetAllOrders();
    public Order GetOrder(int id);
    public Order UpdateShipping(int id);
    public Order UpdateDelivery(int id);
    public OrderTracking TrackOrder(int id);
    public int? GetNextOrderToHandle();
}

