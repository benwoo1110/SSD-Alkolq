using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace SSD_Alkolq.Pages.Roles
{
    //[Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DeleteModel(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public ApplicationRole ApplicationRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole = await _roleManager.FindByIdAsync(id);

            if (ApplicationRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole = await _roleManager.FindByIdAsync(id);
            IdentityResult roleRuslt = await _roleManager.DeleteAsync(ApplicationRole);

            return RedirectToPage("./Index");

        }
    }
}

