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
    private MainWindowViewModel mainWindowViewModel;
    private object value;

    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
        this.navigationStore = navigationStore;
        this.createViewModel = createViewModel;
    }

    public NavigationService(MainWindowViewModel mainWindowViewModel, object value)
    {
        this.mainWindowViewModel = mainWindowViewModel;
        this.value = value;
    }

    public void Navigate()
    {
        navigationStore.CurrentViewModel = createViewModel();
    }
}
