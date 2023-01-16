using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymManager.Data;
using GymManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace GymManager.Pages.GymUsers
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly GymManager.Data.GymManagerContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;

        public CreateModel(GymManager.Data.GymManagerContext context, UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore)
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<IdentityUser>)_userStore;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public GymUser GymUser { get; set; }

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = Activator.CreateInstance<IdentityUser>();
                await _userStore.SetUserNameAsync(user, GymUser.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, GymUser.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Password);

                if (result.Succeeded)
                {

                    var role = await _userManager.AddToRoleAsync(user, "Member");
                    var userId = await _userManager.GetUserIdAsync(user);
                    GymUser.AspNetUserId = userId;
                    _context.GymUser.Add(GymUser);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
