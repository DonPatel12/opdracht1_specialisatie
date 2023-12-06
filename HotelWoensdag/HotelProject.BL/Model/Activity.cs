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
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public int MaxParticipants { get; set; }
        public int Price { get; set; }
        public List<Customer> Participants { get; set; }
        
        public Activity(string name, string description, DateTime eventDateTime, int maxParticipants, int price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ActivityException("Name cannot be null or whitespace.");

            if (maxParticipants <= 0)
                throw new ActivityException("MaxParticipants must be greater than 0.");

            Name = name;
            Description = description;
            EventDateTime = eventDateTime;
            MaxParticipants = maxParticipants;
            Price = price;
            Participants = new List<Customer>();
        }

        public void AddParticipant(Customer customer)
        {
            if (customer == null)
                throw new ActivityException(nameof(customer));

            if (Participants.Count >= MaxParticipants)
                throw new ActivityException("Cannot add more participants, activity is full.");

            Participants.Add(customer);
        }

        public bool IsFull()
        {
            return Participants.Count >= MaxParticipants;
        }

        
        //public void RemoveParticipant(Member member)
        //{
        //    if (member == null)
        //        throw new ActivityException(nameof(member));
        //
        //    Participants.Remove(member);
        //}
    }
}
