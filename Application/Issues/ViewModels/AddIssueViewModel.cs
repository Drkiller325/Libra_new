using Application.Poses.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.ViewModels
{
    public class AddIssueViewModel : IRequest
    {
        public IEnumerable<PosesGridViewModel> Pos { get; set; }
        public int TypeId { get; set; }
        public int SubTypeId { get; set; }
        public int ProblemId { get; set; }
        public IEnumerable<Priority> PriorityList { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }
        public string Solution { get; set; }
        public int AssignedToId { get; set; }
        public string Memo { get; set; }


    }
}
