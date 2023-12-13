using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
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

namespace HotelProject.UI.CustomerWPF
{
    /// <summary>
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        private CustomerManager customerManager;
        public CustomerUI customerUI;
        private bool isUpdate;
        public AddCustomerWindow(bool isUpdate, CustomerUI customerUI)
        {
            InitializeComponent();
            this.customerUI = customerUI;
            this.isUpdate = isUpdate;
            if (customerUI != null)
            {
                IdTextBox.Text = customerUI.Id.ToString();
                NameTextBox.Text = customerUI.Name;
                EmailTextBox.Text = customerUI.Email;
                PhoneTextBox.Text = customerUI.Phone;
                //CityTextBox.Text=customerUI.address
            }
            IdTextBox.IsEnabled = false;
        }
        
        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                customerUI.Name = NameTextBox.Text;
                customerUI.Email = EmailTextBox.Text;
                customerUI.Phone = PhoneTextBox.Text;
                var c = customerManager.GetCustomerById((int)customerUI.Id);

                Address a = new Address(c.ContactInfo.Address.Municipality, c.ContactInfo.Address.ZipCode, c.ContactInfo.Address.HouseNumber, c.ContactInfo.Address.Street);

                ContactInfo contactinfo = new ContactInfo(customerUI.Email, customerUI.Phone, a);

                Customer customer = new Customer(customerUI.Name, (int)customerUI.Id, contactinfo);

                customerManager.UpdateCustomer(customer);
            } else
            {
                Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));

                customerManager.AddCustomer(c);

                customerUI = new CustomerUI(c.Id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count);
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
