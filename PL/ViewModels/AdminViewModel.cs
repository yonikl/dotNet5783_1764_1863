using PL.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using BlApi;

namespace PL.ViewModels;

internal class AdminViewModel : ViewModelBase
{
    private IBl bl = Factory.Get();
	public AdminViewModel(NavigationStore navigationStore)
    {

    }
}
