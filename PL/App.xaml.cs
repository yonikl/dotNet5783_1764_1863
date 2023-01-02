using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PL.ViewModels;
using PL.Stores;
namespace PL;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly NavigationStore navigationStore;

    public App()
    {
        navigationStore = new NavigationStore();
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        navigationStore.CurrentViewModel = new MainWindowViewModel(navigationStore);
        MainWindow = new MainWindow(navigationStore) 
        { 
            DataContext = new MainViewModel(navigationStore) 
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}
