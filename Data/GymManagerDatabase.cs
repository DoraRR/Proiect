using GymManagerApp.Data;
using GymManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagerApp.Data
{
    public class GymManagerDatabase
    {
        IRestService restService;
        private string Token { get; set; }
        public GymManagerDatabase(IRestService service)
        {
            restService = service;
            Token = string.Empty;
        }

        public void SetToken(string token)
        {
            Token = token;
            restService.SetToken(Token);
        }

        public Task<List<WorkoutPlan>> GetWorkoutPlanListsAsync()
        {
            return restService.GetWorkoutPlanListsAsync();

        }

        public Task<List<Workout>> GetWorkoutListsAsync()
        {
            return restService.GetWorkoutListsAsync();

        }

        public Task<bool> SaveWotkoutPlanAsync(WorkoutPlanCrud item, bool isNewItem = true)
        {
            return restService.SaveWorkoutPlanAsync(item, isNewItem);
        }
        public Task<bool> SaveWotkoutAsync(Workout item, bool isNewItem = true)
        {
            return restService.SaveWorkoutAsync(item, isNewItem);
        }
        public Task<bool> DeleteWorkoutPlanAsync(WorkoutPlanCrud item)
        {
            return restService.DeleteWorkoutPlanAsync(item.Id);
        }

        public Task<bool> DeleteWorkoutAsync(Workout item)
        {
            return restService.DeleteWorkoutAsync(item.Id);
        }

        public Task<string> LoginAsync(LoginData login)
        {
            return restService.LoginAsync(login);
        }

    }

}
