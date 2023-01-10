using BlApi;
using BO;
using PL.Commands;
using PL.Services;
using PL.Stores;
using System.Windows.Input;

namespace PL.ViewModels;
/// <summary>
/// ViewModel for ProductView that let the user to add the product to the cart
/// </summary>
internal class ProductViewModel : ViewModelBase
{
    private IBl bl = Factory.Get();
    private readonly NavigationStore navigationStore;
	private readonly ProductItem item;
	private readonly Cart cart;
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing windows
    /// </param>
    /// <param name="id">
	/// the id of the product we showing
	/// </param>
    /// <param name="cart">
	/// the cart thar we take the amount from
	/// </param>
#pragma warning disable CS8618 // Non-nullable field 'errorMessages' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    public ProductViewModel(NavigationStore navigationStore, int id, Cart cart)
#pragma warning restore CS8618 // Non-nullable field 'errorMessages' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
	{
        this.cart = cart;
        this.navigationStore = navigationStore;
        this.item = bl.Product.GetProductForUser(id, cart);
        AddToCart = new AddProductToCartCommand(cart, id, navigationStore, this);
        GoBack = new NavigationCommand(new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore, cart)));
		if (!item.InStock) ErrorMessages = "Can't add product that isn't in stock";
    }
	
	public ProductItem Item
	{
		get
		{
			return item;
		}
	}
	public ICommand GoBack { get; }
	public ICommand AddToCart { get; }

	private string errorMessages;
	public string ErrorMessages
	{
		get
		{
			return errorMessages;
		}
		set
		{
			errorMessages = value;
			OnPropertyChanged(nameof(ErrorMessages));
		}
	}
}
