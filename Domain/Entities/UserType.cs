using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<User> users { get; set; }
    }
}
