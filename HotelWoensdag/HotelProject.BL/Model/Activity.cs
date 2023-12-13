using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model
{
    public class Activity
    {

        public Activity(int id, string name, string description, DateTime eventDateTime, string location, int numberOfSpots, PriceInfo priceInfo, int organiserId)
        {
            Id = id;
            Name = name;
            Description = description;
            EventDateTime = eventDateTime;
            Location = location;
            NumberOfSpots = numberOfSpots;
            PriceInfo = priceInfo;
            
            OrganiserId = organiserId;
        }

        public Activity(string name, string description, DateTime eventDateTime, string location, int numberOfSpots, PriceInfo priceInfo, int organiserId)
        {
            Name = name;
            Description = description;
            EventDateTime = eventDateTime;
            Location = location;
            NumberOfSpots = numberOfSpots;
            PriceInfo = priceInfo;
            OrganiserId = organiserId;
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                {
                    throw new ActivityException("Id is invalid!");
                }
                _id = value;
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ActivityException("Name is invalid!");
                }
                _name = value;
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ActivityException("Description is invalid!");
                }
                _description = value;
            }
        }

        private DateTime _eventDateTime;
        public DateTime EventDateTime
        {
            get => _eventDateTime;
            set
            {
                if (value <= DateTime.Now)
                {
                    throw new ActivityException("Event datetime is invalid!");
                }
                _eventDateTime = value;
            }
        }

        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ActivityException("Location is invalid!");
                } else
                {
                    _location = value;
                }
            }
        }

        private int _numberOfSpots;
        public int NumberOfSpots
        {
            get => _numberOfSpots;
            set
            {
                if (value <= 0)
                {
                    throw new ActivityException("Number of spots is invalid!");
                }
                _numberOfSpots = value;
            }
        }
        

        private PriceInfo _priceInfo;
        public PriceInfo PriceInfo
        {
            get => _priceInfo;
            set
            {
                if (value == null)
                {
                    throw new ActivityException("Price info is null!");
                }
                _priceInfo = value;
            }
        }

        //private List<Customer> _participants;
        //public List<Customer> Participants
        //{
        //    get => _participants;
        //    set
        //    {
        //        if (value == null)
        //        {
        //            throw new ActivityException("Participants is null!");
        //        }
        //        _participants = value;
        //    }
        //}

        private int _organiserId;
        public int OrganiserId
        {
            get => _organiserId;
            set
            {
                if (value <= 0)
                {
                    throw new ActivityException("Organiser id is invalid!");
                }
                _organiserId = value;
            }
        }

        public decimal Cost(List<Member> members)
        {
            return PriceInfo.Cost(members);
        }

        
        //public Activity(string name, string description, DateTime eventDateTime, int maxParticipants, int price)
        //{

        //    Name = name;
        //    Description = description;
        //    EventDateTime = eventDateTime;
        //    MaxParticipants = maxParticipants;
        //    Price = price;
        //    Participants = new List<Customer>();
        //}

        //public void AddParticipant(Customer customer)
        //{
        //    if (customer == null)
        //        throw new ActivityException(nameof(customer));

        //    if (Participants.Count >= MaxParticipants)
        //        throw new ActivityException("Cannot add more participants, activity is full.");

        //    Participants.Add(customer);
        //}

        //public bool IsFull()
        //{
        //    return Participants.Count >= MaxParticipants;
        //}

        
        ////public void RemoveParticipant(Member member)
        ////{
        ////    if (member == null)
        ////        throw new ActivityException(nameof(member));
        ////
        ////    Participants.Remove(member);
        ////}
    }
}
