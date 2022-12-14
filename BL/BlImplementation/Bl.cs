using BlApi;

namespace BlImplementation;

/// <summary>
/// Class to manage Bl
/// </summary>
internal sealed class Bl : IBl
{
    internal Bl()
    {
        Order = new Order();
        Product = new Product();
        Cart = new Cart();
    }
    public IOrder Order { get;}
    public IProduct Product { get;}
    public ICart Cart { get; }

}

