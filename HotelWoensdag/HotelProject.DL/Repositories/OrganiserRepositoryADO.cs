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
    public class OrganiserRepositoryADO : IOrganiserRepository
    {
        private string connectionString;
        public OrganiserRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddOrganiser(Organiser organiser)
        {
            try
            {
                string sql = "INSERT INTO Organizer(name,email,phone,address) output INSERTED.id VALUES(@name,@email,@phone,@address)";
                using (SqlConnection conn = new (connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = sql;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@name", organiser.Name);
                        cmd.Parameters.AddWithValue("@email", organiser.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", organiser.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", organiser.ContactInfo.Address.ToAddressLine());
                        int id = (int)cmd.ExecuteScalar();
                        transaction.Commit();
                    } catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            } catch (Exception ex)
            {
                throw new OrganiserRepositoryException("AddOrganiser", ex);
            }
        }

        public void DeleteOrganiser(Organiser organiser)
        {
            string sql = "DELETE FROM Organizer WHERE id=@id";
            using (SqlConnection conn = new (connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", organiser.Id);
                    cmd.ExecuteNonQuery();
                } catch (Exception ex)
                {
                    throw new OrganiserRepositoryException("DeleteOrganiser", ex);
                }
            }
        }

        public Organiser GetOrganiserById(int id)
        {
            string sql = "SELECT * FROM Organizer WHERE id=@id";
            using (SqlConnection conn = new (connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    Organiser o = new ((int)dr["id"], (string)dr["name"], new ((string)dr["email"], (string)dr["phone"], new ((string)dr["address"])));
                    dr.Close();
                    return o;
                } catch (Exception ex)
                {
                    throw new OrganiserRepositoryException("GetOrganiserById", ex);
                }
            }
        }

        public List<Organiser> GetOrganisers(string filter)
        {
            try
            {
                List<Organiser> Organisers = new ();
                string sql = "SELECT * FROM Organizer";
                using (SqlConnection conn = new (connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Organiser organiser = new (
                            (int)reader["id"],
                            (string)reader["name"],
                            new (
                                (string)reader["email"],
                                (string)reader["phone"],
                                new ((string)reader["address"])
                            )
                        );
                        Organisers.Add(organiser);
                    }
                    return Organisers;
                }
            } catch (Exception ex)
            {
                throw new OrganiserRepositoryException("GetOrganisers", ex);
            }
        }

        public void UpdateOrganiser(Organiser organiser)
        {
            string sql = "UPDATE Organizer SET name=@name, email=@email, phone=@phone WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", organiser.Id);
                    cmd.Parameters.AddWithValue("@name", organiser.Name);
                    cmd.Parameters.AddWithValue("@email", organiser.ContactInfo.Email);
                    cmd.Parameters.AddWithValue("@phone", organiser.ContactInfo.Phone);
                    cmd.ExecuteNonQuery();
                } catch (Exception ex)
                {
                    throw new OrganiserRepositoryException("UpdateOrganiser", ex);
                }
            }
        }
    }
}
