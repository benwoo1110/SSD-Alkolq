using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Models;
using Stripe;
using Stripe.Checkout;

namespace SSD_Alkolq.Pages.Payment
{
    public class SuccessModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public SuccessModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

        public async Task OnGetAsync([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            var customerService = new CustomerService();
            Customer = customerService.Get(session.CustomerId);

            var paymentRecord = await _context.PaymentRecords
                .FirstOrDefaultAsync(r => r.CheckoutSessionID.Equals(session_id));
            if (paymentRecord != null)
            {
                return;
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // New payment entry record
            _context.PaymentRecords.Add(new PaymentRecord
            {
                UserID = userId,
                CheckoutSessionID = session_id,
                Amount = (decimal)(session.AmountTotal / 100m),
                DateTimeStamp = DateTime.Now
            });

            // Clear user's shopping cart
            _context.ShoppingCart.RemoveRange(await _context.ShoppingCart
                .Where(s => s.UserID.Equals(userId))
                .ToListAsync());

            await _context.SaveChangesAsync();
        }
    }
}
