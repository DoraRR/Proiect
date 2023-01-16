namespace GymManager.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<WorkoutWorkoutPlans> WorkoutPlans { get; set; } = new HashSet<WorkoutWorkoutPlans>();
    }
}
