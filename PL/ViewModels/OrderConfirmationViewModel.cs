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
/// ViewModel for OrderConfirmationView that let the user to fill personal details and commit to the order
/// </summary>
internal class OrderConfirmationViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly BO.Cart cart;
    private ICommand back;
    public ICommand Back
    {
        get => back;
        set
        {
            back = value;
            OnPropertyChanged(nameof(Back));
        }
    }

    public ICommand Confirm { get; }
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing windows
    /// </param>
    /// <param name="cart">
    /// the cart
    /// </param>
#pragma warning disable CS8618 // Non-nullable field 'message' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8618 // Non-nullable field 'name' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8618 // Non-nullable field 'address' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8618 // Non-nullable field 'back' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8618 // Non-nullable field 'email' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    public OrderConfirmationViewModel(NavigationStore navigationStore, BO.Cart cart)
#pragma warning restore CS8618 // Non-nullable field 'email' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning restore CS8618 // Non-nullable field 'back' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning restore CS8618 // Non-nullable field 'address' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning restore CS8618 // Non-nullable field 'name' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning restore CS8618 // Non-nullable field 'message' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    {
        this.navigationStore = navigationStore;
        this.cart = cart;

        Back = new NavigationCommand(new NavigationService(navigationStore, () => new CartViewModel(navigationStore, cart)));

        Confirm = new CartCommend(this, cart, navigationStore);
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
