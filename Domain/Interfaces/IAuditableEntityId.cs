using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuditableEntityId : IBaseEntityId<int>
    {
        int CreatedBy { get; set; }
        DateTime Created {  get; set; }
        int? LastModifiedBy { get; set; }
        DateTime? LastModified { get; set; }
    }
}
