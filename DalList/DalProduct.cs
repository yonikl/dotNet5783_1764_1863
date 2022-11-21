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
        foreach (var t in DataSource.s_products)
        {
            if (t.ID == ID) return t;
        }

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// returning all products
    /// </summary>
    /// <returns></returns>
    /// return array of all products
    public IEnumerable<Product> GetAll()
    {
        List<Product> products = new List<Product>();

        //coping the list
        foreach (var t in DataSource.s_products)
        {
            products.Add(t);
        }

        return products;
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
        foreach (var t in DataSource.s_products)
        {
            if (t.ID == ID) DataSource.s_products.Remove(t);
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
        foreach (var t in DataSource.s_products)
        {
            if (t.ID == product.ID)
            {
                DataSource.s_products.Remove(t);
                DataSource.s_products.Add(product);
                return;
            }
        }
        //if we don't found the product
        throw new DalItemNotFoundException();
    }
}

