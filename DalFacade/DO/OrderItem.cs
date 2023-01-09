using System.Xml.Serialization;

namespace DO;

public struct OrderItem
{
   
    /// <summary>
    /// Id for this order item
    /// </summary>
    public int Id
    {
        get;
        set;
    }

    /// <summary>
    /// the id of the product that in this order item
    /// </summary>
    public int ProductID
    {
        get;
        set;
    }

    /// <summary>
    /// the id of the order that this order item belong to
    /// </summary>
    public int OrderID
    {
        set;
        get;
    }

    /// <summary>
    /// the price of the product (does not change if the product price changed) 
    /// </summary>
    public double Price
    {
        set;
        get;
    }

    /// <summary>
    /// the amount of the ordered product
    /// </summary>
    public int Amount
    {
        get;
        set;
    }

    /// <summary>
    /// printing this order item
    /// </summary>
    /// <returns></returns>
    /// return string that represent this order item
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
