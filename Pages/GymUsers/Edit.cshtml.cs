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
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GymManager.Pages.GymUsers
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
        public GymUser GymUser { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GymUser == null)
            {
                return NotFound();
            }

            var gymuser = await _context.GymUser.FirstOrDefaultAsync(m => m.Id == id);
            if (gymuser == null)
            {
                return NotFound(); 
            }
            GymUser = gymuser;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            //var gymuser = await _context.GymUser.FirstOrDefaultAsync(m => m.Id == id);
            //if (gymuser == null)
            //    return NotFound();

            //GymUser.Email = gymuser.Email;

            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync<GymUser>(
                    GymUser,
                    "GymUser",
                    i => i.Name, i=>i.Gender))
                {
                    _context.Attach(GymUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }

        private bool GymUserExists(int id)
        {
            return _context.GymUser.Any(e => e.Id == id);
        }
    }
}
