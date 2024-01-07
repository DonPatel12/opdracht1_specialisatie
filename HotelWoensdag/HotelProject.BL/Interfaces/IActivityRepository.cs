using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IActivityRepository
    {
        List<Activity> GetAllActivities();
        void AddActivity(Activity activity);
        void UpdateActivity(Activity activity);
        void DeleteActivity(Activity activity);

        Activity GetActivityById(int id);

    }
}
