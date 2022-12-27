using BlApi;
using BO;
using PL.Commands;
using PL.Services;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PL.ViewModels;

internal class CreateNewOrderViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private IBl bl = Factory.Get();
    private Cart cart;
    public IEnumerable<Enums.Category> Categories => (IEnumerable<Enums.Category>)Enum.GetValues(typeof(BO.Enums.Category));
    public IEnumerable<ProductItem?> Products
    {
        get => bl!.Product.GetCatalog(cart);
        set
        {
            Products = value;
            OnPropertyChanged(nameof(Products));
        }
    }
    public ICommand Back { get; }
    public ICommand GoToProduct { get; }
    public bool GroupByCategory
    {
        get => GroupByCategory;
        set
        {
            Products = GroupByCategory ? (IEnumerable<ProductItem?>)bl!.Product.GetCatalog(cart).GroupBy(x => x!.Category) : bl!.Product.GetCatalog(cart);
        }
    }

    public CreateNewOrderViewModel(NavigationStore navigationStore)
    {
        cart = new Cart() { Items = new List<OrderItem?>() };
        Back = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        GoToProduct = new NavigationCommand(new NavigationService(navigationStore, () => new ProductViewModel(navigationStore, selectedProduct!)));
        this.navigationStore = navigationStore;
    }

    private ProductItem? selectedProduct;
    public ProductItem SelectedProduct
    {
        get => selectedProduct!;
        set
        {
            selectedProduct = value;
            OnPropertyChanged(nameof(selectedProduct));
        }
    }
}
