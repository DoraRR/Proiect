using GymManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagerApp.Data
{
    public interface IRestService
    {
        Task<List<WorkoutPlan>> GetWorkoutPlanListsAsync();
        Task<List<Workout>> GetWorkoutListsAsync();
        Task<bool> SaveWorkoutPlanAsync(WorkoutPlanCrud item, bool isNewItem);
        Task<bool> SaveWorkoutAsync(Workout item, bool isNewItem);
        Task<bool> DeleteWorkoutPlanAsync(int id);
        Task<bool> DeleteWorkoutAsync(int id);
        void SetToken(string token);
        Task<string> LoginAsync(LoginData login);
    }
}
