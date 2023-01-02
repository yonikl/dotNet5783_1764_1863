using PL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class NavigationCommand : BaseCommand
{
    private readonly NavigationService navigationService;

    /// <summary>
    /// Constructor that save the navigation sarvice
    /// </summary>
    /// <param name="navigationService"></param>
    public NavigationCommand(NavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    /// <summary>
    /// Execute the navigate
    /// </summary>
    /// <param name="parameter"></param>
    public override void Execute(object? parameter)
    {
        this.navigationService.Navigate();
    }
}
