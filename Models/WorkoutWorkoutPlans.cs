using Microsoft.EntityFrameworkCore;

namespace GymManager.Models
{
    //[Keyless]
    public class WorkoutWorkoutPlans
    {
        public int Id { get; set; }
        public int WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public int Day { get; set; }
    }
}
