using PL.Commands;
using PL.Services;
using PL.Stores;
using PL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.ViewModels;

internal class MainWindowViewModel : ViewModelBase
{
    private readonly NavigationStore navigationStore;
    public ICommand GoToAdminView { get; }
    public ICommand GoToCreateNewOrder { get; }
    public ICommand GoToTrackOrders { get; }

    public MainWindowViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;

        GoToAdminView = new NavigationCommand(new NavigationService(navigationStore, () => new AdminViewModel(navigationStore)));
        GoToCreateNewOrder = new NavigationCommand(new NavigationService(navigationStore, () => new CreateNewOrderViewModel(navigationStore)));
        GoToTrackOrders = new NavigationCommand(new NavigationService(navigationStore, () => new TrackOrdersViewModel(navigationStore)));
    }
}

