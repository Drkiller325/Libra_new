using Application.Issues.Queries;
using Application.Issues.ViewModels;
using Application.Poses.Queries;
using Application.Users.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddIssueViewModel> _AddIssueValidator;
        public IssueController(IMediator mediator, IValidator<AddIssueViewModel> AddIssueValidator)
        {
            _mediator = mediator;
            _AddIssueValidator = AddIssueValidator;
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

        [HttpGet]
        public ActionResult GetAddIssue()
        {
            return View("_GetAddIssue");
        }

        public async Task<ActionResult> GetIssuePos(int id)
        {
            try
            {
                var pos = await _mediator.Send(new GetPosByIdGridQuery() { Id = id });

                return Json(pos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetEditIssuePos(int id)
        {
            var pos = await _mediator.Send(new GetPosByIdGridQuery() { Id = id });
            var priorityList = await _mediator.Send(new GetPriorityListQuery());
            var IssueTypes = await _mediator.Send(new GetIssueTypesQuery());
            var IssueStatuses = await _mediator.Send(new GetIssueStatusesQuery());
            var AssignedTo = await _mediator.Send(new GetUserRolesQuery());

            ViewBag.Statuses = new SelectList(IssueStatuses, "Id", "Status"); ;
            ViewBag.Types = new SelectList(IssueTypes, "Id", "Type");
            ViewBag.Assigned = new SelectList(AssignedTo, "Id", "Role");
            AddIssueViewModel model = new AddIssueViewModel
            {
                PosId = id,
                Pos = pos,
                PriorityList = priorityList
            };
            return View("_GetAddPosIssue", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetSubTypes(int typeId)
        {
            try
            {
                var subTypes = await _mediator.Send(new GetIssueSubTypesQuery() { Id = typeId });

                return Json(subTypes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }


        }

        [HttpGet]
        public async Task<JsonResult> GetProblems(int typeId)
        {
            try
            {
                var Problems = await _mediator.Send(new GetIssueProblemQuery() { Id = typeId });

                return Json(Problems, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddIssue(AddIssueViewModel model)
        {
            var validationResult = _AddIssueValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var IssueTypes = await _mediator.Send(new GetIssueTypesQuery());
                var IssueStatuses = await _mediator.Send(new GetIssueStatusesQuery());
                var AssignedTo = await _mediator.Send(new GetUserRolesQuery());
                var priorityList = await _mediator.Send(new GetPriorityListQuery());
                var pos = await _mediator.Send(new GetPosByIdGridQuery() { Id = model.PosId });

                model.Pos = pos;
                model.PriorityList = priorityList;
                ViewBag.Statuses = new SelectList(IssueStatuses, "Id", "Status"); ;
                ViewBag.Types = new SelectList(IssueTypes, "Id", "Type");
                ViewBag.Assigned = new SelectList(AssignedTo, "Id", "Role");
                
                return View("_GetAddPosIssue", model);
            }
            else
            {
                try
                {
                    var AddIssue = await _mediator.Send(model);

                    if (AddIssue)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        var IssueTypes = await _mediator.Send(new GetIssueTypesQuery());
                        var IssueStatuses = await _mediator.Send(new GetIssueStatusesQuery());
                        var AssignedTo = await _mediator.Send(new GetUserRolesQuery());
                        var priorityList = await _mediator.Send(new GetPriorityListQuery());
                        var pos = await _mediator.Send(new GetPosByIdGridQuery() { Id = model.PosId });

                        model.Pos = pos;
                        model.PriorityList = priorityList;
                        ViewBag.Statuses = new SelectList(IssueStatuses, "Id", "Status"); ;
                        ViewBag.Types = new SelectList(IssueTypes, "Id", "Type");
                        ViewBag.Assigned = new SelectList(AssignedTo, "Id", "Role");

                        return View("_GetAddPosIssue", model);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Name", e.Message);


                    var IssueTypes = await _mediator.Send(new GetIssueTypesQuery());
                    var IssueStatuses = await _mediator.Send(new GetIssueStatusesQuery());
                    var AssignedTo = await _mediator.Send(new GetUserRolesQuery());
                    var priorityList = await _mediator.Send(new GetPriorityListQuery());

                    model.PriorityList = priorityList;
                    ViewBag.Statuses = new SelectList(IssueStatuses, "Id", "Status"); ;
                    ViewBag.Types = new SelectList(IssueTypes, "Id", "Type");
                    ViewBag.Assigned = new SelectList(AssignedTo, "Id", "Role");

                    return View("_GetAddPosIssue", model);
                }
            }
        }
    }
}