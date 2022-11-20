using BlApi;

namespace BlImplementation;

public sealed class Bl : IBl
{
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public ICart Cart => new Cart();

}

