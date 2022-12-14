using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using PL.ViewModels;

namespace PL.Commands;

internal class AddProductCommand : BaseCommand
{
    readonly IBl bl = Factory.Get();
    readonly AddOrUpdateProductViewModel model;
    public AddProductCommand(AddOrUpdateProductViewModel model)
    {
        this.model = model;
    }
    public override void Execute(object? parameter)
    {
    }
}