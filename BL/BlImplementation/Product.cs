using Dal;
using BlApi;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal dal = new DalList();
    public IEnumerable<BO.ProductForList> GetAlProducts()
    {
        List<BO.ProductForList> forList = new List<BO.ProductForList>();
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

    public BO.ProductItem GetProductForUser(int id, BO.Cart c)
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
        BO.OrderItem orderItem = c.Items.Find(x => x.ID == id);
        if (orderItem != null)
        {
            return doProductToBoProductItem(product, orderItem.Amount);
        }
        else
        {
            throw new Exception();
        }
    }

    public void AddProduct(BO.Product item)
    {
        if (item.ID <= 0) throw new Exception();
        if (item.Name == "") throw new Exception();
        if (item.InStock < 0) throw new Exception();
        if(item.Price <= 0) throw new Exception();
        try
        {
            dal.Product.Add(new DO.Product()
            {
                Category = (DO.Enums.Category)System.Enum.Parse(typeof(DO.Enums.Category), item.Category.ToString()),
                Name = item.Name,
                InStock = item.InStock,
                Price = item.Price
            });
        }
        catch(DalApi.DalItemAlreadyExist)
        {
            throw new Exception();
        }
    }

    public void UpdateProduct(BO.Product item)
    {
        if (item.ID <= 0) throw new Exception();
        if (item.Name == "") throw new Exception();
        if (item.InStock < 0) throw new Exception();
        if (item.Price <= 0) throw new Exception();

        try
        {
            dal.Product.Update((new DO.Product()
            {
                Category = (DO.Enums.Category)System.Enum.Parse(typeof(DO.Enums.Category), item.Category.ToString()),
                Name = item.Name,
                InStock = item.InStock,
                Price = item.Price
            }));
        }
        catch (DalApi.DalItemNotFound e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void RemoveProduct(int id)
    {
        try
        {
            DO.Product product = dal.Product.Get(id);
        }
        catch (DalApi.DalItemNotFound e)
        {
            Console.WriteLine(e);
            throw;
        }

        List<DO.OrderItem> orderItems = (List<DO.OrderItem>)dal.OrderItem.GetAll();
        if (((List<DO.OrderItem>)dal.OrderItem.GetAll()).Find(x => x.ProductID == id).ProductID == id)
            throw new Exception();
        dal.Product.Delete(id);
    }

    private BO.ProductForList doProductToBoProductForList(DO.Product p)
    {
        return new BO.ProductForList
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
            Amount = amount
            
        };
    }
}

