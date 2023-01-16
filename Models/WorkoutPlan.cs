namespace GymManager.Models
{
    public class WorkoutPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } 
        public ICollection<WorkoutWorkoutPlans> Workouts { get; set; }=new HashSet<WorkoutWorkoutPlans>();
        public IEnumerable<GymUser> GymUsers { get; set; } = new HashSet<GymUser>();

    }
}
