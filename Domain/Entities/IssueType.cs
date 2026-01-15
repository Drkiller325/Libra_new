using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class IssueType : BaseEntityId
    {
        public int IssueLevel { get; set; }
        public string Name { get; set; }
        public int? ParentIssueId { get; set; }

        public ICollection<Issue> SubTypes { get; set; } = new HashSet<Issue>();
        public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
        public ICollection<Issue> Problems { get; set; } = new HashSet<Issue>();

        public DateTime InsertDate { get; set; }
    }
}
