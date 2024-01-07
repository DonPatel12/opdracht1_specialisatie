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
                string sql = @"INSERT INTO Activity( name, description, eventdatetime, duration,  location, availablespots, adultCost, childCost, discount, adultage) 
                               OUTPUT INSERTED.id 
                               VALUES( @name, @description, @eventdatetime, @duration, @location, @availablespots, @adultcost, @childcost, @discount, @adultage)";

                using (SqlConnection conn = new (connectionString))
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
                        cmd.Parameters.AddWithValue("@eventdatetime", activity.EventDateTime);
                        cmd.Parameters.AddWithValue("@duration", activity.Duration);
                        cmd.Parameters.AddWithValue("@location", activity.Location);
                        cmd.Parameters.AddWithValue("@availablespots", activity.NumberOfSpots);
                        cmd.Parameters.AddWithValue("@adultcost", activity.PriceInfo.AdultCost);
                        cmd.Parameters.AddWithValue("@childcost", activity.PriceInfo.ChildCost);
                        cmd.Parameters.AddWithValue("@discount", activity.PriceInfo.Discount);
                        cmd.Parameters.AddWithValue("@adultage", activity.PriceInfo.AdultAge);
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
            string sql = @"DELETE FROM Activity 
                           WHERE id=@id";
            using (SqlConnection conn = new (connectionString))
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

        public Activity GetActivityById(int id)
        {
            try
            {
                string sql = @"SELECT * FROM Activity 
                               WHERE id=@id";
                using (SqlConnection conn = new (connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Activity activity = new(
                             (int)reader["id"],
                             (string)reader["name"],
                             (string)reader["description"],
                             (DateTime)reader["eventdatetime"],
                             (int)reader["duration"],
                             (string)reader["location"],
                             (int)reader["availableSpots"],
                             new PriceInfo(
                                 (decimal)reader["adultCost"],
                                 (decimal)reader["childCost"],
                                 (decimal)reader["discount"],
                                 (int)reader["adultAge"])
                             );
                        return activity;
                    }
                    return null;
                }   
            } catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetActivityById", ex);
            }
        }

        public List<Activity> GetAllActivities()
        {
            try
            {
                List<Activity> activities = new ();
                string sql = @"SELECT * FROM Activity";
                using (SqlConnection conn = new (connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Activity activity = new  (
                            (int)reader["id"],
                            (string)reader["name"], 
                            (string)reader["description"],
                            (DateTime)reader["eventdatetime"],
                            (int)reader["duration"],
                            (string)reader["location"], 
                            (int)reader["availableSpots"],
                            new PriceInfo(
                                (decimal)reader["adultCost"],
                                (decimal)reader["childCost"],
                                (decimal)reader["discount"],
                                (int)reader["adultAge"]
                            )
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

        public void UpdateActivity(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
