
namespace BO;

public class ProductItem
{
    /// <summary>
    /// ID of the product
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of the product
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Price of the product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    ///  product's category
    /// </summary>
    public Enums.Category? Category { get; set; }

    /// <summary>
    /// Amount of the product in the shopper cart
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// if the product in stock
    /// </summary>
    public bool InStock{ get; set; }

    public override string ToString()
    {
        return $" * {nameof(ID)}: {ID}\n * {nameof(Name)}: {Name}\n * {nameof(Price)}: {Price}\n * {nameof(Category)}: {Category}\n * {nameof(Amount)}: {Amount}\n * {nameof(InStock)}: {InStock}";
    }
}

