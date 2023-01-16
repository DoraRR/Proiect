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
    [Authorize(Roles = "Admin,Trainer")]
    public class DeleteModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public DeleteModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Workout == null)
            {
                return NotFound();
            }
            var workout = await _context.Workout.FindAsync(id);

            if (workout != null)
            {
                Workout = workout;
                _context.Workout.Remove(Workout);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
