using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class IssueStatusConfiguration : EntityTypeConfiguration<IssueStatus>
    {
        public IssueStatusConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.Status).IsRequired().HasMaxLength(25);
        }
    }
}
