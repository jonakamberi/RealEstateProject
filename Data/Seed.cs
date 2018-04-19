using System;
using System.Collections.Generic;  

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateProject.Models;

namespace RealEstateProject.Data
{
    public class Seed 
    {

        public static void CreatePropertyType(ApplicationDbContext context)
        {
            IList<PropertyType> propertyTypes = new List<PropertyType>();

            int items = 0;

            foreach (var propertyType in context.PropertyType)
            {
                items++;
            }

            // If there are already items, do not seed
            if (items > 0)
            {
                return;
            }

            propertyTypes.Add(new PropertyType() { name = "Apartment" });
            propertyTypes.Add(new PropertyType() { name = "House" });
            propertyTypes.Add(new PropertyType() { name = "Villa" });
            propertyTypes.Add(new PropertyType() { name = "Duplex" });

            context.PropertyType.AddRange(propertyTypes);
            context.SaveChanges();

        }

        public static void CreateProperties(ApplicationDbContext context) {
            IList<Property> properties = new List<Property>();

            int items = 0;

            foreach (var professor in context.Property)
            {
                items++;
            }

            // If there are already items, do not seed
            if (items > 0) {
                return;
            }

            properties.Add(new Property()
            {
                title = "Sample 1",
                description = "Description 1",
                address = "Somewhere 1",
                price = 134049,
                URL = "http://gaitaproperty.com/wp-content/uploads/2015/08/Miami-Property-search-300x200.jpg"
            });

            properties.Add(new Property()
            {
                title = "Sample 2",
                description = "Description 2",
                address = "Somewhere 2",
                price = 504937,
                URL = "http://gaitaproperty.com/wp-content/uploads/2015/08/Miami-Property-search-300x200.jpg"
            });

            properties.Add(new Property()
            {
                title = "Sample 3",
                description = "Description 3",
                address = "Somewhere 3",
                price = 443344,
                URL = "http://gaitaproperty.com/wp-content/uploads/2015/08/Miami-Property-search-300x200.jpg"
            });

            context.Property.AddRange(properties);
            context.SaveChanges();

        }

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            //adding customs roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //creating a super user who could maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("AppSettings")["UserEmail"],
                Email = Configuration.GetSection("AppSettings")["UserEmail"]
            };

            string userPassword = Configuration.GetSection("AppSettings")["UserPassword"];
            var user = await UserManager.FindByEmailAsync(Configuration.GetSection("AppSettings")["UserEmail"]);

            if (user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
        }
    }
}
