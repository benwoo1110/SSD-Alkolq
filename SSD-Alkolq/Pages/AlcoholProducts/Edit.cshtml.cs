using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SSD_Alkolq.Pages.AlcoholProducts
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(SSD_Alkolq.Data.AlkolqContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Models.AlcoholProduct AlcoholProduct { get; set; }

        [BindProperty]
        public IFormFile Image { set; get; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AlcoholProduct = await _context.AlcoholProduct.FirstOrDefaultAsync(m => m.ID == id);

            if (AlcoholProduct == null)
            {
                return NotFound();
            }
            return Page();
        }

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

            _context.Attach(AlcoholProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlchoholProductExists(AlcoholProduct.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AlchoholProductExists(int id)
        {
            return _context.AlcoholProduct.Any(e => e.ID == id);
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
