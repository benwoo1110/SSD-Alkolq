using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSD_Alkolq.Data;
using System;
using System.Linq;

namespace SSD_Alkolq.Models
{
    public static class RoleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AlkolqContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AlkolqContext>>()))
            {

                if (context.Roles.Any())
                {
                    return;   // DB has been seeded
                }

        context.Roles.AddRange(
                    new ApplicationRole
                    {
                        Name = "Admin",
                        Description = "Administrator",
                        CreatedDate = DateTime.Parse("2021-7-20"),
                        IPAddress = ""
                    },

                    new ApplicationRole
                    {
                        Name = "Manager",
                        Description = "Product Manager",
                        CreatedDate = DateTime.Parse("2021-7-20"),
                        IPAddress = ""
                    }
                );
                context.SaveChanges();
            }
        }
    }
}