using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;
using Persistence.Configuration;
using Persistence.Migrations;
using Application.Interfaces;

namespace Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext() : base("DefaultConnection") 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueStatus> Statuses { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<Pos> Pos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ConnectionType> ConnectionTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
