using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
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
    /// Interaction logic for AddOrganiserWindow.xaml
    /// </summary>
    public partial class AddOrganiserWindow : Window
    {
        public OrganiserUI organiserUI;
        private bool isUpdate;
        private OrganiserManager organiserManager;
        public AddOrganiserWindow(bool isUpdate, OrganiserUI organiserUI)
        {
            InitializeComponent();
            organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
            this.organiserUI = organiserUI;
            this.isUpdate = isUpdate;
            if (organiserUI != null)
            {
                IdTextBox.Text = organiserUI.Id.ToString();
                NameTextBox.Text = organiserUI.Name;
                EmailTextBox.Text = organiserUI.Email;
                PhoneTextBox.Text = organiserUI.Phone;
            }
            IdTextBox.IsEnabled = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                organiserUI.Name = NameTextBox.Text;
                organiserUI.Email = EmailTextBox.Text;
                organiserUI.Phone = PhoneTextBox.Text;

                var o = organiserManager.GetOrganiserById((int)organiserUI.Id);

                Address a = new Address(o.ContactInfo.Address.Municipality, o.ContactInfo.Address.ZipCode, o.ContactInfo.Address.HouseNumber, o.ContactInfo.Address.Street);

                ContactInfo contactinfo = new ContactInfo(organiserUI.Email, organiserUI.Phone, a);

                Organiser organiser = new Organiser(organiserUI.Name, (int)organiserUI.Id, contactinfo);

                organiserManager.UpdateOrganiser(organiser);
            } else
            {
                Organiser o = new Organiser(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));
                organiserManager.AddOrganiser(o);
                organiserUI = new OrganiserUI(o.Id, o.Name, o.ContactInfo.Email, o.ContactInfo.Phone, o.ContactInfo.Address.ToString());
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
