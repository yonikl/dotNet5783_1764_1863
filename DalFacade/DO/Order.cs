

namespace DO;

public struct Order
{
    public int Id
    {
        get;
        set;
    }

    public string CustomerName
    {
        set;
        get;
    }

    public string CustomerEmail
    {
        set;
        get;
    }

    public string CustomerAdress
    {
        set;
        get;
    }

    public DateTime OrderDate
    {
        set;
        get;
    }

    public DateTime ShipDate
    {
        set;
        get;
    }

    public DateTime DeliveryDate
    {
        set;
        get;
    }

    public override string ToString()
    {
        return $@"
            Order ID = {Id}: {CustomerName}, 
            customer emile - {CustomerEmail}
    	    customer Adress: {CustomerAdress}
    	    Order date: {OrderDate}
            Ship Date: {ShipDate}
            Delivery Date:{DeliveryDate}";
    }
}
