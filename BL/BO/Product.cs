
namespace BO;

public class Product
{
    /// <summary>
    /// ID for product
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of the customer
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price of the product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Category for the shop
    /// </summary>
    public Enums.Category Category{ get; set; }


    /// <summary>
    /// How mach in stock
    /// </summary>
    public int InStock { get; set; }

    public override string ToString()
    {
        return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(Price)}: {Price}, {nameof(Category)}: {Category}, {nameof(InStock)}: {InStock}";
    }
}

