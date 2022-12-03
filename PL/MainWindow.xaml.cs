using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BlApi.IBl bl = BlApi.Factory.Get();
    public MainWindow()
    {
        
        InitializeComponent();
    }

    private void ShowProductListViewButton_Click(object sender, RoutedEventArgs e)
    {
        new ListView(bl).Show();
        Close();
    }
}

