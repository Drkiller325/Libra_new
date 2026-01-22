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
    public class GetPosByIdQuery : IRequest<PosViewModel>
    {
        public int Id { get; set; }
    }

    public class GetPosByIdQueryHandler : IRequestHandler<GetPosByIdQuery, PosViewModel>
    {
        private readonly IAppDbContext _context;

        public GetPosByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<PosViewModel> Handle(GetPosByIdQuery request, CancellationToken cancellationToken)
        {
            var pos = await _context.Pos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (pos == null) throw new Exception("Pos not Found");

            var result = new PosViewModel
            {
                Id = pos.Id,
                Name = pos.Name,
                Telephone = pos.Telephone,
                Cellphone = pos.Cellphone,
                Address = pos.Address,
                Brand = pos.Brand,
                Modeel = pos.Model,
                CityId = pos.CityId,
                ConnectionTypeId = pos.ConnectionTypeId,
                MorningOpening = pos.MorningOpening,
                MorningClosing = pos.MorningClosing,
                AfternoonOpening = pos.AfternoonOpening,
                AfternoonClosing = pos.AfternoonClosing,
                ClosingDays = pos.DaysClosed
            };

            return result;
        }

    }
}
