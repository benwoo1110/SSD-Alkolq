using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using SSD_Alkolq.Data;
using System.Security.Claims;

namespace SSD_Alkolq.Pages.Roles
{
    //[Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly AlkolqContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public DeleteModel(AlkolqContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
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

            if (roleRuslt.Succeeded)
            {
                // Audit log
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var auditrecord = new AuditRecord
                {
                    Performer = userId,
                    AffectedData = "ApplicationRole",
                    AffectedDataID = ApplicationRole.Id,
                    Action = "DELETE ENTRY",
                    DateTimeStamp = DateTime.Now,
                };
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");

        }
    }
}

