using HotelProject.BL.Exceptions;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Managers
{
    public class OrganiserManager 
    {
        private IOrganiserRepository _organiserRepository;
        public OrganiserManager(IOrganiserRepository organiserRepository)
        {
            _organiserRepository = organiserRepository;
        }

        public void AddOrganiser(Organiser organiser)
        {
            try
            {
                _organiserRepository.AddOrganiser(organiser);
            }
            catch (Exception ex)
            {
                throw new OrganiserException("AddOrganiser", ex);
            }
        }

        public void DeleteOrganiser(Organiser organiser)
        {
            try
            {
                _organiserRepository.DeleteOrganiser(organiser);
            }
            catch (Exception ex)
            {
                throw new OrganiserException("DeleteOrganiser", ex);
            }
        }

        public List<Organiser> GetOrganisers(string filter)
        {
            try
            {
                return _organiserRepository.GetOrganisers(filter);
            }
            catch (Exception ex)
            {
                throw new OrganiserException("GetOrganisers", ex);
            }
        }

        public Organiser GetOrganiserById(int id)
        {
            try
            {
                return _organiserRepository.GetOrganiserById(id);
            }
            catch (Exception ex)
            {
                throw new OrganiserException("GetOrganiserById", ex);
            }
        }

        public void UpdateOrganiser(Organiser organiser)
        {
            try
            {
                _organiserRepository.UpdateOrganiser(organiser);
            }
            catch (Exception ex)
            {
                throw new OrganiserException("UpdateOrganiser", ex);
            }


        }
    }
}
