
namespace BO;


/// <summary>
/// Exception when we don't find item
/// </summary>
public class BlItemNotFoundException : Exception
{
    public BlItemNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
/// <summary>
/// Exception when id isn't valid
/// </summary>
public class BlIDNotValidException : Exception
{

}
/// <summary>
/// Exception when we try to order more then we have in stock
/// </summary>
public class BlNotEnoughInStockException : Exception
{
    public BlNotEnoughInStockException(string? message) : base(message)
    {
    }
}

/// <summary>
/// Exception when the cart is empty
/// </summary>
public class NotItemsInCart:Exception
{

}
/// <summary>
/// Exception when th amount isn't valid
/// </summary>
public class BlAmountNotValidException : Exception
{

}
/// <summary>
/// Exception when we try to update something that isn't in the cart
/// </summary>
public class BlItemNotFoundInCartException : Exception
{

}
/// <summary>
/// Exception when we found empty order
/// </summary>
public class BlEmptyOrderExistsException : Exception
{

}
/// <summary>
/// Exception when we try to ship order that was already shipped
/// </summary>
public class BlOrderAlreadyShippedException : Exception
{

}
/// <summary>
/// Exception when we try to deliver order that was already delivered
/// </summary>
public class BlOrderAlreadyDeliveredException : Exception
{

}
/// <summary>
/// Exception when we try to deliver order that wasn't shipped
/// </summary>
public class BlOrderDoesNotShippedException : Exception
{

}
/// <summary>
/// Exception when we have no products in dal
/// </summary>
public class BlNoProductsException : Exception
{

}
/// <summary>
/// Exception when the product isn't in the cart
/// </summary>
public class BlProductNotInCartsException : Exception
{

}
/// <summary>
///  Exception when we try to remove product that is in some orders
/// </summary>
public class BlProductExistsInOrdersException : Exception
{

}
/// <summary>
/// Exception when personal info isn't correct
/// </summary>
public class BlPersonalDetailsException : Exception
{

}
/// <summary>
/// Exception when email incourect
/// </summary>
public class BlEmailIncourect : Exception
{

}

/// <summary>
///  Exception when name is empty
/// </summary>
public class BlNameEmptyException : Exception
{

}

/// <summary>
/// Exception when price isn't valid
/// </summary>
public class BlPriceNotValidException : Exception
{

}

/// <summary>
/// Exception when we try to add something that already exists
/// </summary>
public class BlItemAlreadyExistException : Exception
{
    public BlItemAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception when the category set to None
/// </summary>
public class BlCategoryDoesntSet : Exception
{

}