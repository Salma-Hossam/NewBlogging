using System.Collections.Generic;
using YakeenBlog.BLL;
using YakeenBlog.Models.EFModels;

namespace YakeenBlog.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using YakeenBlog.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<YakeenBlog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "YakeenBlog.Models.ApplicationDbContext";
        }

        protected override void Seed(YakeenBlog.Models.ApplicationDbContext context)
        {
            List<string> roles = new List<string>()
                {"Admin" , "User"};
            List<string> categories = new List<string>()
                { "Travels", "Lifestyle", "Fashion", "Music", "Health" , "Video", "Sports" , "Technology"};

            
            //Create roles in database
            foreach (var item in roles)
            {
                IdentityRole role = context.Roles
                    .FirstOrDefault(c => c.Name.ToLower() == item.ToLower());
                if (role == null)
                {
                    role = new IdentityRole();
                    role.Name = item;
                    context.Roles.Add(role);
                }
            }
            

            //Create categories in database
            foreach (var item in categories)
            {
                
                Category category = context.Categories
                    .FirstOrDefault(c => c.Name.ToLower() == item.ToLower());
                if (category == null)
                {
                    category = new Category();
                    category.Name = item;
                    context.Categories.Add(category);
                }
            }
            context.SaveChanges();

            
        }
    }
}
