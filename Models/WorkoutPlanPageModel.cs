using Microsoft.AspNetCore.Mvc.RazorPages;
using GymManager.Data;

namespace GymManager.Models
{
    public class WorkoutPlanPageModel : PageModel
    {
        public List<SelectedWorkouts> SelectedWorkoutsList;
        public void PopulateSelectedWorkouts(GymManagerContext context, WorkoutPlan workoutPlan)
        {
            var allWorkouts = context.Workout;
            var selectedWorkouts = new HashSet<int>(workoutPlan.Workouts.Select(c => c.WorkoutId));
            SelectedWorkoutsList = new List<SelectedWorkouts>();
            foreach (var workout in allWorkouts)
            {
                SelectedWorkoutsList.Add(new SelectedWorkouts
                {
                    Id = workout.Id,
                    Name = workout.Name,
                    Day = workoutPlan.Workouts.FirstOrDefault(x=>x.WorkoutId == workout.Id)?.Day ?? 0,
                    Selected = selectedWorkouts.Contains(workout.Id)
                });
            }
        }

        public void UpdateWorkoutPlan(GymManagerContext context, List<SelectedWorkouts> selectedWorkouts, WorkoutPlan planToUpdate)
        {
            if (selectedWorkouts == null)
            {
                planToUpdate.Workouts = new List<WorkoutWorkoutPlans>();
                return;
            }
            var workouts = new HashSet<int>(planToUpdate.Workouts.Select(c => c.WorkoutId));

            var newWorkouts = new List<WorkoutWorkoutPlans>();
            foreach (var workout in selectedWorkouts.Where(x => x.Id != 0))
            {
                var thisWorkout = planToUpdate.Workouts.SingleOrDefault(x => x.WorkoutId == workout.Id);

                if (thisWorkout == null)
                {
                    thisWorkout = new WorkoutWorkoutPlans
                    {
                        WorkoutPlanId = planToUpdate.Id,
                        WorkoutId = workout.Id,
                        Day = workout.Day
                    };
                    planToUpdate.Workouts.Add(thisWorkout);
                }else
                {
                    thisWorkout.Day = workout.Day;
                }

                newWorkouts.Add(thisWorkout);
            }

            var workoutsToRemove = new List<WorkoutWorkoutPlans>();
            foreach (var workout in planToUpdate.Workouts.Except(newWorkouts))
            {
                workoutsToRemove.Add(workout);

            }
            foreach (var workout in workoutsToRemove)
            {
                planToUpdate.Workouts.Remove(workout);
                context.Remove(workout);
            }
        }
    }

}