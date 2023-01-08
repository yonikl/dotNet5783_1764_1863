using PL.Commands;
using PL.Services;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlApi;
using BO;

namespace PL.ViewModels;

internal class OrderViewModel: ViewModelBase
{
    private readonly IBl bl = Factory.Get();
    private readonly NavigationStore navigationStore;
    private readonly OrderForList SelctedOrder;
    public IEnumerable<OrderItem?> OrderItems => bl.Order.GetOrder(SelctedOrder.ID).Items;
    public ICommand Back { get; }

    /// <summary>
    /// Contructor to show all order items
    /// </summary>
    /// <param name="navigationStore"></param>
    /// <param name="Orderid"></param>
    public OrderViewModel(NavigationStore navigationStore, OrderForList SelctedOrder)
    {
        this.navigationStore = navigationStore; 
        this.SelctedOrder = SelctedOrder;
        messageForOrder = "Total price: "+ SelctedOrder.TotalPrice.ToString();
        Back = new NavigationCommand(new NavigationService(this.navigationStore, () => new AdminViewModel(navigationStore)));
    }

    private string messageForOrder;
    public string MessageForOrder
    {
        get
        {
            return messageForOrder;
        }
        set
        {
            messageForOrder = value;
            OnPropertyChanged(nameof(MessageForOrder));
        }
    }
}
