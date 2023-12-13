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
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }
        private void Click_Customer(object sender, RoutedEventArgs e)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Show();

            Close();
        }

        private void Click_Organiser(object sender, RoutedEventArgs e)
        {
            OrganiserWindow organiserWindow = new OrganiserWindow();
            organiserWindow.Show();

            Close();
        }

        private void Click_Activity(object sender, RoutedEventArgs e)
        {
            ActivityWindow activitiesWindow = new ActivityWindow(null);
            activitiesWindow.Show();

            Close();
        }
    }
}
