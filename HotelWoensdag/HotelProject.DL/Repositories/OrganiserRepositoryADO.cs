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
                string SQL = "INSERT INTO Organiser(name,email,phone,address) output INSERTED.id VALUES(@name,@email,@phone,@address)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = SQL;
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
            string sql = "DELETE FROM Organiser WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
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
            string sql = "SELECT * FROM Organiser WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    IDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    Organiser o = new Organiser((string)dr["name"], (int)dr["id"], new ContactInfo((string)dr["email"], (string)dr["phone"], new Address((string)dr["address"])));
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
                List<Organiser> Organisers = new List<Organiser>();
                string sql = "SELECT * FROM Organiser";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Organiser organiser = new Organiser(
                            (string)reader["name"],
                            (int)reader["id"],
                            new ContactInfo(
                                (string)reader["email"],
                                (string)reader["phone"],
                                new Address((string)reader["address"])
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
            string sql = "UPDATE Organiser SET name=@name, email=@email, phone=@phone WHERE id=@id";
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
