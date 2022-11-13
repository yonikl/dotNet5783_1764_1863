namespace Dal;
using DalApi;

internal class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public IOrder Order => new DalOrder();
}

