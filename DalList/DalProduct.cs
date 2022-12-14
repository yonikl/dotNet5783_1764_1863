namespace Dal;
using DO;
using DalApi;
internal class DalProduct : IProduct
{
    /// <summary>
    /// adding new product to the array
    /// </summary>
    /// <param name="product"></param>
    /// the product that we adding
    public int Add(Product product)
    {
        if (product.ID != 0)//if it already have ID
        {
            try
            {
                Get(product.ID);
            }
            catch (DalItemNotFoundException)//if the id isn't already exists
            {
                DataSource.s_products.Add(product);
                return product.ID;
            }
            //if the id already exists
            throw new DalItemAlreadyExistException();

        }
        int id = 0;
        bool isIdExist = true;
        while (isIdExist)
        {
            id = DataSource.s_generator.Next(100000, 999999);
            try
            {
                Get(id);
            }
            catch (DalItemNotFoundException)
            {
                isIdExist = false;
            }

        }

        product.ID = id;

        DataSource.s_products.Add(product);
        return id;
    }

    /// <summary>
    /// returning product by ID
    /// </summary>
    /// <param name="ID"></param>
    /// the id we looking for
    /// <returns></returns>
    /// the product with the given id
    /// <exception cref="Exception"></exception>
    /// if we didn't found
    public Product Get(int ID)
    {
        var orderId = from product in DataSource.s_products where product?.ID == ID select product;
        if (orderId.Any())
            return orderId.First() ?? throw new NullReferenceException();

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// returning all products
    /// </summary>
    /// <returns></returns>
    /// return array of all products
    public IEnumerable<Product> GetAll(Func<Product,bool>? func)
    {
        
        if (func == null)
        {
            //coping the list
           return from pro in DataSource.s_products select pro ?? throw new NullReferenceException();
        }

        //coping the list by the given func
        return (IEnumerable<Product>)DataSource.s_products.Where(pro => func(pro ?? throw new NullReferenceException()));

    }

    public Product GetByCondition(Func<Product, bool> func)
    {
        var product =   DataSource.s_products.Where(x => func(x ?? throw new NullReferenceException()));
        if (product.Any())
            return product.First() ?? throw new NullReferenceException();
        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// deleting product by ID
    /// </summary>
    /// <param name="ID"></param>
    /// the id we looking by
    /// <exception cref="Exception"></exception>
    /// if we didn't found this id
    public void Delete(int ID)
    {
        var productToDelete = from product in DataSource.s_products where product?.ID == ID select product ?? throw new NullReferenceException();
        if (productToDelete.Any())
        {
            DataSource.s_products.Remove(productToDelete.First());
            return;
        }
        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// updating given product
    /// </summary>
    /// <param name="product"></param>
    /// the product we updating
    /// <exception cref="Exception"></exception>
    /// if we didn't found what to update
    public void Update(Product product)
    {
       Delete(product.ID);
       DataSource.s_products.Add(product);
    }
}

