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
            HasKey(x => x.Id);

            Property(x => x.Name).HasMaxLength(128).IsRequired();

            Property(x => x.Login).HasMaxLength(10).IsRequired();

            HasIndex(x => x.Login).IsUnique();

            Property(x => x.Email).HasMaxLength(128).IsRequired();

            HasIndex(x => x.Email).IsUnique();

            Property(x => x.Telephone).IsOptional();

            HasRequired(x => x.UserType)
                .WithMany(x => x.users)
                .HasForeignKey(x => x.UserTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
