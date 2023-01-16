using GymManager.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymManager.Models
{
    public class SetWorkoutPlanPageModel : PageModel
    {
        public List<SelectedWorkoutPlan> SelectedWorkoutPlanList;
        public void PopulateSelectedWorkoutPlans (GymManagerContext context, GymUser gymUser)
        {
            var allWorkoutPlans = context.WorkoutPlan;
            var selectedWorkoutPlans = new HashSet<int>(gymUser.WorkoutPlans.Select(c => c.Id));
            SelectedWorkoutPlanList = new List<SelectedWorkoutPlan>();
            foreach (var workoutPlan in allWorkoutPlans)
            {
                SelectedWorkoutPlanList.Add(new SelectedWorkoutPlan
                {
                    Id = workoutPlan.Id,
                    Name = workoutPlan.Name,
                    Selected = selectedWorkoutPlans.Contains(workoutPlan.Id)
                });
            }
        }

        public void UpdateWorkoutPlan(GymManagerContext context, int[] selectedWorkoutPlans, GymUser gymUserToUpdate)
        {
            if (selectedWorkoutPlans == null)
            {
                gymUserToUpdate.WorkoutPlans = new List<WorkoutPlan>();
                return;
            }
            var workoutPlans = new HashSet<int>(gymUserToUpdate.WorkoutPlans.Select(c => c.Id));

            var newWorkoutPlans = new List<WorkoutPlan>();
            foreach (var workoutPlan in selectedWorkoutPlans)
            {
                var thisWorkoutPlan = gymUserToUpdate.WorkoutPlans.SingleOrDefault(x => x.Id == workoutPlan);

                if (thisWorkoutPlan == null)
                {
                    thisWorkoutPlan = context.WorkoutPlan.FirstOrDefault(x=>x.Id == workoutPlan);
                    gymUserToUpdate.WorkoutPlans.Add(thisWorkoutPlan);
                }

                newWorkoutPlans.Add(thisWorkoutPlan);
            }

            var workoutPlansToRemove = new List<WorkoutPlan>();
            foreach (var workout in gymUserToUpdate.WorkoutPlans.Except(newWorkoutPlans))
            {
                workoutPlansToRemove.Add(workout);

            }
            foreach (var workout in workoutPlansToRemove)
            {
                gymUserToUpdate.WorkoutPlans.Remove(workout);
            }
        }
    }
}
