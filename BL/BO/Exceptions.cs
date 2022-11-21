
namespace BO;



public class BlItemNotFoundException : Exception
{
    public BlItemNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class BlIDNotValidException : Exception
{

}

public class BlNotEnoughInStockException : Exception
{

}

public class BlAmountNotValidException : Exception
{

}

public class BlItemNotFoundInCartException : Exception
{

}

public class BlEmptyOrderExistsException : Exception
{

}
public class BlOrderAlreadyShippedException : Exception
{

}

public class BlOrderAlreadyDeliveredException : Exception
{

}

public class BlOrderDoesNotShippedException : Exception
{

}

public class BlNoProductsException : Exception
{

}

public class BlProductNotInCartsException : Exception
{

}

public class BlProductExistsInOrdersException : Exception
{

}