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
#pragma warning disable CS0169 // The field 'NavigationService.value' is never used
    private object value;
#pragma warning restore CS0169 // The field 'NavigationService.value' is never used
    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="navigationStore">
    /// we get the navigation store for changing windows
    /// </param>
    /// <param name="createViewModel">
    /// Function that returns the ViewModel that we changing to
    /// </param>
#pragma warning disable CS8618 // Non-nullable field 'value' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
#pragma warning restore CS8618 // Non-nullable field 'value' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
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
