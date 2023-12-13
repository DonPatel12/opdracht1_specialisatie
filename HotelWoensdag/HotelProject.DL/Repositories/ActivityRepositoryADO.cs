using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System.Data.SqlClient;

namespace HotelProject.DL.Repositories
{
    public class ActivityRepositoryADO : IActivityRepository
    {
        private string connectionString;

        public ActivityRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddActivity(Activity activity)
        {
            try
            {
                string sql = "INSERT INTO Activity( name, description, date, location, availableSpots, adultCost, childCost, discount, organiserId) OUTPUT INSERTED.id VALUES( @name, @description, @date, @location, @availableSpots, @adultCost, @childCost, @discount, @organiserId)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.CommandText = sql;
                        cmd.Transaction = transaction;
                        
                        cmd.Parameters.AddWithValue("@name", activity.Name);
                        cmd.Parameters.AddWithValue("@description", activity.Description);
                        cmd.Parameters.AddWithValue("@datetime", activity.EventDateTime);
                        cmd.Parameters.AddWithValue("@location", activity.Location);
                        cmd.Parameters.AddWithValue("@availableSpots", activity.NumberOfSpots);
                        cmd.Parameters.AddWithValue("@adultCost", activity.PriceInfo.AdultCost);
                        cmd.Parameters.AddWithValue("@childCost", activity.PriceInfo.ChildCost);
                        cmd.Parameters.AddWithValue("@discount", activity.PriceInfo.Discount);
                        cmd.Parameters.AddWithValue("@organiserId", activity.OrganiserId);
                        int id = (int)cmd.ExecuteScalar();
                        transaction.Commit();

                    } catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            } catch (Exception ex)
            {
                throw new ActivityRepositoryException("AddActivity", ex);
            }
        }

        public void DeleteActivity(Activity activity)
        {
            string sql = "DELETE FROM Activity WHERE id=@id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", activity.Id);
                    cmd.ExecuteNonQuery();
                } catch (Exception ex)
                {
                    throw new ActivityRepositoryException("DeleteActivity", ex);
                }
            }
        }

        public List<Activity> GetAllActivities()
        {
            try
            {
                List<Activity> activities = new List<Activity>();
                string sql = "SELECT * FROM Activity";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Activity activity = new  Activity(
                            (int)reader["id"],
                            (string)reader["name"], 
                            (string)reader["description"],
                            (DateTime)reader["eventdatetime"],
                            (string)reader["location"], 
                            (int)reader["availableSpots"],
                            new PriceInfo((decimal)reader["adultCost"], (decimal)reader["childCost"], (decimal)reader["discount"], 18),
                            (int)reader["organiserId"]
                        );
                        activities.Add(activity);
                    }
                    return activities;
                }
            } catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetAllActivities", ex);
            }
        }
    }
}
