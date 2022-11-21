
using BlApi;
using BO;
using Dal;
using DalApi;


namespace BlImplementation;
internal class Cart : ICart
{

    private IDal dal = new DalList();
    public void MakeAnOrder(BO.Cart c, string name, string address, string email)
    {
        if (name == "" || address == "" || email == "")
            throw new Exception() { };
        //need to check email

        int id = dal.Order.Add(new DO.Order() { 
            CustomerAddress = address,
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
                if (i.Amount <= 0)
                    throw new BlAmountNotValidException() { };
                if (i.Amount > dal.OrderItem.Get(i.ID).Amount)
                    throw new BlNotEnoughInStockException() { };
                product.InStock -= i.Amount;
                dal.Product.Update(product);
            }
            catch (DalApi.DalItemNotFound ex)
            {
                throw new BlItemNotFoundException("",ex);
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
        catch (DalApi.DalItemNotFound ex)
        {
            throw new BlItemNotFoundException("", ex);
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
            throw new BlAmountNotValidException();
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

