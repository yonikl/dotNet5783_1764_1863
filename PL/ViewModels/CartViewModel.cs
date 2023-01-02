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
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace PL.ViewModels;
/// <summary>
/// ViewModel for the CartView that allow user to see their cart
/// </summary>
internal class CartViewModel : ViewModelBase, INotifyPropertyChanged
{
    private readonly NavigationStore navigationStore;
    private Cart? cart;
    private IBl? bl = Factory.Get();

    public ObservableCollection<OrderItem?> OrderItems => new ObservableCollection<OrderItem?>(cart!.Items);
    
    public ICommand Back { get; }
    public ICommand Confirm { get; }
    public ICommand UpdateItem { get; }

    public ICommand DeleteItem { get; }
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing userControlls
    /// </param>
    /// <param name="cart">
    /// the cart to show
    /// </param>
    public CartViewModel(NavigationStore navigationStore, Cart cart)
    {
        
        this.navigationStore = navigationStore;
        this.cart = cart;
        
        Back = new NavigationCommand(new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore, cart)));


        if (cart.Items.Any())
            message = "To update or delete item right click on the context manu";
        else
            message = "Your cart is empty";
        Confirm = new NavigationCommand(new NavigationService(navigationStore, () => new OrderConfirmationViewModel(navigationStore, cart)));

        UpdateItem = new NavigationCommand(new NavigationService(navigationStore, () => new UpdateItemInCartViewModel(navigationStore, orderItem!.ProductID, cart)));

        DeleteItem = new DeleteItemCommand(cart,this);
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

    private int id ;
    public int Id
    {
        get
        {
            return orderItem.ProductID;
        }
        set
        {
            id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
    /// <summary>
    /// Function to refresh the ListView
    /// </summary>
    public void Refresh()
    {
        OnPropertyChanged(nameof(cart));
        OnPropertyChanged(nameof(OrderItems));
    }
}
