
namespace BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetAllItems();
    public BO.Product GetProductForAdmin(int id);
    public BO.ProductItem GetProductForUser(int id);
    public void AddProduct(BO.Product item);
    public void UpdateProduct(BO.Product item);
    public void RemoveProduct(BO.Product item);
}

