using System.Runtime.Serialization;
using BO;
using Dal;
using DalApi;
using IProduct = BlApi.IProduct;

namespace BlImplementation;

internal class Product : IProduct
{
    private IDal dal = new DalList();
    public IEnumerable<ProductForList> GetAlProducts()
    {
        List<ProductForList> forList = new List<ProductForList>();
        IEnumerable<DO.Product> products = dal.Product.GetAll();
        foreach (var i in products)
        {
            forList.Add(doProductToBoProductForList(i));
        }

        return forList;
    }

    public BO.Product GetProductForAdmin(int id)
    {
        if (id <= 0)
        {
            throw new Exception();
        }

        DO.Product product;
        try
        {
            product = dal.Product.Get(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return doProductToBoProduct(product);
    }

    public ProductItem GetProductForUser(int id, Cart c)
    {
        if (id <= 0)
        {
            throw new Exception();
        }

        DO.Product product;
        try
        {
            product = dal.Product.Get(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        ///to repair
        return doProductToBoProductItem(product, 0);
    }

    public void AddProduct(BO.Product item)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(BO.Product item)
    {
        throw new NotImplementedException();
    }

    public void RemoveProduct(BO.Product item)
    {
        throw new NotImplementedException();
    }

    private BO.ProductForList doProductToBoProductForList(DO.Product p)
    {
        return new ProductForList
        {
            ID = p.ID,
            Name = p.Name,
            Price = p.Price,
            Category = (BO.Enums.Category)System.Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString())
        };
    }

    private BO.Product doProductToBoProduct(DO.Product p)
    {
        return new BO.Product()
        {
            Category = (BO.Enums.Category)System.Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString()),
            ID = p.ID,
            InStock = p.InStock,
            Name = p.Name,
            Price = p.Price
        };
    }
    private BO.ProductItem doProductToBoProductItem(DO.Product p, int amount)
    {
        return new BO.ProductItem()
        {
            Category = (BO.Enums.Category)System.Enum.Parse(typeof(BO.Enums.Category), p.Category.ToString()),
            ID = p.ID,
            InStock = p.InStock > 0,
            Name = p.Name,
            Price = p.Price,
            Amount = 
            
        };
    }
}

