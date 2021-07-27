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

namespace SSD_Alkolq.Pages.Feedback
{
    public class EditModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public EditModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FeedbackRecord FeedbackRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeedbackRecord = await _context.FeedbackRecords.FirstOrDefaultAsync(m => m.Feedback_ID == id);

            if (FeedbackRecord == null)
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

            _context.Attach(FeedbackRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackRecordExists(FeedbackRecord.Feedback_ID))
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

        private bool FeedbackRecordExists(int id)
        {
            return _context.FeedbackRecords.Any(e => e.Feedback_ID == id);
        }
    }
}
