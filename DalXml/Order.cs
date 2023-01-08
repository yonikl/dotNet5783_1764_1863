using DalApi;

namespace Dal;

internal class Order : IOrder
{
    public int Add(DO.Order entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int ID)
    {
        throw new NotImplementedException();
    }

    public DO.Order Get(int ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.Order GetByCondition(Func<DO.Order, bool> func)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order o)
    {
        throw new NotImplementedException();
    }
}
