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
    public class EditModel : PageModel
    {
        private readonly AlkolqContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public EditModel(AlkolqContext context, RoleManager<ApplicationRole> roleManager)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationRole appRole = await _roleManager.FindByIdAsync(ApplicationRole.Id);

            appRole.Id = ApplicationRole.Id;
            appRole.Name = ApplicationRole.Name;
            appRole.Description = ApplicationRole.Description;

            IdentityResult roleRuslt = await _roleManager.UpdateAsync(appRole);

            if (roleRuslt.Succeeded)
            {
                // Audit log
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var auditrecord = new AuditRecord
                {
                    Performer = userId,
                    AffectedData = "ApplicationRole",
                    AffectedDataID = ApplicationRole.Id,
                    Action = "UPDATE ENTRY",
                    DateTimeStamp = DateTime.Now,
                    IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                };
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");

            }
            return RedirectToPage("./Index");
        }

    }
}

