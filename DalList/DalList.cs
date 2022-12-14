namespace Dal;
using DalApi;
/// <summary>
/// Class to manage the dal
/// </summary>
internal sealed class DalList :  IDal
{
    public static IDal Instance { get; } = new DalList();

    DalList()
    {
        Product = new DalProduct();
        OrderItem = new DalOrderItem();
        Order = new DalOrder();
    }
    public IProduct Product { get; }
    public IOrderItem OrderItem { get; }
    public IOrder Order { get; }
}

