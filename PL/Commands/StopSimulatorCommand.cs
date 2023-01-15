using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Commands;

internal class StopSimulatorCommand : BaseCommand
{
    public override void Execute(object? parameter)
    {
        Simulator.StopSimulation();
    }
}
