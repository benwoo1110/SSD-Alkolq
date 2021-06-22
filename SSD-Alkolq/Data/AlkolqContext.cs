using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Data
{
    public class AlkolqContext : DbContext
    {
        public AlkolqContext (DbContextOptions<AlkolqContext> options)
            : base(options)
        {
        }

        public DbSet<SSD_Alkolq.Models.AlcoholProduct> AlchoholProduct { get; set; }

        public DbSet<SSD_Alkolq.Models.Customer> Customer { get; set; }
    }
}
