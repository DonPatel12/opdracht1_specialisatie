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
    public class ActivityManager
    {
        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public void AddActivity(Activity activity)
        {
            try
            {
                _activityRepository.AddActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityException("Error adding activity", ex);
            }
        }

        public void DeleteActivity(Activity activity)
        {
            try
            {
                _activityRepository.DeleteActivity(activity);
            }
            catch (Exception ex)
            {
                throw new ActivityException("Error deleting activity", ex);
            }
        }

        public List<Activity> GetAllActivities()
        {
            try
            {
                return _activityRepository.GetAllActivities();
            }
            catch (Exception ex)
            {
                throw new ActivityException("Error getting all activities", ex);
            }


        }
    }
}
