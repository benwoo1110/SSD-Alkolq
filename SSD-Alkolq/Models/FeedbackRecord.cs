using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Alkolq.Models
{
    public class FeedbackRecord
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("ApplicationUser")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        public ApplicationUser User { get; set; }
        //Logged in user performing the action

        [Display(Name = "Feedback Type")]
        [Required]
        public string FeedbackType { get; set; }
        //Type of Feedback

        [Display(Name = "Feedback Content")]
        [Required]
        [StringLength(2048, MinimumLength = 80)]
        public string FeedbackContent { get; set; }
        //Content of Feedback

        [Display(Name = "Date/Time Stamp")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }
        //Time when the event occurred
    }
}
