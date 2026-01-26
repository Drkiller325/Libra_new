using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Poses.Commands
{
    public class DeletePosCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeletePosCommandHandler : IRequestHandler<DeletePosCommand>
    {
        private readonly IAppDbContext _context;

        public DeletePosCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeletePosCommand request, CancellationToken cancellationToken)
        {
            Pos todelete = await _context.Pos.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            List<Issue> issues = await _context.Issues.Where(x => x.PosId == request.Id).ToListAsync(cancellationToken);

            List<Log> logs = new List<Log>();

            foreach(var issue in issues)
            {
                logs.AddRange(await _context.Logs.Where(x => x.IssueId == issue.Id).ToListAsync(cancellationToken));
            }

            if(logs.Count > 0)
            {
                _context.Logs.RemoveRange(logs);
            }
            
            if(issues.Count > 0)
            {
                _context.Issues.RemoveRange(issues);
            }

            _context.Pos.Remove(todelete);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
