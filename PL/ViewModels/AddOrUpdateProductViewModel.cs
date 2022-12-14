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

internal class AddOrUpdateProductViewModel : ViewModelBase
{
    private NavigationStore navigationStore;
    private int productId;
    private string? productName;
    private double productPrice;
    private int productInStock;
    private BO.Enums.Category productCategory;

    
    public ICommand GoBack { get; }
    public ICommand AddOrUpdate { get; }
    public AddOrUpdateProductViewModel(NavigationStore navigationStore, string str, int id=0)
    {
        submitButtonText = str;
        this.navigationStore = navigationStore;
        GoBack = new NavigationCommand(new NavigationService( this.navigationStore, GoBackToMainWindowViewModel));

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
        return new MainWindowViewModel(navigationStore);
    }
}
