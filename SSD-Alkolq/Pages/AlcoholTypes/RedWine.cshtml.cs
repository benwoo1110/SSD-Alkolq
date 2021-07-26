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
using System.Security.Claims;

namespace SSD_Alkolq.Pages.AlcoholTypes
{
    public class RedWineModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public RedWineModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public IList<Models.AlcoholProduct> AlchoholProduct { get; set; }
        [BindProperty(SupportsGet = true)]

        public string AlcoholType { get; set; }
        public async Task OnGetAsync()
        {
            IQueryable<string> typeQuery = from m in _context.AlcoholProduct
                                           orderby m.Type
                                           select m.Type;
            var alcohol = from m in _context.AlcoholProduct
                          select m;

            AlchoholProduct = await alcohol.ToListAsync();
        }
        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Redirect("~/Identity/Account/Login");
            }

            var cartItem = new ShoppingCartItem
            {
                UserID = userId,
                AlcoholProductID = id,
                Quantity = 1
            };

            _context.ShoppingCart.Add(cartItem);
            await _context.SaveChangesAsync();

            return Redirect("~/ShoppingCart");
        }
    }
}
