using Application.Users.Commands.AddUser;
using Application.Users.Queries;
using Application.Users.ViewModels;
using FluentValidation;
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
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddUserViewModel> _userValidator;

        public AdminController(IMediator mediator, IValidator<AddUserViewModel> userValidator)
        {
            _mediator = mediator;
            _userValidator = userValidator;
        }
        // GET: Admin
        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _mediator.Send(new GetUsers() { }, cancellationToken);

                return View(users);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }
            
        }


        public async Task<ActionResult> AddUser(CancellationToken cancellationToken)
        {

            var roles = await _mediator.Send(new GetUserRoles() { }, cancellationToken);

            ViewBag.UserRoles = new SelectList(roles, "Id", "Role");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(AddUserViewModel model)
        {
            var validationResult = _userValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var roles = await _mediator.Send(new GetUserRoles() { });
                ViewBag.UserRoles = new SelectList(roles, "Id", "Role");
                return View(model);
            }
            else
            {
                try
                {
                    var addUser = await _mediator.Send(new AddUserCommand
                    {
                        Data = model
                    });

                    if (addUser)
                    {
                        //return Json(new { StatusCode = 201, message = "User was successfully created." });
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