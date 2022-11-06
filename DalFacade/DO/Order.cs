namespace DO;

public struct Order
{
    /// <summary>
    /// Id for this product
    /// </summary>
    public int Id
    {
        get;
        set;
    }
    
    /// <summary>
    /// the customer name
    /// </summary>
    public string CustomerName
    {
        set;
        get;
    }
    
    /// <summary>
    ///the customer email 
    /// </summary>
    public string CustomerEmail
    {
        set;
        get;
    }

    /// <summary>
    /// the customer address
    /// </summary>
    public string CustomerAddress
    {
        set;
        get;
    }

    /// <summary>
    /// the date that the order was done
    /// </summary>
    public DateTime OrderDate
    {
        set;
        get;
    }

    /// <summary>
    /// the date of shipping (DateTime.minValue if not shipped)
    /// </summary>
    public DateTime ShipDate
    {
        set;
        get;
    }

    /// <summary>
    /// the date of delivery (DateTime.minValue if not delivered)
    /// </summary>
    public DateTime DeliveryDate
    {
        set;
        get;
    }

    /// <summary>
    /// printing the order
    /// </summary>
    /// <returns></returns>
    /// return string that represent this order

    public override string ToString()
    {
        return $@"
            Order Id = {Id}: {CustomerName}
            customer emile - {CustomerEmail}
    	    customer Adress: {CustomerAddress}
    	    Order date: {OrderDate}
            Ship Date: {ShipDate}
            Delivery Date:{DeliveryDate}";
    }
}
