using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSD_Alkolq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Alkolq.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/complex-data-model?view=aspnetcore-5.0&tabs=visual-studio#seed-the-database

            using (var context = new AlkolqContext(serviceProvider.GetRequiredService<DbContextOptions<AlkolqContext>>()))
            {
                // Look for any movies.
                if (context.AlchoholProduct.Any())
                {
                    return;   // DB has been seeded
                }

                context.AlchoholProduct.AddRange(
                    new AlcoholProduct
                    {
                        Name = "Name",
                        Type = "Type",
                        Price = 1.00m
                    }
                    // Add more here
                );

                context.SaveChanges();
            }
        }
    }
}
