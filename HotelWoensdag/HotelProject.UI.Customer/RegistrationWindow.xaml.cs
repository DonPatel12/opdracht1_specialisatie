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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        private ActivityManager _activityManager;
        private CustomerManager _customerManager;
        private RegistrationManager _registrationManager;
        private ActivityUI activityUI;
        private CustomerUI customerUI;

        private List<Member> selectedmembers;
        public RegistrationWindow(ActivityUI activityUI, CustomerUI customerUI)
        {
            InitializeComponent();
            _activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            _customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            _registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);

            this.activityUI = activityUI;
            this.customerUI = customerUI;

            DataContext = activityUI;

            int CustomerId = (int)customerUI.Id;
            Customer customer = _customerManager.GetCustomerById(CustomerId);

            List<Member> members = customer.GetMembers().ToList();

            MemberDataGrid.ItemsSource = members;
            MemberDataGrid.SelectionChanged += MemberDataGrid_SelectionChanged;
        }

        private void MemberDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedmembers = MemberDataGrid.SelectedItems.Cast<Member>().ToList();

            UpdateRegistrationCost();
        }

        private void UpdateRegistrationCost()
        {
            if (_registrationManager is not null && activityUI is not null)
            {
                Customer customer = _customerManager.GetCustomerById(customerUI.Id ?? 0);
                PriceInfo priceInfo = new (activityUI.AdultCost, activityUI.ChildCost, activityUI.Discount, activityUI.AdultAge);
                Activity activity = new (activityUI.Name, activityUI.Description, activityUI.Date, activityUI.Duration, activityUI.Location, activityUI.AvailableSpots, priceInfo);

                Registration registration = new (activity, customer);

                foreach (Member member in selectedmembers)
                {
                    registration.AddMember(member);
                }

                decimal cost = registration.Cost();

                CostTextBlock.Text = $"         {cost:C}";
            }
        }

        private void AddRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = _customerManager.GetCustomerById(customerUI.Id ?? 0);
            PriceInfo priceInfo = new (activityUI.AdultCost, activityUI.ChildCost, activityUI.Discount, activityUI.AdultAge);
            Activity activity = new (activityUI.Name, activityUI.Description, activityUI.Date, activityUI.Duration, activityUI.Location, activityUI.AvailableSpots, priceInfo);

            Registration registration = new (activity, customer);

            var registrations = _registrationManager.GetRegistrationByActivityId(activity.Id);

            if (selectedmembers is null)
            {
                MessageBox.Show("No members selected!");
                return;
            } else
            {
                foreach (Member member in selectedmembers)
                {
                    if (registrations.Count() == 0)
                    {
                        registration.AddMember(member);
                    } else
                    {
                        foreach (Registration r in registrations)
                        {
                            var exsit = _registrationManager.CheckIfRegistered(member.Name, r.Id);
                            if (exsit == true)
                            {
                                MessageBox.Show($"Member {member.Name} is already registrated to this activity");
                            } else
                            {
                                registration.AddMember(member);
                            }
                        }
                    }
                }
            }

            if (selectedmembers.Count > activityUI.AvailableSpots)
            {
                MessageBox.Show("Too many people: avalaibleSpots overrided!");
                return;
            }

            _registrationManager.AddRegistration(registration);

            int newAvalaibleSpots = activityUI.AvailableSpots - selectedmembers.Count;
            activity.NumberOfSpots = newAvalaibleSpots;
            _activityManager.UpdateActivity(activity);

            ActivityWindow a = new (null);
            a.Show();
            Close();
        }






        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            StartWindow s = new();
            s.Show();
            Close();
        }
    }
}
