namespace Dal;
using DalApi;
/// <summary>
/// Class to manage the dal
/// </summary>
internal sealed class DalList :  IDal
{
    public static IDal Instance { get; } = new DalList();
    DalList(){}
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public IOrder Order => new DalOrder();
}

