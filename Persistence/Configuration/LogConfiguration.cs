using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration() 
        {
            HasKey(x => x.Id);

            Property(x => x.Action).IsRequired().HasMaxLength(50);

            Property(x => x.Notes).IsOptional().HasMaxLength(300);

            HasRequired(x => x.Issue)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.IssueId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.User)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
