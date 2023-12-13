using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IOrganiserRepository
    {
        List<Organiser> GetOrganisers(string filter);
        void AddOrganiser(Organiser organiser);
        void DeleteOrganiser(Organiser organiser);
        Organiser GetOrganiserById(int id);
        void UpdateOrganiser(Organiser organiser);
    }
}
