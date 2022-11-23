namespace DalApi;
using DO;

/// <summary>
/// Interface for DalOrderItem
/// </summary>
public interface IOrderItem : ICrud<OrderItem>
{
    public IEnumerable<OrderItem> GetOrderItemsInSpecificOrder(int orderId);

    public OrderItem GetOrderItemByProductIdAndOrderId(int productId, int orderId);

}

