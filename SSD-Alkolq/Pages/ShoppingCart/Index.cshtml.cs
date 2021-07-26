using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SSD_Alkolq.Models;
using SSD_Alkolq.Pages.Services;
using Stripe.Checkout;

namespace SSD_Alkolq.Pages.ShoppingCart
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;
        public readonly StripeOptions _stripeOptions;

        public IndexModel(SSD_Alkolq.Data.AlkolqContext context, IOptions<StripeOptions> stripeOptions)
        {
            _context = context;
            _stripeOptions = stripeOptions.Value;
        }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            ShoppingCartItems = await _context.ShoppingCart
                .Include(s => s.AlcoholProduct)
                .Include(s => s.User)
                .Where(s => s.UserID.Equals(userId))
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            ShoppingCartItems = await _context.ShoppingCart
                .Include(s => s.AlcoholProduct)
                .Include(s => s.User)
                .Where(s => s.UserID.Equals(userId))
                .ToListAsync();

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var cartItem in ShoppingCartItems)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long?) (cartItem.AlcoholProduct.Price * 100),
                        Currency = "sgd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = cartItem.AlcoholProduct.Name,
                        },
                    },
                    Quantity = cartItem.Quantity,
                });
            }

            var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = url + "/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = url + "/ShoppingCart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
