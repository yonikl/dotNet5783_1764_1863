namespace Dal;
using DalApi;
/// <summary>
/// Class to manage the dal
/// </summary>
public class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public IOrder Order => new DalOrder();
}

