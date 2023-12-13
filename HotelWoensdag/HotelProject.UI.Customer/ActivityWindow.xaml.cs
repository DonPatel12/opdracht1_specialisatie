using HotelProject.BL.Managers;
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
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private ActivityManager activityManager;
        private ObservableCollection<ActivityUI> activitiesUIs = new ObservableCollection<ActivityUI>();
        private OrganiserUI organiserUI;
        public ActivityWindow(OrganiserUI organiserUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            this.organiserUI = organiserUI;

            if (organiserUI == null)
            {
                activitiesUIs = new ObservableCollection<ActivityUI>(activityManager.GetAllActivities().Select(x => new ActivityUI(x.Id, x.Name, x.Description,  x.EventDateTime, x.Location, x.NumberOfSpots, x.PriceInfo.AdultCost, x.PriceInfo.ChildCost, x.PriceInfo.Discount, x.OrganiserId)));
            } else
            {
                activitiesUIs = new ObservableCollection<ActivityUI>(activityManager.GetAllActivities().Where(x => x.OrganiserId == organiserUI.Id).Select(x => new ActivityUI(x.Id, x.Name, x.Description, x.EventDateTime, x.Location, x.NumberOfSpots, x.PriceInfo.AdultCost, x.PriceInfo.ChildCost, x.PriceInfo.Discount, x.OrganiserId)));
            }

            ActivitiesDataGrid.ItemsSource = activitiesUIs;
        }
        

        

        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ActivitiesDataGrid.ItemsSource = new ObservableCollection<OrganiserUI>(activityManager.GetOrganisers(SearchTextBox.Text).Select(x => new OrganiserUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString())));
        //}

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow s = new StartWindow();
            s.Show();
            Close();
        }
    }
}
