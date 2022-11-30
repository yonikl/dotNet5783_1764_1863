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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddAndUpdate.xaml
    /// </summary>
    public partial class AddAndUpdate : Window
    {
        private IBl bl;
        public AddAndUpdate(IBl bl)
        {
            this.bl = bl;
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            ProceedButton.Content = "Add";
            IdTextBox.Text = "";
            CategorySelector.Text = "None";
            PriceTextBox.Text = "0";
            InStockTextBox.Text = "0";
            ProceedButton.Click += addProduct;
        }

        public AddAndUpdate(IBl bl, int id)
        {
            this.bl = bl;
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            var a = bl.Product.GetProductForAdmin(id);
            ProceedButton.Content = "Update";
            IdTextBox.Text = a.ID.ToString();
            CategorySelector.Text = a.Category.ToString();
            NameTextBox.Text = a.Name;
            PriceTextBox.Text = a.Price.ToString();
            InStockTextBox.Text = a.InStock.ToString();
            ProceedButton.Click += updateProduct;

        }

        private void addProduct(object sender, RoutedEventArgs e)
        {
            bl.Product.AddProduct(createProductFromData());
            this.Close();
        }

        private void updateProduct(object sender, RoutedEventArgs e)
        {
            bl.Product.UpdateProduct(createProductFromData());
            this.Close();
        }

        private Product createProductFromData()
        {
            return new Product()
            {
                ID = Convert.ToInt32(IdTextBox.Text),
                Category = (BO.Enums.Category)Enum.Parse(typeof(BO.Enums.Category), CategorySelector.Text),
                InStock = Convert.ToInt32(InStockTextBox.Text),
                Name = NameTextBox.Text,
                Price = Convert.ToDouble(PriceTextBox.Text)
            };
        }
    }
}
