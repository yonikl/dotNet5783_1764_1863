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
    public IEnumerable<ProductItem?> Products => groupByCategory ? (IEnumerable<ProductItem?>)bl!.Product.GetCatalog(cart, ).OrderBy(x => x!.Category) : bl!.Product.GetCatalog(cart);
    public ICommand Back { get; }

    public ICommand ToTheCart { get; }
    public ICommand GoToProduct { get; }

    private bool groupByCategory = false;
    public bool GroupByCategory
    {
        get => groupByCategory;
        set
        {
            groupByCategory = value;
            OnPropertyChanged(nameof(GroupByCategory));
            OnPropertyChanged(nameof(Products));
        }
    }

    private BO.Enums.Category selectedCategory;
    public BO.Enums.Category SelectedCategory { get; set; }

    public CreateNewOrderViewModel(NavigationStore navigationStore)
    {
        cart = new Cart() { Items = new List<OrderItem?>() };
        Back = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        GoToProduct = new NavigationCommand(new NavigationService(navigationStore, () => new ProducttViewModel(navigationStore, selectedProduct!)));
        this.navigationStore = navigationStore;
        GroupByCategory = false;

        ToTheCart = new NavigationCommand(new NavigationService(navigationStore, () => new CartViewModel(navigationStore, cart)));
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
