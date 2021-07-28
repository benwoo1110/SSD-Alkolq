using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Areas.Identity.Pages.Account.Manage
{
    public class PaymentHistory : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public PaymentHistory(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public IList<PaymentRecord> PaymentRecords { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            PaymentRecords = await _context.PaymentRecords
                .Where(p => p.UserID.Equals(userId))
                .ToListAsync();
        }
    }
}
