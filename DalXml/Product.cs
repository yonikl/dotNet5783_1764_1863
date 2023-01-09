using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class Product : IProduct
{
    private readonly string pathToProducts = @"..\xml\Product.xml";
    public int Add(DO.Product entity)
    {
        if (entity.ID == 0)
        {
            int id = 0;
            bool isIdExist = true;
            while (isIdExist)
            {
                id = new Random().Next(100000, 999999);
                try
                {
                    Get(id);
                }
                catch (DalItemNotFoundException)
                {
                    isIdExist = false;
                }

            }
            entity.ID = id;
        }
        try
        {
            Get(entity.ID);
            throw new DO.DalItemAlreadyExistException();
        }
        catch (DO.DalItemNotFoundException)
        {
            var list = GetAll().ToList();
            list.Add(entity);
            WriteToXml(list);
        }
        return entity.ID;
    }

    public void Delete(int ID)
    {
        Get(ID);
        var list = GetAll().ToList();
        list.RemoveAll(x => x.ID == ID);
        WriteToXml(list);
    }

    public DO.Product Get(int ID)
    {
        return GetByCondition(x => x.ID == ID);
    }

    public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
    {
        List<DO.Product> list = new List<DO.Product>();
        var xmlSerializer = new XmlSerializer(typeof(List<DO.Product>));
        using (var reader = new StreamReader(pathToProducts))
        {
            try
            {
                list = xmlSerializer.Deserialize(reader) as List<DO.Product> ?? new List<DO.Product>();
            }
            catch(InvalidOperationException)
            {
                return list;
            }
        }
        if (func == null)
        {
            return list;
        }
        else
        {
            return from i in list where func(i) select i;
        }
    }

    public DO.Product GetByCondition(Func<DO.Product, bool> func)
    {
        return GetAll(func).Any() ? GetAll(func).First() : throw new DO.DalItemNotFoundException();
    }

    public void Update(DO.Product p)
    {
        Delete(p.ID);
        Add(p);

    }

    private void WriteToXml(List<DO.Product> list)
    {
        var xmlSerializer = new XmlSerializer(typeof(List<DO.Product>));
        using (var writer = new StreamWriter(pathToProducts))
        {
            xmlSerializer.Serialize(writer, list);
        }
    }

}
