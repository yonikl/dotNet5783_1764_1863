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

internal class OrderConfirmationViewModel:ViewModelBase
{
	private readonly NavigationStore navigationStore;
	private readonly BO.Cart cart;

	public ICommand Back { get; }

	public ICommand Confirm { get; } 
	public OrderConfirmationViewModel(NavigationStore navigationStore, BO.Cart cart)
	{
		this.navigationStore = navigationStore;
		this.cart = cart;

		Back = new NavigationCommand(new NavigationService(navigationStore, () => new CartViewModel(navigationStore, cart)));

		Confirm = new CartCommend(this,cart,navigationStore);
	}

	private string name;
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
			OnPropertyChanged(nameof(Name));
		}
	}

	private string email;
	public string Email
	{
		get
		{
			return email;
		}
		set
		{
			email = value;
			OnPropertyChanged(nameof(Email));
		}
	}

	private string address;
	public string Address
	{
		get
		{
			return address;
		}
		set
		{
			address = value;
			OnPropertyChanged(nameof(Address));
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
}
