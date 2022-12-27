using BlApi;
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

internal class TrackOrdersViewModel : ViewModelBase
{
    private IBl? bl = Factory.Get();

    readonly NavigationStore navigationStore;

    private int id;
    public ICommand GoBack { get; }

    public ICommand Submmit { get; }

    private string message;
    public TrackOrdersViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        GoBack = new NavigationCommand(new NavigationService(navigationStore, () => new MainWindowViewModel(navigationStore)));
        Submmit = new TrackOrderCommand(this,navigationStore);
   
    }

    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    public int Id
    {
        get => id;
        set
        {
            id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
}
