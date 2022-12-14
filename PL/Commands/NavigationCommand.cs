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
    public NavigationCommand(NavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    public override void Execute(object? parameter)
    {
        this.navigationService.Navigate();
    }
}
