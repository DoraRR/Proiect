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

namespace GymManager.Pages.WorkoutPlans
{
    [Authorize(Roles = "Trainer")]
    public class EditModel : WorkoutPlanPageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public EditModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WorkoutPlan WorkoutPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.WorkoutPlan == null)
            {
                return NotFound();
            }

            var workoutplan = await _context.WorkoutPlan.Include(b => b.Workouts).ThenInclude(b=>b.Workout).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (workoutplan == null)
            {
                return NotFound();
            }
            PopulateSelectedWorkouts(_context, workoutplan);
            WorkoutPlan = workoutplan;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, List<SelectedWorkouts> selectedWorkouts)
        {
            if (id == null)
                return NotFound();


            //_context.Attach(WorkoutPlan).State = EntityState.Modified;
            var workoutplan = await _context.WorkoutPlan.Include(b=>b.Trainer).Include(b => b.Workouts).ThenInclude(b => b.Workout).FirstOrDefaultAsync(m => m.Id == id);
            if (workoutplan == null)
                return NotFound();

            //ignoram proprietate Trainer la validare
            ModelState.Remove("WorkoutPlan.Trainer");
            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync<WorkoutPlan>(
                    workoutplan,
                    "WorkoutPlan",
                    i => i.Name))
                {
                    UpdateWorkoutPlan(_context, selectedWorkouts, workoutplan);
                    WorkoutPlan.Workouts = workoutplan.Workouts;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");

                }
            }

            UpdateWorkoutPlan(_context, selectedWorkouts, WorkoutPlan);
            PopulateSelectedWorkouts(_context, WorkoutPlan);
            return Page();

        }
    }
}
