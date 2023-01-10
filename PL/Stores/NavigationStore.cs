using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Stores;
/// <summary>
/// The place where we saving the current ViewModel to show the relevant View
/// </summary>
public class NavigationStore
{
#pragma warning disable CS8618 // Non-nullable field 'currentViewModel' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    private ViewModelBase currentViewModel;
#pragma warning restore CS8618 // Non-nullable field 'currentViewModel' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
    public ViewModelBase CurrentViewModel
    {
        get => currentViewModel;
        set
        {
            currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

#pragma warning disable CS8618 // Non-nullable event 'CurrentViewModelChanged' must contain a non-null value when exiting constructor. Consider declaring the event as nullable.
    public event Action CurrentViewModelChanged;
#pragma warning restore CS8618 // Non-nullable event 'CurrentViewModelChanged' must contain a non-null value when exiting constructor. Consider declaring the event as nullable.
    private void OnCurrentViewModelChanged() 
    {
        CurrentViewModelChanged?.Invoke();
    }
}
