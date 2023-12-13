using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Organiser
    {
        private string _name;
        public string Name { get { return _name; } set { if (string.IsNullOrWhiteSpace(value)) throw new OrganiserException("name is empty"); _name = value; } }
        private int _id;
        public int Id { get { return _id; } set { if (value <= 0) throw new OrganiserException("invalid id"); _id = value; } }
        private ContactInfo _contactInfo;
        public ContactInfo ContactInfo { get { return _contactInfo; } set { if (value == null) throw new OrganiserException("contact info is null"); _contactInfo = value; } }
        
        public Organiser(string name, int id, ContactInfo contactInfo)
        {
            _name = name;
            _id = id;
            _contactInfo = contactInfo;
        }

        public Organiser(string name, ContactInfo contactInfo)
        {
            _name = name;
            _contactInfo = contactInfo;
        }
    }
}
