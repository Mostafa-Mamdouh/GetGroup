using GetGroup.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace GetGroup.Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {

            if (userManager.Users.Count()==0)
            {
                var user = new AppUser
                {
                    FirstName = "Mostafa",
                    LastName = "Mamdouh",
                    UserName = "admin",
                    Email = "admin@yahoo.com",
                    PhoneNumber = "01287349897"
                };

                await userManager.CreateAsync(user, "P@ssw0rd");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}