using PL.Commands;
using PL.Services;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PL.Models;

namespace PL.ViewModels;

internal class AddOrUpdateProductViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly Shop shop;
    private int productId;
    private string? productName;
    private double productPrice;
    private int productInStock;
    private BO.Enums.Category productCategory;

    
    public ICommand GoBack { get; }
    public ICommand AddOrUpdate { get; }


    public AddOrUpdateProductViewModel(NavigationStore navigationStore, Shop shop, int id=0)
    {
        this.navigationStore = navigationStore;
        this.shop = shop;
        GoBack = new NavigationCommand(new NavigationService( this.navigationStore, GoBackToMainWindowViewModel));
        submitButtonText = id == 0 ? "Add product" : "Update product";
        AddOrUpdate = id == 0 ? new AddProductCommand(this) : new UpdateProductCommand(this);
    }
    public string submitButtonText { get; }
    public int ProductId
	{
		get
		{
			return productId;
		}
		set
		{
            productId = value;
			OnPropertyChanged(nameof(ProductId));
		}
	}

    public string ProductName
    {
        get
        {
            return productName;
        }
        set
        {
            productName = value;
            OnPropertyChanged(nameof(ProductName));
        }
    }
	public double ProductPrice
    {
		get
		{
			return productPrice;
		}
		set
		{
			productPrice = value;
		OnPropertyChanged(nameof(ProductPrice));
		}
	}
    public int ProductInStock
    {
        get
        {
            return productInStock;
        }
        set
        {
            productInStock = value;
            OnPropertyChanged(nameof(ProductInStock));
        }
    }
    public BO.Enums.Category ProductCategory
    {
        get
        {
            return productCategory;
        }
        set
        {
            productCategory = (BO.Enums.Category)value;
            OnPropertyChanged(nameof(ProductCategory));
        }
    }

    public ViewModelBase GoBackToMainWindowViewModel()
    {
        return new MainWindowViewModel(navigationStore, shop);
    }
}
