using Application.Interfaces;
using Application.Poses.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Poses.Queries
{
    public class GetAllConnectionTypes : IRequest<IEnumerable<ConnectionTypeViewModel>>
    {
    }

    public class GetAllConnectionTypesHandler : IRequestHandler<GetAllConnectionTypes, IEnumerable<ConnectionTypeViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllConnectionTypesHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ConnectionTypeViewModel>> Handle(GetAllConnectionTypes request, CancellationToken cancellationToken)
        {
            var connTypes = await _context.ConnectionTypes.Select(connType => new ConnectionTypeViewModel
            {
                Id = connType.Id,
                Type = connType.ConnType
            }).ToListAsync(cancellationToken);

            
            return connTypes;
        }
    }
}
