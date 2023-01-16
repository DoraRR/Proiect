using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagerApp.Models
{
    public class WorkoutCrud
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Day { get; set; }
        public bool Selected { get; set; }
    }
}
