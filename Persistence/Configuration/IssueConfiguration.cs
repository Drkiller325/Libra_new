using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class IssueConfiguration : EntityTypeConfiguration<Issue>
    {
        public IssueConfiguration() 
        {
            HasKey(x => x.Id);

            HasRequired(x => x.CreatedByUser)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.CreatedBy);

            HasRequired(x => x.LastModifiedByUser)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.LastModifiedBy);

            HasRequired(x => x.Type)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.TypeId);

            HasRequired(x => x.SubType)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.SubTypeId);

            HasRequired(x => x.Status)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.StatusId);

            HasRequired(x => x.Assigned)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.AssignedId);

            HasRequired(x => x.Pos)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.PosId);


        }
    }
}
