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

namespace GymManager.Pages.Trainers
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public IndexModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Trainer != null)
            {
                Trainer = await _context.Trainer.ToListAsync();
            }
        }
    }
}
