using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagerApp.Models
{
    public class WorkoutPlanCrud
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        public List<WorkoutCrud> SelectedWorkouts { get; set; } = new List<WorkoutCrud>();
    }
}
