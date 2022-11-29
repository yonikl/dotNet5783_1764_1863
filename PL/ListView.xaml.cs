﻿using System;
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
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategorySelector.ItemsSource =
                bl.Product.GetAllProducts(x => x?.Category == (DO.Enums.Category)Enum.Parse(typeof(DO.Enums.Category),CategorySelector.SelectedItem.ToString()));
        }
    }
}
