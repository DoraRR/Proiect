using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GymManager.Pages.GymUsers
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(GymManager.Data.GymManagerContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
      public GymUser GymUser { get; set; }

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
            else 
            {
                GymUser = gymuser;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.GymUser == null)
            {
                return NotFound();
            }
            var gymuser = await _context.GymUser.FindAsync(id);

            if (gymuser != null)
            {
                GymUser = gymuser;
                var user = await _userManager.FindByIdAsync(GymUser.AspNetUserId);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }


                var result = await _userManager.DeleteAsync(user);
                var userId = await _userManager.GetUserIdAsync(user);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred deleting user.");
                }
                else
                {
                    _context.GymUser.Remove(GymUser);
                    await _context.SaveChangesAsync();
                }
                
            }

            return RedirectToPage("./Index");
        }
    }
}
