using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class Product : IProduct
{
    //path to the xml file for the products
    private readonly string pathToProducts = @"..\xml\Product.xml";
    /// <summary>
    /// add the product
    /// </summary>
    /// <param name="entity">
    /// the product to add
    /// </param>
    /// <returns>
    /// return the id of that product
    /// </returns>
    /// <exception cref="DO.DalItemAlreadyExistException">
    /// if the item already exists
    /// </exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Product entity)
    {
        if (entity.ID == 0) // if the product have no id
        {
            int id = 0;
            bool isIdExist = true;
            while (isIdExist)
            {
                id = new Random().Next(100000, 999999);//try to get new id
                try
                {
                    Get(id);
                }
                catch (DalItemNotFoundException)// if the id isn't exists
                {
                    isIdExist = false;
                }

            }
            entity.ID = id;
        }
        try
        {
            Get(entity.ID); //check if the product already exists
            throw new DO.DalItemAlreadyExistException();
        }
        catch (DO.DalItemNotFoundException)//if the product doesn't exists
        {
            var list = GetAll().ToList();
            list.Add(entity);
            WriteToXml(list);
        }
        return entity.ID;
    }


    /// <summary>
    /// deleting product by ID
    /// </summary>
    /// <param name="ID"></param>
    /// the id we looking by
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int ID)
    {
        Get(ID);
        var list = GetAll().ToList();
        list.RemoveAll(x => x.ID == ID);
        WriteToXml(list);
    }
    /// <summary>
    /// get product by id
    /// </summary>
    /// <param name="ID">
    /// the id we looking by
    /// </param>
    /// <returns>
    /// the product
    /// </returns>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Product Get(int ID)
    {
        return GetByCondition(x => x.ID == ID);
    }
    /// <summary>
    /// get all product that match the func if provides
    /// </summary>
    /// <param name="func">
    /// the function to look by
    /// </param>
    /// <returns>
    /// list of eligible products
    /// </returns>
   
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        List<DO.Product> list = new List<DO.Product>();
        var xmlSerializer = new XmlSerializer(typeof(List<DO.Product>));
        using (var reader = new StreamReader(pathToProducts)) // read from xml
        {
            try
            {
                list = xmlSerializer.Deserialize(reader) as List<DO.Product>;
            }
            catch(InvalidOperationException)
            {
                return list!;
            }
        }
        if (func == null)
        {
            return list!;
        }
        else
        {
            return from i in list where func(i) select i;
        }
    }
    /// <summary>
    /// get the first product that match the condition
    /// </summary>
    /// <param name="func">
    /// the condition
    /// </param>
    /// <returns>
    /// the first product that match the condition
    /// </returns>
    /// <exception cref="DO.DalItemNotFoundException">
    /// if the item doesn't exists
    /// </exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Product GetByCondition(Func<DO.Product, bool> func)
    {
        return GetAll(func).Any() ? GetAll(func).First() : throw new DO.DalItemNotFoundException();
    }
    /// <summary>
    /// update product
    /// </summary>
    /// <param name="p">
    /// the product to update
    /// </param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Product p)
    {
        Delete(p.ID);
        Add(p);
    }
    /// <summary>
    /// write back to xml
    /// </summary>
    /// <param name="list">
    /// the list to write
    /// </param>
   
    private void WriteToXml(List<DO.Product> list)
    {
        var xmlSerializer = new XmlSerializer(typeof(List<DO.Product>));
        using (var writer = new StreamWriter(pathToProducts))
        {
            xmlSerializer.Serialize(writer, list);
        }
    }
}
