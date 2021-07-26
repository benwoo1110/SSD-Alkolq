using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Stripe.Checkout;

namespace SSD_Alkolq.Pages.Payment
{
    public class SuccessModel : PageModel
    {
        public Customer Customer { get; set; }

        public async Task OnGetAsync([FromQuery] string session_id)
        {
            var sessionService = new SessionService();
            Session session = sessionService.Get(session_id);

            var customerService = new CustomerService();
            Customer = customerService.Get(session.CustomerId);
        }
    }
}
