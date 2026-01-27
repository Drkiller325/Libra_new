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
    public class GetAllPosQuery : IRequest<IEnumerable<GetPosesViewModel>>
    {
    }

    public class GetAllPosQueryHandler : IRequestHandler<GetAllPosQuery, IEnumerable<GetPosesViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllPosQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GetPosesViewModel>> Handle(GetAllPosQuery request, CancellationToken cancellationToken)
        {
            var Poses = await _context.Pos
                .Include(x => x.Issues)
                .Include(x => x.City)
                .Select(pos => 
                new GetPosesViewModel
            {
                Id = pos.Id,
                Name = pos.Name,
                Address = pos.Address,
                City = pos.City.CityName,
                Telephone = pos.Telephone,
                IssueCount = pos.Issues.Count(),
            }).ToListAsync(cancellationToken);


            return Poses;
        }
    }
}
