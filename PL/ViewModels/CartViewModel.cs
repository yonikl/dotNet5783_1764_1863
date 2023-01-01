using BO;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Commands;
using System.Windows.Input;
using PL.Services;
using BlApi;
using System.Collections.Specialized;

namespace PL.ViewModels;

internal class CartViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly Cart cart;
    public ICommand Back { get; }
    public ICommand Cancal { get; }
    public ICommand Confirm { get; }
    public ICommand UpdateItem { get; }
    public ICommand DeleteItem { get; }    
    public CartViewModel(NavigationStore navigationStore, Cart cart)
	{
        this.navigationStore = navigationStore;
        this.cart = cart;   

        Back = new NavigationCommand(new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore)));
      

        if (cart.Items.Any())
            message = "To update item right click on the context manu";
        else
            message = "Your cart is empty";
       
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
