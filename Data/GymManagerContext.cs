using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymManager.Models;

namespace GymManager.Data
{
    public class GymManagerContext : DbContext
    {
        public GymManagerContext (DbContextOptions<GymManagerContext> options)
            : base(options)
        {
        }

        public DbSet<GymManager.Models.Workout> Workout { get; set; } = default!;

        public DbSet<GymManager.Models.WorkoutPlan> WorkoutPlan { get; set; }

        public DbSet<GymManager.Models.GymUser> GymUser { get; set; }

        public DbSet<GymManager.Models.Trainer> Trainer { get; set; }

    }
}
