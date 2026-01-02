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
using System.Threading;

namespace Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext() : base("DefaultConnection") 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        public void Save()
        {
            SaveChanges();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
        }
    }
}
