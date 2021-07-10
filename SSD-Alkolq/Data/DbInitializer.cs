using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSD_Alkolq.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                if (context.AlcoholProduct.Any())
                {
                    return;   // DB has been seeded
                }

                var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                var dataFile = Path.Combine(environment.WebRootPath, "data.csv");

                var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
                csvConfiguration.HeaderValidated = null;
                csvConfiguration.MissingFieldFound = null;

                using (var reader = new StreamReader(dataFile))
                using (var csv = new CsvReader(reader, csvConfiguration))
                {
                    var records = csv.GetRecords<AlcoholProduct>();
                    context.AlcoholProduct.AddRange(records);
                }

                context.SaveChanges();
            }
        }
    }
}
