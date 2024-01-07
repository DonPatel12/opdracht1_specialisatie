using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IRegistrationRepository
    {
        List<Registration> GetRegistrationByActivityId(int activityid);
        void AddRegistration(Registration registration);
        bool CheckIfRegistered(string name, int registrationid);
    }
}
