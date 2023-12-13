using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Registration 
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                {
                    throw new RegistrationException("Id is invalid!");
                }
                _id = value;
            }
        }

        public Registration(int id)
        {
            Id = id;
        }

        public Registration() { }

        private Activity _activity;
        public Activity Activity
        {
            get => _activity;
            set
            {
                if (value == null)
                {
                    throw new RegistrationException("Activity is null!");
                }
                _activity = value;
            }
        }

        public decimal Cost(List<Member> members)
        {
            return Activity.Cost(Member.GetMembers());
        }

    }
}
