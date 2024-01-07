using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class RegistrationRepositoryADO : IRegistrationRepository
    {
        private string connectionString;
        private ICustomerRepository _customerRepository;
        private IActivityRepository _activityRepository;

        public RegistrationRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public RegistrationRepositoryADO(ICustomerRepository customerRepository, IActivityRepository activityRepository)
        {
            
            _customerRepository = customerRepository;
            _activityRepository = activityRepository;
        }

        public void AddRegistration(Registration registration)
        {
            string insertRegistrationSQL = @"
        INSERT INTO Registration(activityId, customerId,  cost) 
        OUTPUT INSERTED.registrationId 
        VALUES(@activityId, @customerId,  @cost)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Prepare and execute the SQL command for inserting the registration
                            using (SqlCommand cmd = new SqlCommand(insertRegistrationSQL, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@activityId", registration.Activity.Id);
                                cmd.Parameters.AddWithValue("@customerId", registration.Customer.Id);
                                cmd.Parameters.AddWithValue("@cost", registration.Cost());

                                // Execute the command and get the newly generated registration ID
                                int registrationId = (int)cmd.ExecuteScalar();

                                // Optional: Create a new registration object if needed in your application
                                Customer customer = _customerRepository.GetCustomerById(registration.Customer.Id);
                                Activity activity = _activityRepository.GetActivityById(registration.Activity.Id);
                                Registration newRegistration = new Registration( activity, customer, registration.Cost());
                            }

                            transaction.Commit();
                        } catch
                        {
                            transaction.Rollback();
                            throw; // Re-throw the exception to maintain the stack trace
                        }
                    }
                }
            } catch (Exception ex)
            {
                // Consider logging the exception details here
                throw new RegistrationRepositoryException("Error during registration.", ex);
            }
        }


        //private int ExecuteInsertRegistration(SqlConnection conn, SqlTransaction transaction, string sql, Registration registration)
        //{
        //    using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
        //    {
        //        cmd.Parameters.AddWithValue("@activityId", registration.Activity.Id);
        //        cmd.Parameters.AddWithValue("@customerId", registration.Customer.Id);
        //        cmd.Parameters.AddWithValue("@memberCount", registration.GetMembers().Count);
        //        cmd.Parameters.AddWithValue("@cost", registration.Cost());

        //        return (int)cmd.ExecuteScalar();
        //    }
        //}


        public bool CheckIfRegistered(string name, int registrationid)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT 1 FROM Registration WHERE memberName = @Name AND registrationId = @RegistrationId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@RegistrationId", registrationid);

                        return cmd.ExecuteScalar() != null;
                    }
                }
            } catch (Exception ex)
            {
                throw new RegistrationRepositoryException("MemberRegistrated", ex);
            }
        }

        public List<Registration> GetRegistrationByActivityId(int activityid)
        {
            try
            {
                List<Registration> registrations = new List<Registration>();

                string query = "SELECT * FROM Registration WHERE activityId = @activityId";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@activityId", activityid);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int customerId = (int)dr["customerId"];
                            Customer customerCorrect = _customerRepository.GetCustomerById(customerId);

                            Activity activity = _activityRepository.GetActivityById(activityid);

                            Registration registration = new Registration(
                                (int)dr["registrationId"],
                                activity,
                                customerCorrect,
                                (decimal)dr["cost"]
                            );

                            registrations.Add(registration);
                        }
                    }

                    return registrations;
                }
            } catch (Exception ex)
            {
                throw new RegistrationRepositoryException("GetRegistrationByActivityId", ex);
            }
        }
    }
}
