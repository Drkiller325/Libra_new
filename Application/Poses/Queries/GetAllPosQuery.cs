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
    public class GetAllPosQuery : IRequest<IEnumerable<GetPosViewModel>>
    {
    }

    public class GetAllPosQueryHandler : IRequestHandler<GetAllPosQuery, IEnumerable<GetPosViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllPosQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<GetPosViewModel>> Handle(GetAllPosQuery request, CancellationToken cancellationToken)
        {
            var Pos = await _context.Pos.Include(x => x.Issues).Include(x => x.City).ToListAsync(cancellationToken);

            var PosViewModels = new List<GetPosViewModel>();

            foreach(var pos in Pos)
            {
                var posViewModel = new GetPosViewModel
                {
                    Id = pos.Id,
                    Name = pos.Name,
                    Address = pos.Address,
                    City = pos.City.CityName,
                    Telephone = pos.Telephone,
                    IssueCount = pos.Issues.Count(),
                };

                PosViewModels.Add(posViewModel);
            }

            return PosViewModels;
        }
    }
}
