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
    public CreateNewOrderViewModel(NavigationStore navigationStore, Cart? cart=null)
    {
        if(cart == null)
            this.cart = new Cart() { Items = new List<OrderItem?>() };
        else
            this.cart = cart;
        Back = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        GoToProduct = new NavigationCommand(new NavigationService(navigationStore, () => new ProductViewModel(navigationStore, selectedProduct!.ID, this.cart)));
        this.navigationStore = navigationStore;
        GroupByCategory = false;
        SelectedCategory = BO.Enums.Category.None;
        ToTheCart = new NavigationCommand(new NavigationService(navigationStore, () => new CartViewModel(navigationStore, this.cart)));
    }


    private readonly NavigationStore navigationStore;
    private IBl bl = Factory.Get();
    private Cart cart;
    public IEnumerable<Enums.Category> Categories => (IEnumerable<Enums.Category>)Enum.GetValues(typeof(BO.Enums.Category));
    public IEnumerable<ProductItem?> Products => groupByCategory ? (IEnumerable<ProductItem?>)bl!.Product.GetCatalog(cart, selectedCategory.ToString() == "None" ? null : x => x?.Category.ToString() == selectedCategory.ToString()).OrderBy(x => x!.Category) : bl!.Product.GetCatalog(cart, selectedCategory.ToString() == "None" ? null : x => x?.Category.ToString() == selectedCategory.ToString());
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
    public BO.Enums.Category SelectedCategory
    {
        get => selectedCategory;
        set
        {
            selectedCategory = value;
            OnPropertyChanged(nameof(selectedCategory));
            OnPropertyChanged(nameof(Products));
        }
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
