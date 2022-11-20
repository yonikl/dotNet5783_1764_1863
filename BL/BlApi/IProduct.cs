
using BO;

namespace BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetAlProducts();
    public BO.Product GetProductForAdmin(int id);
    public BO.ProductItem GetProductForUser(int id, Cart c);
    public void AddProduct(BO.Product item);
    public void UpdateProduct(BO.Product item);
    public void RemoveProduct(int id);
}

