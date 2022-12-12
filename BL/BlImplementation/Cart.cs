using BlApi;


namespace BlImplementation;
internal class Cart : ICart
{

    private DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// Function for commiting cart to actual order
    /// </summary>
    /// <param name="c"></param>
    /// the cart
    /// <param name="name"></param>
    /// name of the customer
    /// <param name="address"></param>
    /// address of the customer
    /// <param name="email"></param>
    /// email of the customer
    /// <exception cref="BO.BlPersonalDetailsException"></exception>
    /// Exception when personal info isn't correct
    /// <exception cref="BO.BlAmountNotValidException"></exception>
    /// Exception when the amount <= 0
    /// <exception cref="BO.BlNotEnoughInStockException"></exception>
    /// Exception when we don't have enough in stock to make order
    /// <exception cref="BO.BlItemNotFoundException"></exception>
    /// Exception when we can't found a product
    public void MakeAnOrder(BO.Cart c, string name, string address, string email)
    {
        if (name == "" || address == "" || email == "")//checking integrity for personal information
            throw new BO.BlPersonalDetailsException();
        if (!IsValidEmail(email))
            throw new BO.BlPersonalDetailsException();

        //making new order in dal
        int id = dal?.Order.Add(new DO.Order() { 
            CustomerAddress = address,
            CustomerEmail = email,
            CustomerName = name,
            DeliveryDate = null,
            OrderDate = DateTime.Now,
            ShipDate = null }) ?? throw new NullReferenceException();

        //adding the products in the cart to the order
        foreach (var i in c.Items)
        {
            try
            {
                DO.Product product = dal?.Product.Get(i?.ProductID ?? throw new NullReferenceException()) ?? throw new NullReferenceException();
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

    /// <summary>
    /// function to add product to cart
    /// </summary>
    /// <param name="id"></param>
    /// the id of the product
    /// <param name="c"></param>
    /// the cart
    /// <returns></returns>
    /// returns the updated cart
    /// <exception cref="BO.BlItemNotFoundException"></exception>
    /// if we didn't found the product in Dal
    /// <exception cref="BO.BlAmountNotValidException"></exception>
    /// when the stock is empty
    public BO.Cart Add(int id, BO.Cart c)
    {
        DO.Product product;

        try
        {
            product = dal?.Product.Get(id) ?? throw new NullReferenceException();
        }
        catch (DO.DalItemNotFoundException ex)
        {
            throw new BO.BlItemNotFoundException("", ex);
        }

        foreach (var i in c.Items)//searching for the product in the cart
        {
            if (i?.ProductID == id )
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

        //if the product isn't in the cart
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



    /// <summary>
    /// update amount of product in cart
    /// </summary>
    /// <param name="id"></param>
    /// id of the product
    /// <param name="amount"></param>
    /// the amount ro update
    /// <param name="c"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlItemNotFoundInCartException"></exception>
    /// if we don't find the product in the cart
    /// <exception cref="BO.BlAmountNotValidException"></exception>
    /// if the amount isn't correct
    public BO.Cart UpdateAmountOfOrder(int id, int amount, BO.Cart c)
    {
        if (amount <= 0) throw new BO.BlAmountNotValidException();
        foreach (var i in c.Items)//searching for the product in the cart
        {
            if (i?.ProductID == id)
            {
                if (i.Amount == amount)
                {
                    return c;
                }

                c.TotalPrice += (amount - i.Amount) * i.Price; 
                i.TotalPrice = i.Price * amount; 
                i.Amount = amount; 
                return c;
                
            }
        }

        throw new BO.BlItemNotFoundInCartException();//if we didn't found the product in the cart

    }

    /// <summary>
    /// casting DO.Product to BO.OrderItem 
    /// </summary>
    /// <param name="p"></param>
    /// the DO.product
    /// <returns></returns>
    /// returns the BO.OrderItem

    private BO.OrderItem doProductToBoOrderItem(DO.Product p)
    {
        return new BO.OrderItem() {Amount = 1, Name = p.Name,Price = p.Price,ProductID = p.ID,TotalPrice = p.Price};
    }

    /// <summary>
    /// check validation of email
    /// </summary>
    /// <param name="email"></param>
    /// the address we verified
    /// <returns></returns>
    /// if the address is valid
    private bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; 
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            Console.WriteLine(addr.Address);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

}

