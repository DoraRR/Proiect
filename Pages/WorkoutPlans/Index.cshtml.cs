using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymManager.Data;
using GymManager.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GymManager.Pages.WorkoutPlans
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(GymManager.Data.GymManagerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<WorkoutPlan> WorkoutPlan { get; set; } = default!;
        public string Filter { get; set; } = string.Empty;

        public async Task OnGetAsync(string searchQuery)
        {
            if (_context.WorkoutPlan != null)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    Filter = searchQuery;
                    Expression<Func<WorkoutPlan, bool>> filter = null;
                    if (User.IsInRole("Member"))
                    {
                        var gymUser = _context.GymUser.Include(b => b.WorkoutPlans).ThenInclude(b => b.Workouts).ThenInclude(b => b.Workout).FirstOrDefault(m => m.AspNetUserId == user.Id);
                        if (gymUser != null)
                        {
                            if (!string.IsNullOrEmpty(Filter))
                                WorkoutPlan = gymUser.WorkoutPlans.Where(x => x.Name.Contains(Filter)).ToList();
                            else
                                WorkoutPlan = gymUser.WorkoutPlans.ToList();
                        }
                    }
                    else
                    {
                        if (User.IsInRole("Trainer"))
                        {
                            var trainer = _context.Trainer.FirstOrDefault(m => m.AspNetUserId == user.Id);
                            if (trainer != null)
                            {
                                filter = x => x.TrainerId == trainer.Id;
                                if (!string.IsNullOrEmpty(Filter))
                                    filter = x => x.TrainerId == trainer.Id && x.Name.Contains(Filter);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Filter))
                                filter = x => x.Name.Contains(Filter);
                        }
                        IQueryable<WorkoutPlan> data = _context.WorkoutPlan.Include(b => b.Workouts).ThenInclude(b => b.Workout);
                        if (filter != null)
                            WorkoutPlan = await data.Where(filter).AsNoTracking().ToListAsync();
                        else
                            WorkoutPlan = await data.AsNoTracking().ToListAsync();
                    }
                }

            }
        }
    }
}
