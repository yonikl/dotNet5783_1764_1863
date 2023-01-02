using BO;
namespace BlApi;

/// <summary>
/// Interface for Cart
/// </summary>
public interface ICart
{
    public Cart Add(int id, Cart c);

    public Cart UpdateAmountOfOrder(int id, int amount, Cart c);

    public int MakeAnOrder(Cart c, string name, string address, string email);

}

