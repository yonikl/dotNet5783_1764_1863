using DalApi;

namespace Dal;

internal class Product : IProduct
{
    public int Add(DO.Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int ID)
    {
        throw new NotImplementedException();
    }

    public DO.Product Get(int ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.Product GetByCondition(Func<DO.Product, bool> func)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product o)
    {
        throw new NotImplementedException();
    }
}
