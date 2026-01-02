using Application.Interfaces;
using Application.Users.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public LoginUserViewModel Data { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAppDbContext _context;

        public LoginUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
