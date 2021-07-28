using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Alkolq.Data;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Areas.Identity.Pages.Account.Manage
{
    public class WebsiteFeedbackModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public WebsiteFeedbackModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet()
        {
        }

        [BindProperty]
        public FeedbackRecord FeedbackRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            FeedbackRecord.UserID = userId;
            FeedbackRecord.DateTimeStamp = DateTime.Now;

            _context.FeedbackRecords.Add(FeedbackRecord);
            await _context.SaveChangesAsync();

            StatusMessage = "Your feedback has been recorded. Thank you!";
            return RedirectToPage();
        }
    }
}
