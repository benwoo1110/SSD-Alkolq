using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Alkolq.Models
{
    public class ShoppingCartItem
    {
        public int ID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("AlcoholProduct")]
        public int AlcoholProductID { get; set; }
        public AlcoholProduct AlcoholProduct { get; set; }
        public int Quantity { get; set; }
    }
}
