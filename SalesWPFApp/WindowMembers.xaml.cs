
using BusinessObject;
using DataAccess.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SalesWPFApp {
    /// <summary>
    /// Interaction logic for WindowMembers.xaml
    /// </summary>
    public partial class WindowMembers :Window {

        ServiceProvider provider;
        List<Member> MyDataList;
        public WindowMembers() {
            InitializeComponent();

            this.DataContext = this;

            var services = new ServiceCollection();
            services.AddScoped<IMemberBusiness,MemberBusinessLogic>();
            provider = services.BuildServiceProvider();

        }

        private void Window_Loaded(object sender,RoutedEventArgs e) {


            LoadDataGrid();
        }

        private void TextBox_TextChanged(object sender,TextChangedEventArgs e) {

            if (!string.IsNullOrEmpty((sender as TextBox).Text.Trim())) {
                dgvMembers.ItemsSource = MyDataList.Where(x => x.Email.Contains((sender as TextBox).Text));
            } else {
                dgvMembers.ItemsSource = MyDataList;
            }
        }

        private void LoadDataGrid() {
            var memberBusinessLogic = provider.GetService<IMemberBusiness>();
            MyDataList = memberBusinessLogic.LoadAllMembers();
            if (MyDataList != null) {
                dgvMembers.ItemsSource = null;
                dgvMembers.ItemsSource = MyDataList;
            }

        }

        private void btnRemoveMember_Click(object sender,RoutedEventArgs e) {
            if (MessageBox.Show("Are you sure to delete this member ?","Confirm", MessageBoxButton.YesNo,MessageBoxImage.Question) != MessageBoxResult.No) {
                Member removeMember= (((sender as Button).DataContext) as Member);
                var memberBusinessLogic = provider.GetService<IMemberBusiness>();

                if (memberBusinessLogic.RemoveMember(removeMember)) {
                    MessageBox.Show("Remove Member Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                } else {
                    MessageBox.Show("Remove Member Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                LoadDataGrid();


            }
        }


        private void btnAddMember_Click(object sender,RoutedEventArgs e) {
            var memberBusinessLogic = provider.GetService<IMemberBusiness>();

            Member newMember = new Member() {
                City = tbCityAdd.Text,
                Email= tbEmailAdd.Text,
                CompanyName =tbCityAdd.Text,
                Country= tbCountryAdd.Text,
                Password = tbPasswordAdd.Password,
            };

            if (memberBusinessLogic.AddMember(newMember)) {
                MessageBox.Show("Add Member Successfully!","Success",MessageBoxButton.OK, MessageBoxImage.Information);
            } else {
                MessageBox.Show("Add Member Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            LoadDataGrid();
        }


        #region updateMember
        Member updateMember;
        private void popupUpdate_Opened(object sender,RoutedEventArgs e) {
            PopupBox popupBox = sender as PopupBox;
            updateMember = popupBox.DataContext as Member;

        }

        private void popupUpdate_Closed(object sender,RoutedEventArgs e) {
            updateMember = null;
        }

        private void btnUpdateMember_Click(object sender,RoutedEventArgs e) {
            var memberBusinessLogic = provider.GetService<IMemberBusiness>();

            if (updateMember != null) {
                if (memberBusinessLogic.UpdateMember(updateMember)) {
                    MessageBox.Show("Update Member Successfully!","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                } else {
                    MessageBox.Show("Update Member Fail!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
                LoadDataGrid();
            }
        }

        private void tbEmailUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateMember != null) {
                updateMember.Email = (sender as TextBox).Text;
            }
        }

        private void tbCompanyUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateMember != null) {
                updateMember.CompanyName = (sender as TextBox).Text;
            }
        }

        private void tbCityUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateMember != null) {
                updateMember.City = (sender as TextBox).Text;
            }
        }

        private void tbCountryUpdate_TextChanged(object sender,TextChangedEventArgs e) {
            if (updateMember != null) {
                updateMember.Country = (sender as TextBox).Text;
            }
        }

        private void tbPasswordUpdate_PasswordChanged(object sender,RoutedEventArgs e) {
            if (updateMember != null) {
                updateMember.Password = (sender as PasswordBox).Password;
            }
        }
        #endregion


    }
}
