

using BO;
namespace BlApi;

public interface ICart
{
    public Cart Add(int id, Cart c);

    public Cart Update(int id, int amount, Cart c);

    public void MakeAnOrder(Cart c, string name, string address, string email);

}

