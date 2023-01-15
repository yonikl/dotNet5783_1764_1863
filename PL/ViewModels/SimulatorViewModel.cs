using PL.Services;
using PL.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModels;

internal class SimulatorViewModel:ViewModelBase
{
	public SimulatorViewModel(NavigationStore navigationStore)
	{
		navigationStore.CurrentViewModel =  this;
	}
}
