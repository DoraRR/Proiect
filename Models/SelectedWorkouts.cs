namespace GymManager.Models
{
    public class SelectedWorkouts
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Day { get; set; }
        public bool Selected { get; set; }
    }
}
