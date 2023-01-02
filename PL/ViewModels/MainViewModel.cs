using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels;
/// <summary>
/// the main ViewModel for changing the userControlls in the window
/// </summary>
internal class MainViewModel : ViewModelBase
{
    private NavigationStore navigationStore;
    public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;
    /// <summary>
    /// constructor that we saving the navigationStore and subscribing to the userControll change event
    /// </summary>
    /// <param name="navigationStore">
    /// the navigationStore
    /// </param>
    public MainViewModel(NavigationStore navigationStore)
    {
        this.navigationStore = navigationStore;
        this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }
    /// <summary>
    /// function to notify the window to change the userControlls in the window
    /// </summary>
    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}
