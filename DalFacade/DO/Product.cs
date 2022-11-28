namespace DO;

public struct Product
{

    /// <summary>
    /// the product id
    /// </summary>
    public int ID
    {
        get;
        set;
    }

    /// <summary>
    /// the name of the product
    /// </summary>
    public string Name
    {   
        get;
        set; 
    }

    /// <summary>
    /// the price of the product
    /// </summary>
    public double Price
    {
        get;
        set;
    }

    /// <summary>
    /// how much of this product is in stock
    /// </summary>
    public int InStock
    {
        get;
        set;
    }

    /// <summary>
    /// the category of this product
    /// </summary>
    public Enums.Category Category
    {
        get;
        set;
    }

    /// <summary>
    /// printing this product
    /// </summary>
    /// <returns></returns>
    /// return string that represent this product
    public override string ToString()
    {
        return $@"
            Product ID= {ID}: {Name}, 
            category - {Category}
    	    Price: {Price}
    	    Amount in stock: {InStock}";
    }
}
