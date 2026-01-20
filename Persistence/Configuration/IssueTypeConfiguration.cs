using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class IssueTypeConfiguration : EntityTypeConfiguration<IssueType>
    {
        public IssueTypeConfiguration()
        {
            HasKey(x => x.Id);

            Property(x => x.IssueLevel).IsRequired();

            Property(x => x.Name).IsRequired().HasMaxLength(50);

            Property(x => x.ParentIssueId).IsOptional();
        }
    }
}
