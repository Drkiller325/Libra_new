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
    public class GetUsers : IRequest<IEnumerable<UserViewModel>>
    {
    }

    public class GetUsersHandler : IRequestHandler<GetUsers, IEnumerable<UserViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetUsersHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Include(x => x.UserType).ToListAsync(cancellationToken);

            var userViewModels = new List<UserViewModel>();

            foreach(var user in users)
            {
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
                    Telephone = user.Telephone,
                    Email = user.Email,
                    IsEnabled = user.IsEnabled,
                    UserRole = user.UserType.Type
                };
                userViewModels.Add(userViewModel);
                
            }

            return userViewModels;
        }
    }
}
