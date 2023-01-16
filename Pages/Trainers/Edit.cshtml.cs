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
using Microsoft.AspNetCore.Identity;

namespace GymManager.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(GymManager.Data.GymManagerContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Trainer == null)
            {
                return NotFound();
            }

            var trainer = await _context.Trainer.FirstOrDefaultAsync(m => m.Id == id);
            if (trainer == null)
            {
                return NotFound();
            }
            Trainer = trainer;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync<Trainer>(
                    Trainer,
                    "Trainer",
                    i => i.Name))
                {
                    _context.Attach(Trainer).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainer.Any(e => e.Id == id);
        }
    }
}
