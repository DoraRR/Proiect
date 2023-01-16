using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace GymManager.Pages.GymUsers
{
    [Authorize(Roles = "Admin,Trainer")]
    public class SetWorkoutPlanModel : SetWorkoutPlanPageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public SetWorkoutPlanModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

        //[BindProperty]
        //public GymUser GymUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GymUser == null)
            {
                return NotFound();
            }

            var gymuser =  await _context.GymUser.Include(s=>s.WorkoutPlans).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (gymuser == null)
            {
                return NotFound();
            }
            PopulateSelectedWorkoutPlans(_context, gymuser);
            //GymUser = gymuser;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, int[] selectedWorkoutPlans)
        {
            if (id == null)
                return NotFound();

            var gymuser = await _context.GymUser.Include(s => s.WorkoutPlans).FirstOrDefaultAsync(m => m.Id == id);
            if (gymuser == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                    UpdateWorkoutPlan(_context, selectedWorkoutPlans, gymuser);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");

            }

            UpdateWorkoutPlan(_context, selectedWorkoutPlans, gymuser);
            PopulateSelectedWorkoutPlans(_context, gymuser);
            return Page();
        }

        private bool GymUserExists(int id)
        {
          return _context.GymUser.Any(e => e.Id == id);
        }
    }
}
