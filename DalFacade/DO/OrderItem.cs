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
}
