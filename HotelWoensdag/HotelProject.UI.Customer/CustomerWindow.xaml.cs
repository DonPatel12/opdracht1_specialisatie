using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HotelProject.UI.CustomerWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private CustomerManager customerManager;
        private ObservableCollection<CustomerUI> customersUIs;
        public CustomerWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customersUIs = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString(), x.GetMembers().Count)));
            CustomerDataGrid.ItemsSource = customersUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerDataGrid.ItemsSource = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString(), x.GetMembers().Count)));
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddCustomerWindow w = new(false, null);
            if (w.ShowDialog() == true)
                customersUIs.Add(w.customerUI);
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            CustomerDataGrid.ItemsSource = new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString(), x.GetMembers().Count)));

        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "Delete");
            } else
            {
                CustomerUI selectedCustomerUI = (CustomerUI)CustomerDataGrid.SelectedItem;
                Customer customer = customerManager.GetCustomerById((int)selectedCustomerUI.Id);
                customerManager.DeleteCustomer(customer);
                customersUIs.Remove(selectedCustomerUI);
            }
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("Customer not selected", "Update");
            else
            {
                AddCustomerWindow w = new(true, (CustomerUI)CustomerDataGrid.SelectedItem);
                w.ShowDialog();
            }
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow s = new StartWindow();
            s.Show();
            Close();
        }

        private void MenuItemAddtoActivity_Click(object sender, EventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Customer not selected", "ViewActivity - null");
            } else
            {
                CustomerUI selectedCustomer = (CustomerUI)CustomerDataGrid.SelectedItem;
                ActivityWindow w = new (selectedCustomer);
                w.Show();
                Close();
            }
        }
    }
}
