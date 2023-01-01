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

    public List<OrderItem?> OrderItems => cart!.Items;

    public ICommand Back { get; }
    public ICommand Confirm { get; }
    public ICommand UpdateItem { get; }
    public ICommand DeleteItem { get; }    
    public CartViewModel(NavigationStore navigationStore, Cart cart)
	{
        this.navigationStore = navigationStore;
        this.cart = cart;   

        Back = new NavigationCommand(new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore)));
      

        if (cart.Items.Any())
            message = "To update or delete item right click on the context manu";
        else
            message = "Your cart is empty";
        Confirm = new NavigationCommand(new NavigationService(navigationStore, () => new OrderConfirmationViewModel(navigationStore,cart)));

        DeleteItem = new DeleteItemCommand(cart, orderItem!);

        UpdateItem = new UpdateItemCommand(cart, orderItem!);
       
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

    private OrderItem orderItem;
    public OrderItem OrderItem
    {
        get
        {
            return orderItem;
        }
        set
        {
            orderItem = value;
            OnPropertyChanged(nameof(OrderItem));
        }
    }
}
