using System;
using System.ComponentModel.DataAnnotations;

namespace SSD_Alkolq.Models
{
    public class AlcoholProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime ListDate { get; set; }
    }
}