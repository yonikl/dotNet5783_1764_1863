using BO;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels;

internal class CartViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    private readonly Cart cart;
    public CartViewModel(NavigationStore navigationStore, Cart cart)
	{
        this.navigationStore = navigationStore;
        this.cart = cart;   
	}
}
