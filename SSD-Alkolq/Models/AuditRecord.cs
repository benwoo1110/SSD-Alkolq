using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Alkolq.Models
{
    public class AuditRecord
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Performed By")]
        public string Performer { get; set; }
        //Identity of user or entity performing the action

        [Display(Name = "Audit Action")]
        [Required]
        public string Action { get; set; }
        // Could be  Login Success /Failure/ Logout, Create, Delete, View, Update

        [Display(Name = "Date/Time Stamp")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }
        //Time when the event occurred

        [Display(Name = "Affected Data")]
        public string AffectedData { get; set; }
        //Store the model class name that is affected.

        [Display(Name = "Affected Data ID")]
        public string AffectedDataID { get; set; }
        //Store the ID of table record that is affected

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
    }
}
