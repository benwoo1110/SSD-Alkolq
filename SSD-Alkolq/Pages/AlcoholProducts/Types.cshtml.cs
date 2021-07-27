using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Pages.AlcoholProducts
{
    public class TypesModel : PageModel
    {
        private readonly SSD_Alkolq.Data.AlkolqContext _context;

        public TypesModel(SSD_Alkolq.Data.AlkolqContext context)
        {
            _context = context;
        }

        public string TypeName { get; set; }

        public ProductType ProductType { get; set; }

        public IList<ShoppingCartItem> ShoppingCartItems { get; set; }

        public IList<AlcoholProduct> AlcoholProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(string typeName)
        {
            TypeName = typeName;
            ProductType = await _context.ProductTypes.FirstOrDefaultAsync(t => t.Name.Equals(TypeName));
            if (ProductType == null)
            {
                return NotFound();
            }

            AlcoholProducts = await _context.AlcoholProducts.Where(p => p.Type.Equals(TypeName)).ToListAsync();
            ShoppingCartItems = new List<ShoppingCartItem>();
            foreach (var product in AlcoholProducts)
            {
                ShoppingCartItems.Add(await _context.ShoppingCart.FirstOrDefaultAsync(m => m.AlcoholProductID == product.ID));
            }

            return Page();
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

        public bool IsItemInCart(AlcoholProduct item)
        {
            return ShoppingCartItems.ElementAt(AlcoholProducts.IndexOf(item)) != null;
        }
    }
}
