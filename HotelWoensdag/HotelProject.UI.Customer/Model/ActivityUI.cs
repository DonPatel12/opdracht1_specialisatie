using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.CustomerWPF.Model
{
    public class ActivityUI : INotifyPropertyChanged
    {
        

        public ActivityUI(int? id, string name, string description, DateTime date, string location, int availableSpots, decimal adultCost, decimal childCost, decimal discount, int organiserId)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.date = date;
            this.location = location;
            this.availableSpots = availableSpots;
            this.adultCost = adultCost;
            this.childCost = childCost;
            this.discount = discount;
            this.organiserId = organiserId;
        }

        private int? id;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged(); } }

        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }

        private string description;
        public string Description { get { return description; } set { description = value; OnPropertyChanged(); } }

        private DateTime date;
        public DateTime Date { get { return date; } set { date = value; OnPropertyChanged(); } }
        private string location;
        public string Location { get { return location; } set { location = value; OnPropertyChanged(); } }

        private int availableSpots;
        public int AvailableSpots { get { return availableSpots; } set { availableSpots = value; OnPropertyChanged(); } }

        private decimal adultCost;
        public decimal AdultCost { get { return adultCost; } set { adultCost = value; OnPropertyChanged(); } }

        private decimal childCost;
        public decimal ChildCost { get { return childCost; } set { childCost = value; OnPropertyChanged(); } }

        private decimal discount;
        public decimal Discount { get { return discount; } set { discount = value; OnPropertyChanged(); } }

        private decimal organiserId;
        public decimal OrganiserId { get { return organiserId; } set { organiserId = value; OnPropertyChanged(); } }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
