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
    /// Interaction logic for AddActivityWindow.xaml
    /// </summary>
    public partial class AddActivityWindow : Window
    {
        public OrganiserUI organiserUI;
        public ActivityUI activityUI;
        private ActivityManager activityManager;
        public AddActivityWindow(OrganiserUI organiserUI, ActivityUI activityUI)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            this.organiserUI = organiserUI;
            this.activityUI = activityUI;
            IdTextBox.IsEnabled = false;
        }

       

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Decimal.TryParse(AdultCostTextBox.Text, out decimal adultCost) ||
                 !Decimal.TryParse(ChildCostTextBox.Text, out decimal childCost) ||
                 !Int32.TryParse(DiscountTextBox.Text, out int discount) ||
                 !Int32.TryParse(AdultAgeTextBox.Text, out int adultAge) ||
                 !Int32.TryParse(AvailableSpotsTextBox.Text, out int availableSpots) ||
                 !DateTime.TryParse(DateTextBox.Text, out DateTime date))
            {
                MessageBox.Show("Please enter valid values in all fields.");
                return;
            }

            int organiserId = organiserUI.Id ?? 0; // Assuming OrganiserUI has an Id property

            PriceInfo priceInfo = new PriceInfo(adultCost, childCost, discount, adultAge);

            Activity activity = new Activity(NameTextBox.Text, DescriptionTextBox.Text, date, LocationTextBox.Text, availableSpots, priceInfo, organiserId);

            try
            {
                activityManager.AddActivity(activity);
                MessageBox.Show("Activity added successfully.");
                DialogResult = true;
                Close();
            } catch (Exception ex)
            {
                MessageBox.Show($"Error adding activity: {ex.Message}");
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
