using DalApi;

namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    DalXml() { }
    public IProduct Product { get; } = new Product();
    public IOrderItem OrderItem { get; } = new OrderItem();
    public IOrder Order { get; } = new Order();
}
