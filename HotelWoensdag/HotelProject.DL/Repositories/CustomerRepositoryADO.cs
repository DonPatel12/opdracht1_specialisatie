using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class CustomerRepositoryADO : ICustomerRepository
    {
        private string connectionString;

        public CustomerRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                string sql;
                if (string.IsNullOrEmpty(filter))
                {
                    sql = @"SELECT t1.id,t1.email,t1.name AS customername,t1.address,t1.phone,t2.name membername,t2.birthday
                            FROM customer t1 
                            LEFT JOIN (SELECT * 
                                        FROM member 
                                        WHERE status=1) t2 
                            ON t1.id=t2.customerId
                            WHERE t1.status=1";
                }
                else
                {
                    sql = @"select t1.id,t1.email,t1.name AS customername,t1.address,t1.phone,t2.name membername,t2.birthday from customer t1 left join (select * from member where status=1) t2 on t1.id=t2.customerId where t1.status=1 and (t1.id like @filter or t1.name like @filter or t1.email like @filter)";
                }
                using(SqlConnection conn = new (connectionString)) 
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    SqlDataReader reader=cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            int id= Convert.ToInt32(reader["ID"]);
                            if (!customers.ContainsKey(id)) //member toevoegen
                            {
                               customers.Add(id, new ((string)reader["customername"], (int)reader["id"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"]))));                              
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                customers[id].AddMember(new ((string)reader["membername"], ((DateTime)reader["birthday"])));
                            }                            
                        }
                    return customers.Values.ToList();
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("GetCustomer",ex);
            }
        }
        public void AddCustomer(Customer customer)
        {
            try
            {
                string sql = "INSERT INTO Customer(name,email,phone,address,status) output INSERTED.ID VALUES(@name,@email,@phone,@address,@status) ";
                using (SqlConnection conn = new (connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction=conn.BeginTransaction();
                    try
                    {
                        //write customer table
                        cmd.CommandText = sql;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.ContactInfo.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@status", 1);
                        int id=(int)cmd.ExecuteScalar();
                        //write members table
                        sql = "INSERT INTO member(name,birthday,customerid,status) VALUES(@name,@birthday,@customerid,@status) ";
                        cmd.CommandText=sql;
                        
                        foreach(Member member in customer.GetMembers())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@name",member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.BirthDay);
                            cmd.Parameters.AddWithValue("@customerid", id);
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("AddCustomer", ex);
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            string sql = "UPDATE Customer SET status=@status WHERE id=@id";
            using (SqlConnection conn = new (connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", customer.Id);
                    cmd.Parameters.AddWithValue("@status", 0);
                    cmd.ExecuteNonQuery();

                    sql = "UPDATE Members SET status=@status WHERE customerId=@customerId";
                    cmd.CommandText = sql;
                    foreach (Member member in customer.GetMembers())
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@customerId", customer.Id);
                        cmd.Parameters.AddWithValue("@status", 0);
                        cmd.ExecuteNonQuery();
                    }
                } catch (Exception ex)
                {
                    throw new CustomerRepositoryException("UpdateCustomer", ex);
                }
            }
        }

        public Customer GetCustomerById(int id)
        {
            string sql = "select c.id, c.name, c.email, c.phone, c.address, m.name AS membername, m.birthday from Customer c left join Member m ON c.id = m.customerId where c.id = @id";
            using (SqlConnection conn = new (connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader dr = cmd.ExecuteReader();

                    Customer c = null;

                    while (dr.Read())
                    {
                        if (c == null)
                        {
                            c = new ((string)dr["name"], (int)dr["id"], new ((string)dr["email"], (string)dr["phone"], new ((string)dr["address"])));
                        }

                        if (!dr.IsDBNull(dr.GetOrdinal("membername")))
                        {
                            c.AddMember(new ((string)dr["membername"], (DateTime)dr["birthday"]));
                        }
                    }

                    dr.Close();
                    return c;
                } catch (Exception ex)
                {
                    throw new CustomerRepositoryException("GetCustomerById", ex);
                }
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            string sql = "UPDATE Customer SET name=@name, email=@email, phone=@phone WHERE id=@id";
            using (SqlConnection conn = new (connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", customer.Id);
                    cmd.Parameters.AddWithValue("@name", customer.Name);
                    cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                    cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                    cmd.ExecuteNonQuery();
                } catch (Exception ex)
                {
                    throw new CustomerRepositoryException("UpdateCustomer", ex);
                }
            }
        }

        public bool CustomerExists(int id)
        {
            bool exists = false;

            string sql = "SELECT COUNT(*) FROM Customer WHERE id = @id";
            using (SqlConnection conn = new (connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@id", id);
                int count = (int)cmd.ExecuteScalar();

                exists = count > 0;
            }

            return exists;
        }
    }
}
