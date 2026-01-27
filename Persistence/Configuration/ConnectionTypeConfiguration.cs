using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class ConnectionTypeConfiguration : EntityTypeConfiguration<ConnectionType>
    {
        public ConnectionTypeConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.ConnType).IsRequired().HasMaxLength(25);
        }
    }
}
