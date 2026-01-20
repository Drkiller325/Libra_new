using Application.Interfaces;
using Application.Poses.ViewModels;
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

namespace Application.Poses.Commands
{
    public class AddPosCommand : IRequest<bool>
    {
        public AddPosViewModel Data { get; set; }
    }

    public class AddPosCommandHandler : IRequestHandler<AddPosCommand, bool>
    {
        private readonly IAppDbContext _context;

        public AddPosCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(AddPosCommand request, CancellationToken cancellationToken)
        {
            var model = request.Data;

            if (model == null) return false;

            bool ifExists = _context.Pos.Any(x => x.Name == model.Name);

            if (ifExists) throw new Exception("Pos Already Exists");

            var City = await _context.Cities.FirstOrDefaultAsync(x => x.Id == model.CityId);
            var ConnType = await _context.ConnectionTypes.FirstOrDefaultAsync(x => x.Id == model.ConnectionTypeId);

            var selectedDays = string.Join(",", model.ClosingDays
                                                     .Where(x => x.IsChecked)
                                                     .Select(x => x.Day)
                                                     .ToList());
            var pos = new Pos
            {
                Name = model.Name,
                Telephone = model.Telephone,
                Cellphone = model.Cellphone,
                Address = model.Address,
                Brand = model.Brand,
                Model = model.Model,
                MorningOpening = model.MorningOpening,
                MorningClosing = model.MorningClosing,
                AfternoonOpening = model.AfternoonOpening,
                AfternoonClosing = model.AfternoonClosing,
                InsertDate = DateTime.Now,
                DaysClosed = selectedDays,
                City = City,
                ConnectionType = ConnType
            };

            _context.Pos.AddOrUpdate(pos);

            if (await _context.SaveChangesAsync(cancellationToken) == 1) return true;

            return false;
        }
    }
}
