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
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddUserViewModel> _userValidator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
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

            ViewBag.UserRoles = new SelectList(roles, "Id", "Type");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(AddUserViewModel model)
        {
            var validationResult = _userValidator.Validate(model);

            if(!validationResult.IsValid)
            {
                var erorrs = validationResult.Errors.GroupBy(x => x.PropertyName).ToDictionary(g => g.Key, g => g.First().ErrorMessage);
                return Json(new { success = false, erorrs });
            }

            try
            {
                var addUser = await _mediator.Send(new AddUserCommand
                {
                    Data = model
                });

                if(addUser)
                {
                    return Json(new { StatusCode = 201, message = "User was successfully created." });
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