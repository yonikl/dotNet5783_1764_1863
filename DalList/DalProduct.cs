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
        foreach (var t in DataSource.s_products)
        {
            if (t?.ID == ID) return t.Value;
        }

        throw new DalItemNotFoundException();
    }

    /// <summary>
    /// returning all products
    /// </summary>
    /// <returns></returns>
    /// return array of all products
    public IEnumerable<Product> GetAll(Func<Product,bool>? func)
    {
        List<Product> products = new List<Product>();
        if (func == null)
        {
            //coping the list
            foreach (var t in DataSource.s_products)
            {
                products.Add(t.Value);
            }
        }
        else
        {
            //coping the list by the given func
            foreach (var t in DataSource.s_products)
            {
                if(func(t.Value))
                    products.Add(t.Value);
            }
        }

        return products;
    }

    public Product GetByCondition(Func<Product, bool> func)
    {
        foreach (var product in DataSource.s_products)
        {
            if (func(product.Value))
                return product.Value;
        }
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
        foreach (var t in DataSource.s_products)
        {
            if (t?.ID == ID) DataSource.s_products.Remove(t);
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
            if (t?.ID == product.ID)
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

