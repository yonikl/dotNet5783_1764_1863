using System;
using System.Windows;

namespace PL;

/// <summary>
/// The main window for the application MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    public MainWindow()
    {
        InitializeComponent();
        Frame.Content = new ListView(bl);
    }
    /// <summary>
    /// Handling click on the "Welcome" Button
    /// </summary>
    private void ShowProductListViewButton_Click(object sender, RoutedEventArgs e)
    {
        //new ListView(bl).Show();
        //Close();
    }
}

