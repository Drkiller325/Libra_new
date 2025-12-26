using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Domain.Entities;
using System.Runtime.CompilerServices;


namespace Persistence.Configuration
{
    class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(x => x.Id);

            this.Property(x => x.Name).HasMaxLength(128).IsRequired();

            this.Property(x => x.Login).HasMaxLength(10).IsRequired();

            this.HasIndex(x => x.Login).IsUnique();

            this.Property(x => x.Email).HasMaxLength(128).IsRequired();

            this.HasIndex(x => x.Email).IsUnique();

            this.HasRequired(x => x.UserType)
                .WithMany(x => x.users)
                .HasForeignKey(x => x.UserTypeId);
        }
    }
}
