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
                new User("ahmed", "admin@test.com", true, "admin", "admin", "123", new UserType(){Type = "admin"}),
                new User("guest", "user@test.com", true, "user", "user", "123", new UserType(){Type = "user"})
            };

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();
        }
    }
}
