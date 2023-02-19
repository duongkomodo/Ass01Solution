using Microsoft.Extensions.DependencyInjection;
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
using DataAccess.Models;
using BusinessObject;
using MaterialDesignThemes.Wpf;
using System.Text.RegularExpressions;

namespace SalesWPFApp {
    /// <summary>
    /// Interaction logic for WindowProducts.xaml
    /// </summary>
    public partial class WindowProducts :Window {

        ServiceProvider provider;
        List<Product> MyDataList;
        public WindowProducts() {
            InitializeComponent();

            this.DataContext = this;

            var services = new ServiceCollection();
            services.AddScoped<IProductBusiness,ProductBusinessLogic>();
            provider = services.BuildServiceProvider();
        }

        private void Window_Loaded(object sender,RoutedEventArgs e) {
            LoadDataGrid(MyDataList);
        }

        private void LoadDataGrid(List<Product> listData) {

            var productBusinessLogic = provider.GetService<IProductBusiness>();
            if (listData == null) {
                listData = productBusinessLogic.LoadAllProducts();
                MyDataList = listData;
            }

            if (MyDataList != null) {
                dgvProducts.ItemsSource = null;
                dgvProducts.ItemsSource = listData;
            }

        }

        private void btnAddProduct_Click(object sender,RoutedEventArgs e) {

            var productBusinessLogic = provider.GetService<IProductBusiness>();
            Product newProduct = new Product() {
                ProductName = tbProductNameAdd.Text,
               CategoryId  = 1,
                UnitPrice = Convert.ToDecimal(tbUnitPriceAdd.Text),
                UnitsInStock = Convert.ToInt32(tbUnitsInStockAdd.Text),
                Weight = tbProductWeightAdd.Text,
            };

            if (productBusinessLogic.AddProduct(newProduct)) {
                MessageBox.Show("Add Product Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
            } else {
                MessageBox.Show("Add Product Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            LoadDataGrid(null);
        }

        #region Update Product
        Product updateProduct;

        private void popupUpdate_Opened(object sender,RoutedEventArgs e) {
            PopupBox popupBox = sender as PopupBox;
            updateProduct = popupBox.DataContext as Product;
        }

        private void popupUpdate_Closed(object sender,RoutedEventArgs e) {
            updateProduct = null;
        }
        private void btnUpdateProduct_Click(object sender,RoutedEventArgs e) {
            var productBusinessLogic = provider.GetService<IProductBusiness>();

            if (updateProduct != null) {
                if (productBusinessLogic.UpdateProduct(updateProduct)) {
                    MessageBox.Show("Update Product Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                } else {
                    MessageBox.Show("Update Product Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                LoadDataGrid(null);
            }
        }

        private void tbProductNameUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateProduct != null) {
                updateProduct.ProductName = (sender as TextBox).Text;
            }
        }

        private void tbWeightUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateProduct != null) {
                updateProduct.Weight = (sender as TextBox).Text;
            }
        }

        private void tbUnitPriceUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateProduct != null) {
                updateProduct.UnitPrice = Convert.ToDecimal((sender as TextBox).Text);
            }
        }

        private void tbUnitsInStockUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateProduct != null) {
                updateProduct.UnitsInStock = Convert.ToInt32((sender as TextBox).Text);
            }
        }


        #endregion

        #region Remove Product
        private void btnRemoveProduct_Click(object sender,RoutedEventArgs e) {
            if (MessageBox.Show("Are you sure to delete this Product ?","Confirm",MessageBoxButton.YesNo,MessageBoxImage.Question) != MessageBoxResult.No) {
                Product removeProduct = (((sender as Button).DataContext) as Product);
                var ProductBusinessLogic = provider.GetService<IProductBusiness>();

                if (ProductBusinessLogic.RemoveProduct(removeProduct)) {
                    MessageBox.Show("Remove Product Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                } else {
                    MessageBox.Show("Remove Product Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                LoadDataGrid(null);

            }
        }

        #endregion

        private void TextBox_TextChanged(object sender,TextChangedEventArgs e) {


            Filltering();
        }

        private void tbMinPrice_TextChanged(object sender,TextChangedEventArgs e) {
            if (string.IsNullOrEmpty((sender as TextBox).Text.Trim())) {
                (sender as TextBox).Text = "0";
            }
            Filltering();

        }

        private void tbMaxPrice_TextChanged(object sender,TextChangedEventArgs e) {
            if (string.IsNullOrEmpty((sender as TextBox).Text.Trim())) {
                (sender as TextBox).Text = "0";
            }
            Filltering();
        }

        List<Product> fillterList= new List<Product>();
        private void Filltering() {
            fillterList = MyDataList;
            if (fillterList != null) {
                if (!string.IsNullOrEmpty(tbSearchByName.Text.Trim())) {

                    fillterList = fillterList.Where(x => x.ProductName.ToLower().Contains((tbSearchByName.Text.Trim().ToLower()))).ToList();

                }

                if (!string.IsNullOrEmpty(tbSearchById.Text.Trim())) {

                    fillterList = fillterList.Where(x => x.ProductId.Equals((Convert.ToInt32(tbSearchById.Text.Trim())))).ToList();

                }

                if (!string.IsNullOrEmpty(tbMinPrice?.Text.Trim()) && !string.IsNullOrEmpty(tbMaxPrice?.Text.Trim())) {

                    fillterList = fillterList.Where(x => x.UnitPrice >= Convert.ToDecimal(tbMinPrice.Text)
                    && x.UnitPrice <= Convert.ToDecimal(tbMaxPrice.Text)).ToList();

                }
                LoadDataGrid(fillterList);
            }


        }
        private void tbMinPrice_PreviewExecuted(object sender,ExecutedRoutedEventArgs e) {
            if (e.Command == ApplicationCommands.Copy ||
    e.Command == ApplicationCommands.Cut ||
    e.Command == ApplicationCommands.Paste) {
                e.Handled = true;
            }
        }


        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text) {
            return _regex.IsMatch(text);
        }
        private void tbMinPrice_PreviewTextInput(object sender,TextCompositionEventArgs e) {
                      e.Handled= IsTextAllowed(e.Text);
        }

        private void tbMaxPrice_PreviewTextInput(object sender,TextCompositionEventArgs e) {
            e.Handled = IsTextAllowed(e.Text);
        }


    }
}
