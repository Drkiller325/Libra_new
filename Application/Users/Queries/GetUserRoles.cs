using Application.Interfaces;
using Application.Users.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUserRoles : IRequest<IEnumerable<RoleViewModel>>
    {
    }

    public class GetUserRolesHandler : IRequestHandler<GetUserRoles, IEnumerable<RoleViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetUserRolesHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RoleViewModel>> Handle(GetUserRoles request, CancellationToken cancellationToken)
        {
            var roles = await _context.UserTypes.ToListAsync(cancellationToken) ;

            var resultRoles = new List<RoleViewModel>();

            foreach(var role in roles)
            {
                var temp = new RoleViewModel
                {
                    Id = role.Id,
                    Role = role.Type
                };
                resultRoles.Add(temp);
            }

            return resultRoles;
        }
    }
}
