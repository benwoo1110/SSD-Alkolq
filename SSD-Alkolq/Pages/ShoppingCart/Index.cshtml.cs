using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Pages.ShoppingCart
{
    public class IndexModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public IndexModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public IList<ShoppingCartItem> ShoppingCart { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ShoppingCart = await _context.ShoppingCart
                .Include(s => s.AlcoholProduct)
                .Include(s => s.User)
                .Where(s => s.UserID.Equals(userId))
                .ToListAsync();
        }
    }
}
