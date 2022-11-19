
using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;
internal class Cart : ICart
{

    private IDal dal = new DalList();
    public void MakeAnOrder(BO.Cart c, string name, string adress, string email)
    {
        if (name == "" || adress == "" || email == "")
            throw new Exception() { };
      //need to check email
        foreach (var i in c.Items)
        {
          if(i.Amount <= 0 )
              throw new Exception() { };
          if (i.Amount > dal.OrderItem.Get(i.ID).Amount)
                throw new Exception() { };
        }

        int id = dal.Order.Add(new DO.Order() { 
            CustomerAddress = adress,
            CustomerEmail = email,
            CustomerName = name,
            DeliveryDate = DateTime.MinValue,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue });

        foreach (var i in c.Items)
        {
            try
            {
                DO.Product product = dal.Product.Get(i.ProductID);
                if (product.InStock < i.Amount)
                {
                    throw new Exception();
                }
                product.InStock -= i.Amount;
                dal.Product.Update(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            dal.OrderItem.Add(new DO.OrderItem()
            {
                Amount = i.Amount,
                OrderID = id,
                Price = i.Price,
                ProductID = i.ProductID
            });

        }
    }

    public BO.Cart Add(int id, BO.Cart c)
    {
        DO.Product product;

        try
        {
            product = dal.Product.Get(id);
        }
        catch (Exception e)
        {
            throw new Exception();
        }

        foreach (var i in c.Items)
        {
            if (i.ProductID == id)
            {
                if (product.InStock > 0)
                {
                    i.TotalPrice += i.Price;
                    c.TotalPrice += i.Price;
                    i.Amount++;
                    return c;
                }
            }
        }

        if (product.InStock > 0)
        {
            c.Items.Add(doProductToBoOrderItem(product));
            return c;
        }
        else
        {
            throw new Exception();
        }
    }




    public BO.Cart Update(int id, int amount, BO.Cart c)
    {

        foreach (var i in c.Items)
        {
            if (i.ProductID == id)
            {
                if (i.Amount == amount)
                {
                    return c;
                }
                else
                {
                    c.TotalPrice += (amount - i.Amount) * i.Price;
                    i.Price = i.Price * amount;
                    i.Amount = amount;
                    return c;
                }
            }
        }

        throw new Exception() { };

    }

    private BO.OrderItem doProductToBoOrderItem(DO.Product p)
    {
        return new BO.OrderItem() {Amount = 1, Name = p.Name,Price = p.Price,ProductID = p.ID,TotalPrice = p.Price};
    }


}

