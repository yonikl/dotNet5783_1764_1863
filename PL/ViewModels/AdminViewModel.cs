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
    public IEnumerable<OrderForList?> Orders => bl!.Order.GetAllOrders().OrderBy(x => x!.Status);
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

    public ICommand UpdateShipping { get; }
    public ICommand UpdateDelivery { get; }
    public ICommand Back { get; }
    /// <summary>
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing userControlls
    /// </param>
    public AdminViewModel(NavigationStore navigationStore)
    {
        AddProduct = new NavigationCommand(new NavigationService(navigationStore, () => new AddOrUpdateProductViewModel(navigationStore)));
        navigationStore.CurrentViewModel = this;

        Back = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        UpdateProduct = new NavigationCommand(new NavigationService(navigationStore, () => new AddOrUpdateProductViewModel(navigationStore, selectedProduct!.ID)));
        Message = "To update status for order right click on the context menu";

        UpdateShipping = new UpdateShippingCommand(this);

        UpdateDelivery = new UpdateDeliveryCommand(this);

    }
    private BO.Enums.Category category;
    private BO.ProductForList selectedProduct;
  
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

    public BO.ProductForList SelectedProduct
    {
        get => selectedProduct;
        set
        {
            selectedProduct = value;
            OnPropertyChanged(nameof(SelectedProduct));
        }
    }
    
    private BO.OrderForList selctedOrderTracking;

    public BO.OrderForList SelectedOrderTracking
    { 
        get => selctedOrderTracking;
        set
        {
            selctedOrderTracking = value;
            OnPropertyChanged(nameof(SelectedOrderTracking));
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
