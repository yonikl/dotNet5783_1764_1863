using PL.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;
using PL.Models;

namespace PL.ViewModels;

internal class AdminViewModel : ViewModelBase
{
    private Shop shop;
    public IEnumerable<OrderForList?> Orders => shop.Orders;
    public IEnumerable<ProductForList?> Products => shop.Products;
	public AdminViewModel(NavigationStore navigationStore, Shop shop)
    {
        navigationStore.CurrentViewModel = this;
        this.shop = shop;
    }
}
