using Application.Interfaces;
using Application.Poses.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Poses.Commands
{
    public class EditPosCommand : IRequest<bool>
    {
        public EditPosViewModel Data { get; set; }
    }


    public class EditPosCommandHandler : IRequestHandler<EditPosCommand, bool>
    {
        private readonly IAppDbContext _context;

        public EditPosCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public Task<bool> Handle(EditPosCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
