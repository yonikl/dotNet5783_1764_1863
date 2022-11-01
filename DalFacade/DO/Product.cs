﻿namespace DO;

public struct Product
{
    public int ID
    {
        get;
        set;
    }

    public string Name
    {   
        get;
        set; 
    }

    public double Price
    {
        get;
        set;
    }

    public int InStock
    {
        get;
        set;
    }

    public Enums.Category Category
    {
        get;
        set;
    }
    public override string ToString()
    {
        return $@"
            Product ID= {ID}: {Name}, 
            category - {Category}
    	    Price: {Price}
    	    Amount in stock: {InStock}";
    }
}
