
namespace BO;

public class Cart
{
    /// <summary>
    /// Customer name
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Customer email
    /// </summary>
    public string CustomerEmail { get; set; }

    /// <summary>
    /// Customer address
    /// </summary>
    public string CustomerAddress { get; set; }

    /// <summary>
    /// items that the customer choose
    /// </summary>
    public List<OrderItem> Items { get; set; }

    /// <summary>
    /// The total price
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return $"{nameof(CustomerName)}: {CustomerName}, {nameof(CustomerEmail)}: {CustomerEmail}, {nameof(CustomerAddress)}: {CustomerAddress}, {nameof(Items)}: {Items}, {nameof(TotalPrice)}: {TotalPrice}";
    }
}

