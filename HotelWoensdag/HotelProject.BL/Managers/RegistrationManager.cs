using HotelProject.BL.Exceptions;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HotelProject.BL.Managers
{
    public class RegistrationManager
    {
        private IRegistrationRepository _registrationRepository;

        public RegistrationManager(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public void AddRegistration(Registration registration)
        {
            try
            {
                _registrationRepository.AddRegistration(registration);
            }
            catch (Exception ex)
            {
                throw new RegistrationManagerException("Error adding registration", ex);
            }
        }


        public List<Registration> GetRegistrationByActivityId(int activityid)
        {
            try
            {
                return _registrationRepository.GetRegistrationByActivityId(activityid);
            }
            catch (Exception ex)
            {
                throw new RegistrationManagerException("Error getting all registrations", ex);
            }

        }

        public bool CheckIfRegistered(string name, int registrationid)
        {
            try
            {
                return _registrationRepository.CheckIfRegistered(name, registrationid);
            }
            catch (Exception ex)
            {
                throw new RegistrationManagerException("Error checking if registered", ex);
            }
        }
    }
}
