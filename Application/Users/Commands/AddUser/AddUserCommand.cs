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

namespace Application.Users.Commands.AddUser
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

            if(ifExists) throw new Exception("User Already Exists");

            var Role = await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == request.Data.UserTypeId);

            var user = new User(model.Name, model.Email, model.IsEnabled, model.Login, model.Password, model.Telephone, Role);

            _context.Users.AddOrUpdate(user);

            if (await _context.SaveChangesAsync(cancellationToken) == 1) return true;

            return false;
        }

    }
}
