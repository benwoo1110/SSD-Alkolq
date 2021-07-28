using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Alkolq.Models
{
    public class AlcoholProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime ListDate { get; set; }
        public string ImageName { set; get; }
    }
}