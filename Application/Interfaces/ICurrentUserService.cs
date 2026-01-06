using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        int Id { get; }

        string Login { get; }

        string Role { get; }
    }
}
