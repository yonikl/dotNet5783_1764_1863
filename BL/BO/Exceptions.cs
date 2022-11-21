
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

