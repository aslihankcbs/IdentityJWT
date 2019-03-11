using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityJWT.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            //DbContext i context e atadık
            var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            //user i yönetmek için
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //rol yönetimi
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //admin isminde bir rol oluşturduk
            string rolAdmin = "admin";
            string rolEditor = "editor";

            //vt yoksa vt yi oluşturuyor
            context.Database.EnsureCreated();

            //identity user dan gelen bilgiler
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "aslihan",
                Email = "kocabasasli@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //user tablosu boşsa
            if (context.Users.Any() == false)
            {
                //eklediğimiz user nesnesini vtye ekledik
                await userManager.CreateAsync(user, "@Password123");


                if (!context.Roles.Any())
                {
                    //vt de admin ve editor isminde 2 tane rol oluşturmus olduk
                    if (await roleManager.FindByNameAsync(rolAdmin) == null)
                    {
                        //rol manager boşşa burada oluştur
                        await roleManager.CreateAsync(new IdentityRole(rolAdmin));
                    }

                    if (await roleManager.FindByNameAsync(rolEditor) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(rolEditor));
                    }
                    //oluşturduğumuz user artık admin rolunde
                    await userManager.AddToRoleAsync(user, rolAdmin);

                }
            }
        }
    }
}
