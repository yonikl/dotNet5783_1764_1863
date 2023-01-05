using BlApi;
using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class ShowDetailsForOrdersCommand : BaseCommand
{
    readonly IBl bl = Factory.Get();
    readonly AdminViewModel model;
    public ShowDetailsForOrdersCommand(AdminViewModel model)
    {
        this.model = model;
    }
    public override void Execute(object? parameter)
    {
       
    }
}
