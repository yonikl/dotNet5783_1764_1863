namespace Dal;
using DO;

public class DalProduct
{
    /// <summary>
    /// adding new product to the array
    /// </summary>
    /// <param name="product"></param>
    /// the product that we adding
    public void AddProduct(Product product)
    {
        int ID = 0;
        bool isIdExist = true;
        while (isIdExist)
        {
            ID = DataSource.s_generator.Next(100000, 999999);
            try
            {
                GetProduct(ID);
            }
            catch (Exception e)
            {
                isIdExist = false;
            }

        }

        product.ID = ID;

        DataSource.s_products[DataSource.Config.productsSize++] = product;
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
    public Product GetProduct(int ID)
    {
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            if (DataSource.s_products[i].ID == ID) return DataSource.s_products[i];
        }

        throw new Exception("Not Found");
    }

    /// <summary>
    /// returning all products
    /// </summary>
    /// <returns></returns>
    /// return array of all products
    public Product[] GetAllProducts()
    {
        Product[] products = new Product[DataSource.Config.productsSize];

        //coping the array
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            products[i] = DataSource.s_products[i];
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
    public void DeleteProduct(int ID)
    {
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            if (DataSource.s_products[i].ID == ID) DataSource.s_products[i].ID = 0;
            return;
        }

        throw new Exception("Not Found");
    }

    /// <summary>
    /// updating given product
    /// </summary>
    /// <param name="product"></param>
    /// the product we updating
    /// <exception cref="Exception"></exception>
    /// if we didn't found what to update
    public void UpdateProduct(Product product)
    {
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            if (DataSource.s_products[i].ID == product.ID)
            {
                DataSource.s_products[i] = product;
                return;
            }
        }
        //if we don't found the product
        throw new Exception("Not Found");
    }
}

