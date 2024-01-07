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
    /// Interaction logic for OrganiserWindow.xaml
    /// </summary>
    public partial class OrganiserWindow : Window
    {
        private OrganiserManager organiserManager;
        private ObservableCollection<OrganiserUI> organiserUIs = new ObservableCollection<OrganiserUI>();
        public OrganiserWindow()
        {
            InitializeComponent();
            organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
            organiserUIs = new ObservableCollection<OrganiserUI>(organiserManager.GetOrganisers(null).Select(x => new OrganiserUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString())));
            OrganiserDataGrid.ItemsSource = organiserUIs;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            OrganiserDataGrid.ItemsSource = new ObservableCollection<OrganiserUI>(organiserManager.GetOrganisers(SearchTextBox.Text).Select(x => new OrganiserUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString())));
        }

        private void MenuItemAddOrganiser_Click(object sender, RoutedEventArgs e)
        {
            AddOrganiserWindow w = new AddOrganiserWindow(false, null);
            if (w.ShowDialog() == true)
            {
                organiserUIs.Add(w.organiserUI);
                organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
                OrganiserDataGrid.ItemsSource = new ObservableCollection<OrganiserUI>(organiserManager.GetOrganisers(null).Select(x => new OrganiserUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString())));
                
            }
        }

        private void MenuItemDeleteOrganiser_Click(object sender, RoutedEventArgs e)
        {
            if (OrganiserDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Organiser not selected", "Delete");
            } else
            {
                OrganiserUI selectedOrganiserUI = (OrganiserUI)OrganiserDataGrid.SelectedItem;
                Organiser organiser = organiserManager.GetOrganiserById((int)selectedOrganiserUI.Id);
                organiserManager.DeleteOrganiser(organiser);
                organiserUIs.Remove(selectedOrganiserUI);
            }
        }

        private void MenuItemUpdateOrganiser_Click(object sender, RoutedEventArgs e)
        {
            if (OrganiserDataGrid.SelectedItem == null) MessageBox.Show("Organiser not selected", "Update");
            else
            {
                AddOrganiserWindow w = new AddOrganiserWindow(true, (OrganiserUI)OrganiserDataGrid.SelectedItem);
                w.ShowDialog();
            }
        }

        private void MenuItemAddActivity_Click(object sender, RoutedEventArgs e)
        {
            if (OrganiserDataGrid.SelectedItem == null) MessageBox.Show("Organiser not selected", "Update");
            else
            {
                OrganiserUI selectedOrganiser = (OrganiserUI)OrganiserDataGrid.SelectedItem;
                AddActivityWindow w = new AddActivityWindow(selectedOrganiser, null);
                w.ShowDialog();
            }
        }

        private void MenuItemViewActivity_Click(object sender, RoutedEventArgs e)
        {
            if (OrganiserDataGrid.SelectedItem == null) MessageBox.Show("Organiser not selected", "ViewActivities");
            else
            {
                //OrganiserUI selectedOrganiser = (OrganiserUI)OrganiserDataGrid.SelectedItem;

                ActivityWindow w = new (null);

                w.ShowDialog();
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow s = new ();
            s.Show();
            Close();
        }
    }
}
