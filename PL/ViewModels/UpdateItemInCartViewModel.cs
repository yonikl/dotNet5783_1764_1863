using BlApi;
using BO;
using MaterialDesignThemes.Wpf;
using PL.Commands;
using PL.Services;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.ViewModels;
/// <summary>
/// ViewModel for UpdateItemInCartView that let the user to change their amount of product in the cart
/// </summary>
internal class UpdateItemInCartViewModel : ViewModelBase
{
	private readonly IBl bl = Factory.Get();
	private readonly NavigationStore navigationStore;
	private readonly int id;
	private readonly Cart cart;
	public ICommand GoBack { get; }
    public ICommand ChangeInCart { get; }
    public ICommand DeleteItem { get; }
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing windows
    /// </param>
    /// <param name="id">
    /// the id of the product we changing
    /// </param>
    /// <param name="cart">
    /// the cart we changing
    /// </param>
#pragma warning disable CS8618 // Non-nullable property 'DeleteItem' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable field 'message' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    public UpdateItemInCartViewModel(NavigationStore navigationStore, int id, Cart cart)
#pragma warning restore CS8618 // Non-nullable field 'message' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning restore CS8618 // Non-nullable property 'DeleteItem' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
	{
		
		this.navigationStore = navigationStore;
		this.id = id;
		this.cart = cart;
		item = bl.Product.GetProductForUser(id, cart);
        this.SelectedAmount = item.Amount;
        GoBack = new NavigationCommand(new NavigationService(navigationStore, () => new CartViewModel(navigationStore,cart)));
		ChangeInCart = new UpdateItemCommand(cart, id, this, navigationStore);

    }

	public IEnumerable<int> LongIntegerList => Enumerable.Range(1, 100).ToList();

    private ProductItem item;
    public ProductItem Item
	{
		get => item;
		set
		{
			item = value;
			OnPropertyChanged(nameof(Item));
		}
	}

    private string message;
    public string Message
    {
        get
        {
            return message;
        }
        set
        {
            message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    private int selectedAmount;
	public int SelectedAmount
    {
		get => selectedAmount;
		set
		{
			selectedAmount = value;
			OnPropertyChanged(nameof(SelectedAmount));
		}
	}

}
