using Dal;
using BlApi;
using DO;

namespace BlImplementation;

internal class Product : IProduct
{
    private DalApi.IDal dal = new DalList();
    /// <summary>
    /// get all the product for displaying
    /// </summary>
    /// <returns></returns>
    /// returns list of the products in BO.ProductForList format
    /// <exception cref="BO.BlNoProductsException"></exception>
    /// if we not have any products
    public IEnumerable<BO.ProductForList> GetAllProducts()
    {
        List<BO.ProductForList> forList = new List<BO.ProductForList>();
        IEnumerable<DO.Product> products = dal.Product.GetAll();
        //check if products exists
        if (!products.Any()) throw new BO.BlNoProductsException();
        //casting to BO.ProductForList
        foreach (var i in products)
        {
            forList.Add(doProductToBoProductForList(i));
        }

        return forList;
    }
    /// <summary>
    /// getting product details for admin
    /// </summary>
    /// <param name="id"></param>
    /// the id of the product
    /// <returns></returns>
    /// return the product
    /// <exception cref="BO.BlIDNotValidException"></exception>
    /// if the id isn't valid
    /// <exception cref="BO.BlItemNotFoundException"></exception>
    /// if we didn't found the product in the Dal
    public BO.Product GetProductForAdmin(int id)
    {
        if (id <= 0)
        {
            throw new BO.BlIDNotValidException();
        }

        DO.Product product;
        try
        {
            product = dal.Product.Get(id);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        return doProductToBoProduct(product);
    }
    /// <summary>
    /// getting product in cart details for user
    /// </summary>
    /// <param name="id"></param>
    /// the id for the product
    /// <param name="c"></param>
    /// the cart
    /// <returns></returns>
    /// returns BO.ProductItem to display for the user
    /// <exception cref="BO.BlIDNotValidException"></exception>
    /// if the id isn't valid
    /// <exception cref="BO.BlItemNotFoundException"></exception>
    /// if we didn't found the product in the Dal
    /// <exception cref="BO.BlProductNotInCartsException"></exception>
    /// if the product isn't in the cart
    public BO.ProductItem GetProductForUser(int id, BO.Cart c)
    {
        if (id <= 0)//checking the id
        {
            throw new BO.BlIDNotValidException();
        }

        DO.Product product;
        try//trying to retrieve from Dal
        {
            product = dal.Product.Get(id);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }
        //searching for the product in the cart
        BO.OrderItem orderItem = c.Items.Find(x => x.ID == id);
        if (orderItem == null)
        { 
            throw new BO.BlProductNotInCartsException();
           
        }
        return doProductToBoProductItem(product, orderItem.Amount);
    }

    /// <summary>
    /// adding product to Dal
    /// </summary>
    /// <param name="item"></param>
    /// the product we adding
    /// <exception cref="BO.BlItemAlreadyExistException"></exception>
    /// if the id already exists
    /// <exception cref="BO.BlIDNotValidException"></exception>
    /// if the id is incorrect
    /// <exception cref="BO.BlNameEmptyException"></exception>
    /// when the name is empty
    /// <exception cref="BO.BlAmountNotValidException"></exception>
    /// when InStock isn't valid
    /// <exception cref="BO.BlPriceNotValidException"></exception>
    /// when price is incorrect
    public void AddProduct(BO.Product item)
    {
        if (item.ID <= 0) throw new BO.BlIDNotValidException();
        if (item.Name == "") throw new BO.BlNameEmptyException();
        if (item.InStock < 0) throw new BO.BlAmountNotValidException();
        if(item.Price <= 0) throw new BO.BlPriceNotValidException();

        try
        {
            //sending the product to Dal
            dal.Product.Add(new DO.Product()
            {
                Category = (Enums.Category)Enum.Parse(typeof(Enums.Category), item.Category.ToString()),
                Name = item.Name,
                InStock = item.InStock,
                Price = item.Price
            });

        }
        catch (DO.DalItemAlreadyExistException ex)//if the id already exists
        {
            throw new BO.BlItemAlreadyExistException("", ex);
        }
        
    }
    /// <summary>
    /// updating product in Dal
    /// </summary>
    /// <param name="item"></param>
    /// the product we updating
    /// <exception cref="BO.BlIDNotValidException"></exception>
    /// if the id is incorrect
    /// <exception cref="BO.BlNameEmptyException"></exception>
    /// when the name is empty
    /// <exception cref="BO.BlAmountNotValidException"></exception>
    /// when InStock isn't valid
    /// <exception cref="BO.BlPriceNotValidException"></exception>
    /// when price is incorrect
    /// <exception cref="BO.BlItemNotFoundException"></exception>
    /// if we didn't found the product to update
    public void UpdateProduct(BO.Product item)
    {
        if (item.ID <= 0) throw new BO.BlIDNotValidException();
        if (item.Name == "") throw new BO.BlNameEmptyException();
        if (item.InStock < 0) throw new BO.BlAmountNotValidException();
        if (item.Price <= 0) throw new BO.BlPriceNotValidException();

        try
        {
            dal.Product.Update((new DO.Product()//trying to update in Dal
            {
                Category = (DO.Enums.Category)System.Enum.Parse(typeof(DO.Enums.Category), item.Category.ToString()),
                Name = item.Name,
                InStock = item.InStock,
                Price = item.Price
            }));
        }
        catch (DO.DalItemNotFoundException ex)//if the id doesn't exists
        {
            throw new BO.BlItemNotFoundException("", ex);
        }
    }
    /// <summary>
    /// removing product from Dal
    /// </summary>
    /// <param name="id"></param>
    /// the id we removing by
    /// <exception cref="BO.BlItemNotFoundException"></exception>
    /// if we didn't found the product to remove 
    /// <exception cref="BO.BlProductExistsInOrdersException"></exception>
    /// if the product exists in orders
    public void RemoveProduct(int id)
    {
        try
        {
            DO.Product product = dal.Product.Get(id);
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        List<DO.OrderItem> orderItems = (List<DO.OrderItem>)dal.OrderItem.GetAll();
        if (orderItems.Find(x => x.ProductID == id).ProductID == id) throw new BO.BlProductExistsInOrdersException();
            
        dal.Product.Delete(id);
    }
    /// <summary>
    /// casting DO.Product to BO.ProductForList
    /// </summary>
    /// <param name="p"></param>
    /// DO.Product to cast
    /// <returns></returns>
    /// the casted BO.ProductForList
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
    /// <summary>
    /// casting DO.Product to BO.Product
    /// </summary>
    /// <param name="p"></param>
    /// DO.Product to cast
    /// <returns></returns>
    /// the casted BO.Product
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
    /// <summary>
    /// casting DO.Product to BO.ProductItem and adding amount
    /// </summary>
    /// <param name="p"></param>
    /// DO.Product to cast
    /// <param name="amount"></param>
    /// the amount to add
    /// <returns></returns>
    /// the casted BO.ProductItem
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

