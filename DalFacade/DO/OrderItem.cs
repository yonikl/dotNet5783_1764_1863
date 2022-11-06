namespace DO;

public struct OrderItem
{
    public int Id
    {
        get;
        set;
    }
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
            ID = {Id}
            Product Id = {ProductID} 
            Order Id - {OrderID}
    	    price - {Price}
    	    Amount: {Amount}";
    }
}
