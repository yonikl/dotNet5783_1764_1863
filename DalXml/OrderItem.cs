using DalApi;

namespace Dal;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int ID)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem Get(int ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem GetByCondition(Func<DO.OrderItem, bool> func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem> GetOrderItemsInSpecificOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem o)
    {
        throw new NotImplementedException();
    }
}
