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
        }

    }
}
