using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSD_Alkolq.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using SSD_Alkolq.Data;

namespace SSD_Alkolq.Pages.Roles
{
    //[Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly AlkolqContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public CreateModel(AlkolqContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApplicationRole ApplicationRole { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationRole.CreatedDate = DateTime.UtcNow;
            ApplicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            IdentityResult roleRuslt = await _roleManager.CreateAsync(ApplicationRole);

            if (roleRuslt.Succeeded)
            {
                // Audit log
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var auditrecord = new AuditRecord
                {
                    Performer = userId,
                    AffectedData = "ApplicationRole",
                    AffectedDataID = ApplicationRole.Id,
                    Action = "CREATE ENTRY",
                    DateTimeStamp = DateTime.Now,
                    IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                };
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
