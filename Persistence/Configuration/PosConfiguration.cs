using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class PosConfiguration : EntityTypeConfiguration<Pos>
    {
        public PosConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Name).IsRequired().HasMaxLength(50);

            Property(x => x.Telephone).IsOptional().HasMaxLength(10);

            Property(x => x.Cellphone).IsOptional().HasMaxLength(10);

            Property(x => x.Address).IsRequired().HasMaxLength(100);

            Property(x => x.Model).IsRequired().HasMaxLength(25);

            Property(x => x.Brand).IsOptional().HasMaxLength(25);

            HasRequired(x => x.City)
                .WithMany(x => x.Poses)
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.ConnectionType)
                .WithMany(x => x.Poses)
                .HasForeignKey(x => x.ConnectionTypeId)
                .WillCascadeOnDelete(false);


        }
    }
}
