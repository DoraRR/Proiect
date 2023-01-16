using System.ComponentModel.DataAnnotations;

namespace GymManager.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public string AspNetUserId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
    }
}
