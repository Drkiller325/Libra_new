using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserType : BaseEntityId
    {
        public string Type { get; set; }
        public ICollection<User> users { get; set; } = new HashSet<User>();
        public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
    }
}
