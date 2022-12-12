using BlApi;

namespace BlImplementation;

/// <summary>
/// Class to manage Bl
/// </summary>
internal sealed class Bl : IBl
{
    public IOrder Order => new Order();
    public IProduct Product => new Product();
    public ICart Cart => new Cart();

}

