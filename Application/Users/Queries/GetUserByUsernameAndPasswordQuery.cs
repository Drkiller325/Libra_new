using Application.Interfaces;
using Application.Users.ViewModels;
using Domain.Entities;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUserByUsernameAndPasswordQuery : IRequest<UserByNameAndPasswordViewModel>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class GetUserByUsernameAndPasswordHandler : IRequestHandler<GetUserByUsernameAndPasswordQuery, UserByNameAndPasswordViewModel>
    {
        private readonly IAppDbContext _context;

        public GetUserByUsernameAndPasswordHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<UserByNameAndPasswordViewModel> Handle(GetUserByUsernameAndPasswordQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.Include(x => x.UserType)
                    .FirstOrDefaultAsync(x => x.Login.ToUpper() == request.Login.ToUpper(), cancellationToken);

                if (user == null) return null;

                if (!checkPassword(request.Password, user.PasswordHash)) return null;

                var userByUsernameAndPasswordDto = new UserByNameAndPasswordViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
                    Password = user.PasswordHash,
                    Email = user.Email,
                    Telephone = user.Telephone,
                    UserType = user.UserType.Type,
                    IsEnabled = user.IsEnabled
                };
                return userByUsernameAndPasswordDto;
            }
            catch (Exception e)
            {
                throw new Exception("Error in User query" + e.Message);
            }
        }

        private bool checkPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }


}
