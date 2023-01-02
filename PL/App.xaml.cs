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
        navigationStore = new NavigationStore(); // we creating navigationStore for our app
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        navigationStore.CurrentViewModel = new MainWindowViewModel(navigationStore); // setting the startup point
        MainWindow = new MainWindow() // creating the MainWindow
        { 
            DataContext = new MainViewModel(navigationStore) // setting the data context for the ContentControl
        };
        MainWindow.Show();
        base.OnStartup(e);
    }
}
