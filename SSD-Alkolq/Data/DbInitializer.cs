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
                if (!context.AlcoholProducts.Any())
                {
                    var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                    var dataFile = Path.Combine(environment.WebRootPath, "data.csv");

                    var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
                    csvConfiguration.HeaderValidated = null;
                    csvConfiguration.MissingFieldFound = null;

                    using (var reader = new StreamReader(dataFile))
                    using (var csv = new CsvReader(reader, csvConfiguration))
                    {
                        var records = csv.GetRecords<AlcoholProduct>();
                        context.AlcoholProducts.AddRange(records);
                    }
                }

                if (!context.ProductTypes.Any())
                {
                    context.ProductTypes.AddRange(
                        new ProductType
                        { 
                            Name = "Beer",
                            Description = "The third most popular drink in the world after water and tea, Beer is one of the oldest and most widely consumed alcoholic drinks in the world. While malted barley is used most commonly, beer is also brewed from wheat, maize, rice and oats.",
                            ImageName = "beer.jpg"
                        },
                        new ProductType
                        {
                            Name = "Red Wine",
                            Description = "Red wine is a type of wine made from dark-coloured grape varieties. The colour of the wine tends to go from an intense violet to brick red, and finally brown as the wine ages. A delicacy around the world, red wine is usually enjoyed with foods with bold flavours, like red meat or seafood.",
                            ImageName = "red-wine.jpg"
                        },
                        new ProductType
                        {
                            Name = "White Wine",
                            Description = "A wine fermented without skin content, white wine is produced by the fermentation of the non-coloured pulp of grapes and can be straw-yellow, yellow-green and yellow-gold. They are often used to stimulate the appetite before a meal as well as with desserts or in betweeen meals. IN addition, they are oftentimes used in cooking to soften meat and deglaze coooking juices due to their acidity and aroma.",
                            ImageName = "white-wine.jpg"
                        },
                        new ProductType
                        {
                            Name = "Whisky",
                            Description = "Made from fermented gain mash, Whisky is a type of distilled alcoholic beverage that has at least 40% alcohol per volume. There are two main kinds of whisky, malt and grain, as well as a variety of combinations of the two. A famous example of whisky is Jack Daniel's, which is the top-selling American Whisky in the world.",
                            ImageName = "whisky.jpg"
                        },
                        new ProductType
                        {
                            Name = "Vodka",
                            Description = "Vodka is a clear distilled alcoholic beverage from Europe that has different varieties originating in Poland, Russia and Sweden. Composed primarily of water and ethanol, it is traditionally distilled from fermented cereal grains, and more recently, potatoes. It is traditionally drunk neat or freezer chill but can also be served in cocktails or mixed drinks.",
                            ImageName = "vodka.jpg"
                        },
                        new ProductType
                        {
                            Name = "Vodka",
                            Description = "Vodka is a clear distilled alcoholic beverage from Europe that has different varieties originating in Poland, Russia and Sweden. Composed primarily of water and ethanol, it is traditionally distilled from fermented cereal grains, and more recently, potatoes. It is traditionally drunk neat or freezer chill but can also be served in cocktails or mixed drinks.",
                            ImageName = "vodka.jpg"
                        },
                        new ProductType
                        {
                            Name = "Brandy",
                            Description = "Produced by distilling wine, Brandy generally contains 35-60% alcohol by volume and is typically consumed as an after-dinner digestif to aid digestion. They come is many varieties across the winemaking world with the most renowned ones being Cognac and Armagnac from southwestern France.",
                            ImageName = "brandy.jpg"
                        },
                        new ProductType
                        {
                            Name = "Tequila",
                            Description = "Distilled from the blue agave plants primarily in the area surrounding the city it is named after, Tequila is traditionally drank neat, but is commonly consumed witha side of sangrita, grenadine and hot chili.",
                            ImageName = "tequila.jpg"
                        },
                        new ProductType
                        {
                            Name = "Gin",
                            Description = "Gin is a distilled alcoholic drink that derives its predominant flavour from juniper berries. With its origins as a medicinal liquour made by monks and alchemists across Europe to rpovide aqua vita, it has since become an object of commerce in the spirits industry.",
                            ImageName = "gin.jpg"
                        },
                        new ProductType
                        {
                            Name = "Rum",
                            Description = "Rum is made by fermenting and then distilling sugarcane molasses or juice. The distillate from the process is aged in oak barrels and becomes rum. There are different grades of wine, the light rums commonly used in cocktails, the dark rums can be consumed neat or on the rocks or in cooking and mixers, while the premium rums are consumed neat or on the rocks.",
                            ImageName = "rum.jpg"
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
