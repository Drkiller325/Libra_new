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
using Domain.Common;
using Domain.Interfaces;
using System.Security.Claims;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public AppDbContext() : base("DefaultConnection") { }
      

        public AppDbContext(ICurrentUserService currentUserService, IDateTime dateTime) : base("DefaultConnection") 
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
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


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntityId>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = _currentUserService.Id;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        entry.Entity.LastModifiedById = _currentUserService.Id;
                        break;

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ConnectionTypeConfiguration());
            modelBuilder.Configurations.Add(new IssueConfiguration());
            modelBuilder.Configurations.Add(new IssueStatusConfiguration());
            modelBuilder.Configurations.Add(new IssueTypeConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new PosConfiguration());
            modelBuilder.Configurations.Add(new UserTypeConfiguration());
        }
    }
}
