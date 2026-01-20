using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pos : BaseEntityId
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string DaysClosed { get; set; } // what to do with this?
        public TimeSpan MorningOpening { get; set; }
        public TimeSpan MorningClosing { get; set; }
        public TimeSpan AfternoonOpening { get; set; }
        public TimeSpan AfternoonClosing { get; set; }
        public DateTime InsertDate { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int ConnectionTypeId { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
    }
}
