using DataAccess.Dao;
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
using BusinessObject;
using Microsoft.Extensions.DependencyInjection;
using System.Formats.Asn1;
using System.Xml;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;

namespace SalesWPFApp {
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin :Window {
        ServiceProvider provider;

        public WindowLogin() {
            InitializeComponent();
            var services = new ServiceCollection();
            services.AddScoped<IMemberBusiness,MemberBusinessLogic>();
            provider = services.BuildServiceProvider();

        }

        private void btnLogin_Click(object sender,RoutedEventArgs e) {


            var memberBusinessLogic = provider.GetService<IMemberBusiness>();

            if ((bool)chkbLoginAsAdmin.IsChecked is false) {
                if (memberBusinessLogic.Login(tbEmail.Text,pbPassword.Password) is true) {
                    MainWindow mainWindow = new MainWindow(false);
                    this.Hide();
                    mainWindow.ShowDialog();

                } else {
                    MessageBox.Show("Wrong email or password!","Warning",MessageBoxButton.OK,MessageBoxImage.Error);
                };
            } else {
                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var email = MyConfig.GetSection("DefaultAccount")["Email"];
                var password = MyConfig.GetSection("DefaultAccount")["Password"];
                if (tbEmail.Text.Equals(email) && pbPassword.Password.Equals(password)) {
                    MainWindow mainWindow = new MainWindow(true);
                    this.Hide();

                    mainWindow.ShowDialog();



                } else {
                    MessageBox.Show("Wrong email or password!","Warning",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }

        }

        private void tbEmail_PreviewKeyDown(object sender,KeyEventArgs e){
            if (e.Key.Equals(Key.Space)) {
                e.Handled = true;
            }
        }

        private void pbPassword_PreviewKeyDown(object sender,KeyEventArgs e) {
            if (e.Key.Equals(Key.Space)) {
                e.Handled = true;
            }
        }


    }
}
