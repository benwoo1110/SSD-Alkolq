using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Alkolq.Models
{
    public class ProductRating
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        //Identity of user UUID

        [Display(Name = "Date/Time Stamp")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }
        //Time when the Rating occurred

        [Display(Name = "RatingContent")]
        [Required]
        public string RatingContent { get; set; }
        //Store Text of Rating

        [Display(Name = "RatingStar")]
        [Range(1, 5)]
        [Required]
        public int RatingStar { get; set; }
        //Store number of Stars from 1 to 5

        [ForeignKey("AlcoholProduct")]
        public int AlcoholProductID { get; set; }

    }
}
