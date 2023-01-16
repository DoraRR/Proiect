using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymManager.Data;
using GymManager.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace GymManager.Pages.Workouts
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;

        public IndexModel(GymManager.Data.GymManagerContext context)
        {
            _context = context;
        }

        public IList<Workout> Workout { get;set; } = default!;
        public string Filter { get; set; } = string.Empty;

        public async Task OnGetAsync(string searchQuery)
        {
            if (_context.Workout != null)
            {
                Filter = searchQuery;
                Expression<Func<Workout, bool>> filter = null;
                if (!string.IsNullOrEmpty(Filter))
                    filter = x => x.Name.Contains(Filter);
                if (filter != null)
                    Workout = await _context.Workout.Where(filter).ToListAsync();
                else
                    Workout = await _context.Workout.ToListAsync();
            }
        }
    }
}
