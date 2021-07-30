using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSD_Alkolq.Models;

namespace SSD_Alkolq.Data
{
    public class AlkolqContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AlkolqContext (DbContextOptions<AlkolqContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<SSD_Alkolq.Models.ProductType> ProductTypes { get; set; }

        public DbSet<SSD_Alkolq.Models.AlcoholProduct> AlcoholProducts { get; set; }

        public DbSet<SSD_Alkolq.Models.AuditRecord> AuditRecords { get; set; }

        public DbSet<SSD_Alkolq.Models.FeedbackRecord> FeedbackRecords { get; set; }

        public DbSet<SSD_Alkolq.Models.PaymentRecord> PaymentRecords { get; set; }

        public DbSet<SSD_Alkolq.Models.ShoppingCartItem> ShoppingCart { get; set; }

        public DbSet<SSD_Alkolq.Models.ProductRating> ProductRatings { get; set; }
    }
}
