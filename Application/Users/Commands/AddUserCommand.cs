using Application.Interfaces;
using Application.Users.ViewModels;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class AddUserCommand : IRequest<bool>
    {
        public AddUserViewModel Data { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IAppDbContext _context;

        public AddUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Data;

            if (model == null) return false;

            bool ifExists = _context.Users.Any(x => x.Login == model.Login);
            bool ifExists2 = _context.Users.Any(x => x.Email == model.Email);

            if(ifExists || ifExists2) throw new Exception("User Or Email Already Exists");

            var Role = await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == request.Data.UserTypeId);

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                IsEnabled = true,
                Login = model.Login,
                PasswordHash = setPassword(model.Password),
                Telephone = model.Telephone,
                UserType = Role
            };

            _context.Users.AddOrUpdate(user);

            if (await _context.SaveChangesAsync(cancellationToken) == 1) return true;

            return false;
        }


        private string setPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        

    }
}
