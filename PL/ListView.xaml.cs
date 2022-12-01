using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BlImplementation;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : Window
    {
        private IBl bl;
        public ListView(IBl bl)
        {
            this.bl = bl;
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetAllProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            CategorySelector.Text = "None";


        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = CategorySelector.SelectedItem.ToString() == "None" ? bl.Product.GetAllProducts() : bl.Product.GetAllProducts(x => x?.Category.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            new AddAndUpdate(bl).Show();
            Close();
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var a = (BO.ProductForList)((System.Windows.Controls.ListView)sender).Items[
                ((System.Windows.Controls.ListView)sender).SelectedIndex];
            new AddAndUpdate(bl, a.ID).Show();
            Close();
        }
           
    }
}
