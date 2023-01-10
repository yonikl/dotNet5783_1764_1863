using PL.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using System.Windows.Input;
using PL.Commands;
using PL.Services;
using System.Collections.Specialized;

namespace PL.ViewModels;
/// <summary>
/// ViewModel for AdminView that let the admin to see the products/orders in the shop
/// </summary>
internal class AdminViewModel : ViewModelBase
{
    private IBl? bl = Factory.Get();

    private string message;
    public ObservableCollection<OrderForList?> Orders => new ObservableCollection<OrderForList?>(bl!.Order.GetAllOrders().OrderBy(x => x!.Status));
    // public IEnumerable<OrderForList?> Orders => bl!.Order.GetAllOrders().OrderBy(x => x!.Status);
    public IEnumerable<ProductForList?> Products
    {
        get
        {
            if (category.ToString() == "None") 
                return bl!.Product.GetAllProducts();
            return bl!.Product.GetAllProducts(x => x?.Category.ToString() == category.ToString());
        }
    }

    public IEnumerable<BO.Enums.Category> Categories => (IEnumerable<Enums.Category>)Enum.GetValues(typeof(BO.Enums.Category));

    public ICommand AddProduct { get; }
    public ICommand UpdateProduct { get; }
    public ICommand OrderDetails { get; }
    public ICommand DeleteProduct { get; }
    public ICommand UpdateShipping { get; }
    public ICommand UpdateDelivery { get; }
    public ICommand Back { get; }
    /// <summary>
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing userControlls
    /// </param>
#pragma warning disable CS8618 // Non-nullable field 'selectedProduct' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8618 // Non-nullable field 'selctedOrder' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    public AdminViewModel(NavigationStore navigationStore)
#pragma warning restore CS8618 // Non-nullable field 'selctedOrder' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning restore CS8618 // Non-nullable field 'selectedProduct' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    {
        AddProduct = new NavigationCommand(new NavigationService(navigationStore, () => new AddOrUpdateProductViewModel(navigationStore)));
        navigationStore.CurrentViewModel = this;

        Back = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        UpdateProduct = new NavigationCommand(new NavigationService(navigationStore, () => new AddOrUpdateProductViewModel(navigationStore, selectedProduct!.ID)));

        OrderDetails = new NavigationCommand(new NavigationService(navigationStore, () => new OrderViewModel(navigationStore, selctedOrder!)));
        message = "To update status for order right click on the context menu\n" + "To view the order details double click on the context manu";

        messageForProduct = "To delete product right click on the context menu";
        UpdateShipping = new UpdateShippingCommand(this);

        UpdateDelivery = new UpdateDeliveryCommand(this);

        DeleteProduct = new DeleteProductCommand(this);
    }
    private BO.Enums.Category category;
    private BO.ProductForList selectedProduct;

    private string messageForProduct;
    public string MessageForProduct
    {
        get
        {
            return messageForProduct;
        }
        set
        {
            messageForProduct = value;
            OnPropertyChanged(nameof(MessageForProduct));
        }
    }
    public BO.Enums.Category Category
    {
        get => category;
        set
        {
            category = value;
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Products));
        }
    }

    private OrderForList selctedOrder;
    public OrderForList SelctedOrder
    {
        get
        {
            return selctedOrder;
        }
        set
        {
            selctedOrder = value;
            OnPropertyChanged(nameof(SelctedOrder));
        }
    }
    public BO.ProductForList SelectedProduct
    {
        get => selectedProduct;
        set
        {
            selectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
        }
    }
   

    public string Message 
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public void Refresh()
    {
        OnPropertyChanged(nameof(Orders));
        OnPropertyChanged(nameof(Products));
    }

}
