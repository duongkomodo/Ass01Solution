using BusinessObject;
using DataAccess.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace SalesWPFApp {
    /// <summary>
    /// Interaction logic for WindowOrders.xaml
    /// </summary>
    ///

    public partial class WindowOrders :Window {

        ServiceProvider provider;
       public List<Order> MyDataList {
            get; set;
        }
        public List<Member> MyMemberList { get; set; }
        public WindowOrders() {
            InitializeComponent();
            DataContext = this;

            var services = new ServiceCollection();
            services.AddScoped<IOrderBusiness,OrderBusinessLogic>();
            services.AddScoped<IMemberBusiness,MemberBusinessLogic>();
            provider = services.BuildServiceProvider();
        }

        private void Window_Loaded(object sender,RoutedEventArgs e) {
            LoadDataGrid();
            LoadComboBox(cbMemberAdd);
        }

        private void LoadComboBox(ComboBox box) {
            var memberBusinessLogic = provider.GetService<IMemberBusiness>();
        MyMemberList =  memberBusinessLogic.LoadAllMembers();
            if (MyMemberList != null) {
                box.ItemsSource = null;
                box.ItemsSource = MyMemberList;
            }
        }

        private void LoadDataGrid() {
            var OrderBusinessLogic = provider.GetService<IOrderBusiness>();
            MyDataList = OrderBusinessLogic.LoadAllOrders();
            if (MyDataList != null) {
                dgvOrders.ItemsSource = null;
                dgvOrders.ItemsSource = MyDataList;
            }

        }

        private void btnAddOrder_Click(object sender,RoutedEventArgs e) {
            var OrderBusinessLogic = provider.GetService<IOrderBusiness>();

            Order newOrder = new Order() {
                MemberId = (int)cbMemberAdd.SelectedValue,
                OrderDate = dpOrderDateAdd.SelectedDate != null ? (DateTime)dpOrderDateAdd.SelectedDate : DateTime.Now,
                RequiredDate = dpRequiredDateAdd.SelectedDate,
                ShippedDate = dpShippedDateAdd.SelectedDate,
                Freight = Convert.ToDecimal(tbFreightAdd.Text),
            };

            if (OrderBusinessLogic.AddOrder(newOrder)) {
                MessageBox.Show("Add Order Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
            } else {
                MessageBox.Show("Add Order Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            LoadDataGrid();
        }

        Order updateOrder;
        private void popupUpdate_Opened(object sender,RoutedEventArgs e) {
            PopupBox popupBox = sender as PopupBox;
            updateOrder = popupBox.DataContext as Order;
        }

        private void btnUpdateOrder_Click(object sender,RoutedEventArgs e) {
            var OrderBusinessLogic = provider.GetService<IOrderBusiness>();

            if (updateOrder != null) {
                if (OrderBusinessLogic.UpdateOrder(updateOrder)) {
                    MessageBox.Show("Update Order Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                } else {
                    MessageBox.Show("Update Order Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                LoadDataGrid();
            }
        }

        private void cbMemberUpdate_SelectionChanged(object sender,SelectionChangedEventArgs e) {
            if (updateOrder != null) {
                updateOrder.MemberId = Convert.ToInt32((sender as ComboBox).SelectedValue);
            }
        }

        private void dpOrderDateUpdate_SelectedDateChanged(object sender,SelectionChangedEventArgs e) {
            var select = (sender as DatePicker);
            if (updateOrder != null && select.SelectedDate != null) {
                updateOrder.OrderDate = (DateTime)select.SelectedDate;
            }
        }

        private void dpRequiredDateUpdate_SelectedDateChanged(object sender,SelectionChangedEventArgs e) {
            if (updateOrder != null) {
                updateOrder.RequiredDate = (sender as DatePicker).SelectedDate;
            }
        }

        private void dpShippedDateUpdate_SelectedDateChanged(object sender,SelectionChangedEventArgs e) {
            if (updateOrder != null) {
                updateOrder.ShippedDate = (sender as DatePicker).SelectedDate;
            }
        }

        private void tbFreightUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateOrder != null) {
                updateOrder.Freight = Convert.ToDecimal((sender as TextBox).Text);
            }
        }

        private void btnRemoveOrder_Click(object sender,RoutedEventArgs e) {
            if (MessageBox.Show("Are you sure to delete this Order ?","Confirm",MessageBoxButton.YesNo,MessageBoxImage.Question) != MessageBoxResult.No) {
                Order removeOrder = (((sender as Button).DataContext) as Order);
                var OrderBusinessLogic = provider.GetService<IOrderBusiness>();

                if (OrderBusinessLogic.RemoveOrder(removeOrder)) {
                    MessageBox.Show("Remove Order Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                } else {
                    MessageBox.Show("Remove Order Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                LoadDataGrid();


            }
        }

        public void Datefilter() {

            if (dpStartDate.SelectedDate != null && dpEndDate.SelectedDate != null) {
                dgvOrders.ItemsSource = MyDataList.Where(x => x.OrderDate >= dpStartDate.SelectedDate && x.OrderDate <= dpEndDate.SelectedDate).ToList();
            } else {
                dgvOrders.ItemsSource = MyDataList;
            }
        }
        private void dpStartDate_SelectedDateChanged(object sender,SelectionChangedEventArgs e) {
            Datefilter();
        }

        private void dpEndDate_SelectedDateChanged(object sender,SelectionChangedEventArgs e) {
            Datefilter();
        }
    }
}
