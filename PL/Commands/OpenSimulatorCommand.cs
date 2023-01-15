using PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class OpenSimulatorCommand : BaseCommand
{
    public override void Execute(object? parameter)
    {
        new SimulationWindow().Show();
    }
}
