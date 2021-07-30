using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IWebHostEnvironment _environment;
        private readonly IEmailSender _emailSender;
        private readonly SSD_Alkolq.Data.AlkolqContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuccessModel(
            IWebHostEnvironment environment,
            IEmailSender emailSender,
            SSD_Alkolq.Data.AlkolqContext context,
            UserManager<ApplicationUser> userManager)
        {
            _environment = environment;
            _emailSender = emailSender;
            _context = context;
            _userManager = userManager;
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

            // Send receipt to email
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var receiptHtmlPath = Path.Combine(_environment.WebRootPath, "html/paymentreceipt.html");
            string receiptHtmlText = System.IO.File.ReadAllText(receiptHtmlPath);
            await _emailSender.SendEmailAsync(user.Email, "Payment receipt", receiptHtmlText);

            await _context.SaveChangesAsync();
        }
    }
}
