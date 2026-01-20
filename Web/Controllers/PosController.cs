using Application.Poses.Commands;
using Application.Poses.Queries;
using Application.Poses.ViewModels;
using Application.Users.Queries;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Libra.Controllers
{
    [Authorize]
    public class PosController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddPosViewModel> _AddPosValidator;

        public PosController(IMediator mediator, IValidator<AddPosViewModel> AddPosValidator)
        {
            _mediator = mediator;
            _AddPosValidator = AddPosValidator;
        }


        // GET: Pos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetPoses(CancellationToken cancellationToken)
        {

            try
            {
                var Poses = await _mediator.Send(new GetAllPosQuery() { });

                return Json(Poses, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }
            
        }


        [HttpGet]
        public async Task<ActionResult> GetAddPos()
        {
            var Cities = await _mediator.Send(new GetAllCitiesQuery() { });
            var ConnTypes = await _mediator.Send(new GetAllConnectionTypes() { });

            List<DayViewModel> Days = new List<DayViewModel>
            {
                new DayViewModel { Day = "Sun" },
                new DayViewModel { Day = "Mon" },
                new DayViewModel { Day = "Tue" },
                new DayViewModel { Day = "Wed" },
                new DayViewModel { Day = "Thu" },
                new DayViewModel { Day = "Fri" },
                new DayViewModel { Day = "Sat" }
            };

            var model = new AddPosViewModel
            {
                ClosingDays = Days
            };

            ViewBag.ConnectionTypes = new SelectList(ConnTypes, "Id", "Type");
            ViewBag.Cities = new SelectList(Cities, "Id", "City");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPos(AddPosViewModel model)
        {
            var validationResult = _AddPosValidator.Validate(model);

            if(!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var Cities = await _mediator.Send(new GetAllCitiesQuery() { });
                var ConnTypes = await _mediator.Send(new GetAllConnectionTypes() { });

                ViewBag.ConnectionTypes = new SelectList(ConnTypes, "Id", "Type");
                ViewBag.Cities = new SelectList(Cities, "Id", "City");

                return View(model);
            }
            else
            {
                try
                {
                    var addPos = await _mediator.Send(new AddPosCommand
                    {
                        Data = model
                    });

                    if(addPos)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Json(new { StatusCode = 500, message = "A problem on the server occured. Try again" });
                    }
                }
                catch
                {
                    return Json(new { success = false, message = "A problem on the server occured. Try again!" });
                }
            }
        }
    }
}