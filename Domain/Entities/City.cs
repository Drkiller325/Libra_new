using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City : BaseEntityId
    {
        public string CityName { get; set; }

        public ICollection<Pos> Poses { get; set; } = new HashSet<Pos>();
    }
}
