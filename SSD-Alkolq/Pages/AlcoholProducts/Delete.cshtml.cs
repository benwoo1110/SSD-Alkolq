using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SSD_Alkolq.Pages.AlcoholProducts
{
    [Authorize(Roles = "Admin, Manager")]
    public class DeleteModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public DeleteModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.AlcoholProduct AlcoholProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AlcoholProduct = await _context.AlcoholProducts.FirstOrDefaultAsync(m => m.ID == id);

            if (AlcoholProduct == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AlcoholProduct = await _context.AlcoholProducts.FindAsync(id);

            if (AlcoholProduct != null)
            {
                _context.AlcoholProducts.Remove(AlcoholProduct);
                //  await _context.SaveChangesAsync();

                // Once a record is deleted, create an audit record
                if (await _context.SaveChangesAsync() > 0)
                {
                    // Create an auditrecord object
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var auditrecord = new AuditRecord
                    {
                        Performer = userId,
                        AffectedData = "AlcoholProduct",
                        AffectedDataID = AlcoholProduct.ID.ToString(),
                        Action = "DELETE ENTRY",
                        DateTimeStamp = DateTime.Now,
                        IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                    };
                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }

            }

            return RedirectToPage("./Index");
        }
    }
}
