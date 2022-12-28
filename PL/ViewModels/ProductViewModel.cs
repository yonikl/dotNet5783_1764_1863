using BO;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels;

internal class ProducttViewModel : ViewModelBase
{
	private readonly NavigationStore navigationStore;
	private readonly ProductItem item;

	public ProducttViewModel(NavigationStore navigationStore, ProductItem item)
	{
		this.navigationStore = navigationStore;
		this.item = item;
	}
}
