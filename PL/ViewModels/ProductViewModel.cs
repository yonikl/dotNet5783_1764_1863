using BlApi;
using BO;
using PL.Commands;
using PL.Services;
using PL.Stores;
using System.Windows.Input;

namespace PL.ViewModels;

internal class ProducttViewModel : ViewModelBase
{
    private IBl bl = Factory.Get();
    private readonly NavigationStore navigationStore;
	private readonly ProductItem item;
	private readonly Cart cart;

	public ProducttViewModel(NavigationStore navigationStore, int id, Cart cart)
	{
		this.cart = cart;
		this.navigationStore = navigationStore;
		this.item = bl.Product.GetProductForUser(id, cart);
		AddToCart = new AddProductToCartCommand(cart, id, navigationStore, this);
		GoBack = new NavigationCommand(new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore, cart)));
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
	private string errorMessage;
	public  string ErrorMessage
	{
		get
		{
			return errorMessage;
		}
		set
		{
			errorMessage = value;
			OnPropertyChanged(nameof(ErrorMessage));
		}
	}
}
