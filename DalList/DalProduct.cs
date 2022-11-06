namespace Dal;
using DO;

public class DalProduct
{
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

        DataSource.AddNewProduct(product);
    }

    public Product GetProduct(int ID)
    {
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            if (DataSource.s_products[i].ID == ID) return DataSource.s_products[i];
        }
        throw new Exception("Not Found");
    }

    public Product[] GetAllProducts()
    {
        Product[] products = new Product[DataSource.Config.productsSize];
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            products[i] = DataSource.s_products[i];
        }

        return products;
    }

    public void DeleteProduct(int ID)
    {
        for (int i = 0; i < DataSource.Config.productsSize; i++)
        {
            if (DataSource.s_products[i].ID == ID) DataSource.s_products[i].ID = 0;
            return;
        }
        throw new Exception("Not Found");
    }
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
        throw new Exception("Not Found");
    }
}

