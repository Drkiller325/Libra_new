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
    public class GetAllPosQuery : IRequest<IEnumerable<PosViewModel>>
    {
    }

    public class GetAllPosQueryHandler : IRequestHandler<GetAllPosQuery, IEnumerable<PosViewModel>>
    {
        private readonly IAppDbContext _context;

        public GetAllPosQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PosViewModel>> Handle(GetAllPosQuery request, CancellationToken cancellationToken)
        {
            var Pos = await _context.Pos.Include(x => x.Issues).Include(x => x.City).ToListAsync(cancellationToken);

            var PosViewModels = new List<PosViewModel>();

            foreach(var pos in Pos)
            {
                var posViewModel = new PosViewModel
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
