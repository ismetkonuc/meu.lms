using System.Collections.Generic;
using System.Linq;
using meu.lms.business.Interfaces;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Task = System.Threading.Tasks.Task;

namespace meu.lms.api.Initializers
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IAppUserService _appUserService)
        {
            var instructorRole = await roleManager.FindByNameAsync("Instructor");
            if (instructorRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Instructor" });
            }

            var studentRole = await roleManager.FindByNameAsync("Student");
            if (studentRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Student" });
            }

            var adminUser = await userManager.FindByNameAsync("ismet");
            if (adminUser == null)
            {
                AppUser user = new AppUser
                {
                    Name = "İsmet",
                    Surname = "Konuç",
                    Email = "ismetkonuc@gmail.com",
                    UserName = "ismet"
                };
                await userManager.CreateAsync(user, "1");
                await userManager.AddToRoleAsync(user, "Instructor");
            }


            if (!_appUserService.GetStundets().Any())
            {

                List<AppUser> users = new List<AppUser>()
                {
                    new AppUser()
                    {
                        Name = "Sedat",
                        Surname = "Başpınar",
                        Email = "sedatbaspinar@gmail.com",
                        UserName = "sedat",
                    },
                    new AppUser()
                    {
                        Name = "Süleyman",
                        Surname = "Sezer",
                        Email = "suleymansezer@gmail.com",
                        UserName = "suleyman",
                    },
                    new AppUser()
                    {
                        Name = "Berkay",
                        Surname = "Türkoğlu",
                        Email = "berkayturkoglu@gmail.com",
                        UserName = "berkay",
                    },
                    new AppUser()
                    {
                        Name = "Elgin Şevval",
                        Surname = "Erkoç",
                        Email = "elginerkoc@gmail.com",
                        UserName = "elgin",
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "1");
                    await userManager.AddToRoleAsync(user, "Student");
                }
                
            }



        }
    }
}