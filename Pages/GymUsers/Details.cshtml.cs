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

namespace GymManager.Pages.GymUsers
{
    [Authorize(Roles ="Admin,Trainer")]
    public class DetailsModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public DetailsModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

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
    }
}
