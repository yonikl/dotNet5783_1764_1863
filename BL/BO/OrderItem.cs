
namespace BO;

public class OrderItem
{
    /// <summary>
    /// ID for order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Product id
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// Price of the item
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Amount  of the items
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Total price
    /// </summary>
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(ProductID)}: {ProductID}, {nameof(Price)}: {Price}, {nameof(Amount)}: {Amount}, {nameof(TotalPrice)}: {TotalPrice}";
    }
}

