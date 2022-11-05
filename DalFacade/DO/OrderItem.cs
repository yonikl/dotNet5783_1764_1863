namespace DO;

public struct OrderItem
{
    public int ProductID
    {
        get;
        set;
    }

    public int OrderID
    {
        set;
        get;
    }

    public double Price
    {
        set;
        get;
    }

    public int Amount
    {
        get;
        set;
    }

    public override string ToString()
    {
        return $@"
            Product Id = {ProductID} 
            Order Id - {OrderID}
    	    price - {Price}
    	    Amount: {Amount}";
    }
}
