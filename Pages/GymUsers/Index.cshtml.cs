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
    public class IndexModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public IndexModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

        public IList<GymUser> GymUser { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GymUser != null)
            {
                GymUser = await _context.GymUser.ToListAsync();
            }
        }
    }
}
