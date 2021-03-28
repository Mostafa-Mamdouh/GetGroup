using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using GetGroup.Core.Entities;
using Microsoft.Extensions.Logging;

namespace GetGroup.Infrastructure.Data
{
   public class GetGroupSeed
    {
        public static async Task SeedAsync(GetGroupContext context, ILoggerFactory loggerFactory)
        {
            try
            {
               
                if (!context.Services.Any())
                {
                    var servicesData = File.ReadAllText("../Infrastructure/Data/SeedData/services.json");
                    var services = JsonSerializer.Deserialize<List<Service>>(servicesData);

                    foreach (var item in services)
                    {
                        item.CreateDate = DateTime.Now;
                        context.Services.Add(item);
                    }

                    await context.SaveChangesAsync();
                }



            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<GetGroupSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
