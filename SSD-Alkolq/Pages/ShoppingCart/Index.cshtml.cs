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

        public IList<ShoppingCartItem> ShoppingCart { get; set; }

        public string UserID { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserID == null)
            {
                return Redirect("~/");
            }

            ShoppingCart = await _context.ShoppingCart
                .Include(s => s.AlcoholProduct)
                .Include(s => s.User)
                .Where(s => s.UserID.Equals(UserID))
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Redirect("~/");
            }

            

            return Redirect("~/ShoppingCart");
        }
    }
}
