namespace Persistence.Migrations
{
    using Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppDbContext context)
        {
            if (!context.Users.Any()) CreateUsers(context);

            base.Seed(context);

        }


        private void CreateUsers(AppDbContext context)
        {
            var users = new List<User>()
            {
                new User {
                    Name = "ahmed",
                    Email = "admin@test.com",
                    IsEnabled = true,
                    Login = "admin",
                    PasswordHash = setPassword("admin"),
                    Telephone = "123",
                    UserType =  new UserType(){Type = "admin"} 
                },
                new User {
                    Name = "guest",
                    Email = "user@test.com",
                    IsEnabled = true,
                    Login = "user",
                    PasswordHash = setPassword("user"),
                    Telephone = "123",
                    UserType =  new UserType(){Type = "user"} 
                }
            };

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();
        }

        private string setPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
