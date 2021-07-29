using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace SSD_Alkolq.Pages.AlcoholProducts
{
    // [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(SSD_Alkolq.Data.AlkolqContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.AlcoholProduct AlcoholProduct { get; set; }

        [BindProperty]
        public IFormFile Image { set; get; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save image to uploads folder.
            if (Image != null)
            {
                var fileName = GenerateUniqueName(this.Image.FileName);
                var uploadsPath = Path.Combine(_environment.WebRootPath, "images");
                var filePath = Path.Combine(uploadsPath, fileName);
                Image.CopyTo(new FileStream(filePath, FileMode.Create));
                AlcoholProduct.ImageName = fileName;
            }

            _context.AlcoholProducts.Add(AlcoholProduct);

            // Once a record is added, create an audit record
            if (await _context.SaveChangesAsync() > 0)
            {
                // Create an auditrecord object
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var auditrecord = new AuditRecord
                {
                    Performer = userId,
                    AffectedData = "AlcoholProduct",
                    AffectedDataID = AlcoholProduct.ID.ToString(),
                    Action = "CREATE ENTRY",
                    DateTimeStamp = DateTime.Now,
                    IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                };
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private string GenerateUniqueName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_" 
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
