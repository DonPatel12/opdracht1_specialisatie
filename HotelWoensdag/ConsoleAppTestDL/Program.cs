using HotelProject.BL.Model;
using HotelProject.DL.Repositories;

namespace ConsoleAppTestDL
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=DONPATEL\\sqlEXPRESS;Initial Catalog=HotelWoensdag;Integrated Security=True";
            CustomerRepositoryADO repo = new (connectionString);
            //var x=repo.GetCustomers("jo");
            Customer customer = new ("Fred", new ("fred@gmail","0123456789",new ("gent","9000","12f","kerkstraat")));
            //customer.AddMember(new ("Freddy", new DateOnly(1989,8,8)));
            //customer.AddMember(new ("Gino", new DateOnly(1987,5,8)));
            repo.AddCustomer(customer);

        }
    }
}
