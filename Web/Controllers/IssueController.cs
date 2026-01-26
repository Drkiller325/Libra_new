using Application.Issues.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly IMediator _mediator;
        public IssueController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: Issue
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetIssues(CancellationToken cancellationToken)
        {
            try
            {
                var issues = await _mediator.Send(new GetIssuesGridQuery() { }, cancellationToken);

                return Json(issues, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }

        }

        public ActionResult GetAddIssue()
        {
            return View("_GetAddIssue");
        }
    }
}