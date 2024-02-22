using Microsoft.AspNetCore.Identity;
using ReefStories.Domain.Models.Identity;

namespace ReefStories.DataAccess.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {

                var user = new AppUser
                {
                    DisplayName = "Tomo",
                    Email = "kozoloski@yahoo.com",
                    UserName = "kozoloskit",
                    Address = new Address
                    {
                        FirstName = "Tomo",
                        LastName = "Kozoloski",
                        City = "Skopje",
                        State = "Macedonia",
                        ZipCode = "1000"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
