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
    private ViewModelBase currentViewModel;
    public ViewModelBase CurrentViewModel
    {
        get => currentViewModel;
        set
        {
            currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public event Action CurrentViewModelChanged;
    private void OnCurrentViewModelChanged() 
    {
        CurrentViewModelChanged?.Invoke();
    }
}
