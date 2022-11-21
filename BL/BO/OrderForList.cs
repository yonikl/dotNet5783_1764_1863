
namespace BO;

public class OrderForList
{
    /// <summary>
    /// ID of the order list
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of the customer
    /// </summary>
    public string CustomerName { get; set; }

    /// <summary>
    /// Status of the order
    /// </summary>
    public Enums.OrderStatus Status { get; set; }

    /// <summary>
    /// Amount of item's
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// Total price
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return $"{nameof(ID)}: {ID}, {nameof(CustomerName)}: {CustomerName}, {nameof(Status)}: {Status}, {nameof(AmountOfItems)}: {AmountOfItems}, {nameof(TotalPrice)}: {TotalPrice}";
    }
}

