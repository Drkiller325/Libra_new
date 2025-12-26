using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>();

            user.HasKey(u => u.Id);

            user.Property(u => u.Name)
                .HasMaxLength(20)
                .IsRequired();

            user.Property(u => u.Telephone)
                .HasMaxLength(9)
                .IsFixedLength()
                .IsRequired();

            user.Property(u => u.Login)
                .HasMaxLength(20)
                .IsRequired();
            user.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            user.HasRequired(t => t.UserType);

            modelBuilder.Entity<UserType>().HasMany(u => u.users);

            base.OnModelCreating(modelBuilder);
        }
    }
}
