using PL.Stores;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Services;
/// <summary>
/// Service to change the current ViewModel
/// </summary>
internal class NavigationService
{
    private NavigationStore navigationStore;
    private Func<ViewModelBase> createViewModel;
    private object value;
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing windows
    /// </param>
    /// <param name="createViewModel">
    /// Function that returns the ViewModel that we changing to
    /// </param>
    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
    {
        this.navigationStore = navigationStore;
        this.createViewModel = createViewModel;
    }
    /// <summary>
    /// the function to change the current ViewModel
    /// </summary>
    public void Navigate()
    {
        navigationStore.CurrentViewModel = createViewModel();
    }
}
