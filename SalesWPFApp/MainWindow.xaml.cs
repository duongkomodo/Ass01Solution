using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalesWPFApp {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :Window {
        public MainWindow(bool isAdmin) {
            InitializeComponent();
            if (!isAdmin) {
                gridAdmin.Visibility = Visibility.Collapsed;
            } else {
                gridMember.Visibility = Visibility.Collapsed;
            }
        }

        private void btnProductMenu_Click(object sender,RoutedEventArgs e) {
            WindowProducts windowProducts = new WindowProducts();
            this.Hide();
           windowProducts.ShowDialog();
            this.Show();

        }

        private void btnOrderMenu_Click(object sender,RoutedEventArgs e) {
            WindowOrders windowOrders = new WindowOrders();
            this.Hide();
            windowOrders.ShowDialog();
            this.Show();
        }

        private void btnMemberMenu_Click(object sender,RoutedEventArgs e) {
            WindowMembers windowMembers = new WindowMembers();
            this.Hide();
            windowMembers.ShowDialog();
            this.Show();
        }
    }
}
