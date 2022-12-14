using PL.Stores;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Services;

internal class NavigationService
{
    private NavigationStore navigationStore;
    private Func<ViewModelBase> createViewModel;
    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
        this.navigationStore = navigationStore;
        this.createViewModel = createViewModel;
    }

    public void Navigate()
    {
        navigationStore.CurrentViewModel = createViewModel();
    }
}
