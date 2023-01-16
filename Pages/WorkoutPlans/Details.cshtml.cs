using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace GymManager.Pages.WorkoutPlans
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public DetailsModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

      public WorkoutPlan WorkoutPlan { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.WorkoutPlan == null)
            {
                return NotFound();
            }

            var workoutplan = await _context.WorkoutPlan.FirstOrDefaultAsync(m => m.Id == id);
            if (workoutplan == null)
            {
                return NotFound();
            }
            else 
            {
                WorkoutPlan = workoutplan;
            }
            return Page();
        }
    }
}
