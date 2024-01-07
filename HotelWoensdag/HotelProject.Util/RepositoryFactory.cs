using HotelProject.BL.Interfaces;
using HotelProject.DL.Repositories;
using System.Configuration;

namespace HotelProject.Util
{
    public static class RepositoryFactory
    {
        public static IActivityRepository ActivityRepository { get { return new ActivityRepositoryADO(ConfigurationManager.ConnectionStrings["HotelWoensdag"].ConnectionString); } }
        public static ICustomerRepository CustomerRepository { get { return new CustomerRepositoryADO(ConfigurationManager.ConnectionStrings["HotelWoensdag"].ConnectionString); } }
        public static IOrganiserRepository OrganiserRepository { get { return new OrganiserRepositoryADO(ConfigurationManager.ConnectionStrings["HotelWoensdag"].ConnectionString); } }
        public static IRegistrationRepository RegistrationRepository { get { return new RegistrationRepositoryADO(ConfigurationManager.ConnectionStrings["HotelWoensdag"].ConnectionString); } }

        
    }
}
