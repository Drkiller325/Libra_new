using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntityId
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get;  set; }
        public string Telephone { get;  set; }
        public bool IsEnabled { get;  set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public ICollection<Log> Logs { get; set; } = new HashSet<Log>();
        public ICollection<Issue> IssuesCreated { get; set; } = new HashSet<Issue>();
        public ICollection<Issue> IssuesModefied { get; set; } = new HashSet<Issue>();

        
    }
}
