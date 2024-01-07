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
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private ActivityManager activityManager;
        private ObservableCollection<ActivityUI> activityUIs = new ObservableCollection<ActivityUI>();
        private CustomerUI customerUI;
        public ActivityWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            this.customerUI = customerUI;

            InitializeActivitiesDataGrid();
        }

        private void InitializeActivitiesDataGrid()
        {
            var activities = activityManager.GetAllActivities()
                .Select(x => new ActivityUI(x.Id, x.Name, x.Description, x.EventDateTime, x.Duration, x.Location, x.NumberOfSpots, x.PriceInfo.AdultCost, x.PriceInfo.ChildCost, x.PriceInfo.AdultAge, x.PriceInfo.Discount));

            
            List<ActivityUI> activityF = new List<ActivityUI>();

            //// Filter out activities that are in the past
            foreach (ActivityUI activity in activities)
            {
                if (activity.Date >= DateTime.Now)
                {
                    activityF.Add(activity);
                }
            }

            ActivityDataGrid.ItemsSource = new ObservableCollection<ActivityUI>(activityF);
        }

        private void OpenRegistrationWindow()
        {
            var activityUI = (ActivityUI)ActivityDataGrid.SelectedItem;
            var registrateWindow = new RegistrationWindow(activityUI, customerUI);
            registrateWindow.Show();
            Close();
        }

        private void RegistrationCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityDataGrid.SelectedItem is null)
            {
                MessageBox.Show("Activity not selected");
            } else
            {
                if (customerUI is null)
                {
                    MessageBox.Show("Customer not selected", "RegistrateCustomer");
                } else
                {
                    OpenRegistrationWindow();
                }
            }
        }

        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ActivitiesDataGrid.ItemsSource = new ObservableCollection<OrganiserUI>(activityManager.GetOrganisers(SearchTextBox.Text).Select(x => new OrganiserUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToString())));
        //}

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow s = new ();
            s.Show();
            Close();
        }
    }
}
