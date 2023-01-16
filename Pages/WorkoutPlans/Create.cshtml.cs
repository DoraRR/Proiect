using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GymManager.Pages.WorkoutPlans
{
    [Authorize(Roles = "Trainer")]
    public class CreateModel : WorkoutPlanPageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(GymManager.Data.GymManagerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var workoutPlan = new WorkoutPlan();
            workoutPlan.Workouts = new List<WorkoutWorkoutPlans>();
            PopulateSelectedWorkouts(_context, workoutPlan);
            return Page();
        }

        [BindProperty]
        public WorkoutPlan WorkoutPlan { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(List<SelectedWorkouts> selectedWorkouts)
        {
            //ignoram proprietate Trainer la validare pt ca o setam pe baza user-ului autentificat
            ModelState.Remove("WorkoutPlan.Trainer");
            if (ModelState.IsValid)
            {
                var newWorkoutPlan = new WorkoutPlan();
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
                var trainer = _context.Trainer.FirstOrDefault(m => m.AspNetUserId == user.Id);
                if (trainer == null)
                {
                    return NotFound();
                }
                WorkoutPlan.TrainerId = trainer.Id;
                if (selectedWorkouts != null)
                {
                    newWorkoutPlan.Workouts = new List<WorkoutWorkoutPlans>();
                    foreach (var selectedWorkout in selectedWorkouts.Where(x => x.Id != 0))
                    {
                        newWorkoutPlan.Workouts.Add(new WorkoutWorkoutPlans
                        {
                            WorkoutId = selectedWorkout.Id,
                            Day = selectedWorkout.Day
                        });
                    }
                }
                WorkoutPlan.Workouts = newWorkoutPlan.Workouts;

                _context.WorkoutPlan.Add(WorkoutPlan);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            PopulateSelectedWorkouts(_context, WorkoutPlan);
            return Page();
        }
    }
}
