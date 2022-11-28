
namespace BO;
public class ProductForList
{
    /// <summary>
    /// ID of the product in the list
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of the product in the list
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Price of the product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Name of the product's
    /// </summary>
    public Enums.Category Category{ get; set; }

    public override string ToString()
    {
        return $" * {nameof(ID)}: {ID}\n * {nameof(Name)}: {Name}\n * {nameof(Price)}: {Price}\n * {nameof(Category)}: {Category}\n";
    }
}

