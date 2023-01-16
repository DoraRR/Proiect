namespace GymManager.Models
{
    public class SelectedWorkoutPlan
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Selected { get; set; }
    }
}
