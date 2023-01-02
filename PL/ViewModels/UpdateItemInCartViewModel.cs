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

internal class UpdateItemInCartViewModel : ViewModelBase
{
	private readonly IBl bl = Factory.Get();
	private readonly NavigationStore navigationStore;
	private readonly int id;
	private readonly Cart cart;
	public ICommand GoBack { get; }
    public ICommand ChangeInCart { get; }
    public ICommand DeleteItem { get; }

    public UpdateItemInCartViewModel(NavigationStore navigationStore, int id, Cart cart)
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
