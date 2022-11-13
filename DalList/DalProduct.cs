﻿namespace Dal;
using DO;
using DalApi;
public class DalProduct : IProduct
{
    /// <summary>
    /// adding new product to the array
    /// </summary>
    /// <param name="product"></param>
    /// the product that we adding
    public int Add(Product product)
    {
        int ID = 0;
        bool isIdExist = true;
        while (isIdExist)
        {
            ID = DataSource.s_generator.Next(100000, 999999);
            try
            {
                Get(ID);
            }
            catch (ItemNotFound)
            {
                isIdExist = false;
            }

        }

        product.ID = ID;

        DataSource.s_products.Add(product);
        return ID;
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

        throw new ItemNotFound();
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

        throw new ItemNotFound();
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
        throw new ItemNotFound();
    }
}

