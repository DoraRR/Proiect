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

namespace GymManager.Pages.Workouts
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public DetailsModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

      public Workout Workout { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Workout == null)
            {
                return NotFound();
            }

            var workout = await _context.Workout.FirstOrDefaultAsync(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }
            else 
            {
                Workout = workout;
            }
            return Page();
        }
    }
}
