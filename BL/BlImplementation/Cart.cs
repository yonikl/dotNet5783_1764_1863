
using BlApi;
using Dal;


namespace BlImplementation;
internal class Cart : ICart
{

    private DalApi.IDal dal = new DalList();
    public void MakeAnOrder(BO.Cart c, string name, string address, string email)
    {
        if (name == "" || address == "" || email == "")
            throw new Exception() { };
        if(IsValidEmail(email))
            throw new Exception();

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
                    throw new BO.BlAmountNotValidException() { };
                if (i.Amount > dal.OrderItem.Get(i.ID).Amount)
                    throw new BO.BlNotEnoughInStockException() { };
                product.InStock -= i.Amount;
                dal.Product.Update(product);
            }
            catch (DO.DalItemNotFoundException ex)
            {
                throw new BO.BlItemNotFoundException("",ex);
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
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
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
            throw new BO.BlAmountNotValidException();
        }
    }




    public BO.Cart UpdateAmountOfOrder(int id, int amount, BO.Cart c)
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
                    i.TotalPrice = i.Price * amount;
                    i.Amount = amount;
                    return c;
                }
            }
        }

        throw new BO.BlItemNotFoundInCartException();

    }

    private BO.OrderItem doProductToBoOrderItem(DO.Product p)
    {
        return new BO.OrderItem() {Amount = 1, Name = p.Name,Price = p.Price,ProductID = p.ID,TotalPrice = p.Price};
    }

    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

}

