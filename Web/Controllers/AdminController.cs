using Application.Users.Commands;
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
        private readonly IValidator<EditUserViewModel> _editUserValidator;

        public AdminController(IMediator mediator, IValidator<AddUserViewModel> userValidator, IValidator<EditUserViewModel> editUserValidator)
        {
            _mediator = mediator;
            _userValidator = userValidator;
            _editUserValidator = editUserValidator;
        }
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
            
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers(CancellationToken cancellationToken)
        {
            try
            {
                var users = await _mediator.Send(new GetAllUsersQuery() { }, cancellationToken);

                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot send request via mediator" + ex);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAddUser(CancellationToken cancellationToken)
        {

            var roles = await _mediator.Send(new GetUserRolesQuery() { }, cancellationToken);

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

                var roles = await _mediator.Send(new GetUserRolesQuery() { });
                ViewBag.UserRoles = new SelectList(roles, "Id", "Role");
                return View("GetAddUser",model);
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

        [HttpGet]
        public async Task<ActionResult> GetUserDetails(int Id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery() { Id = Id });
            var roles = await _mediator.Send(new GetUserRolesQuery() { });
            ViewBag.UserRoles = new SelectList(roles, "Id", "Role");

            if (user == null) return View("Error");
            else
                return View(user);
        }

        [HttpGet]
        public async Task<ActionResult> GetEditUser(int Id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery() { Id = Id });
            var roles = await _mediator.Send(new GetUserRolesQuery() { });
            ViewBag.UserRoles = new SelectList(roles, "Id", "Role");
            if (user == null) return View("Error");
            else
                return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(EditUserViewModel model)
        {
            var validationResult = _editUserValidator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                var roles = await _mediator.Send(new GetUserRolesQuery() { });
                ViewBag.UserRoles = new SelectList(roles, "Id", "Role");
                return View("GetEditUser", model);
            }
            else
            {
                try
                {
                    var editUser = await _mediator.Send(new EditUserCommand()
                    {
                        Data = model
                    });

                    if (editUser) return RedirectToAction("Index");
                    else
                    {
                        ViewBag.Erorr = "An error occured in the server";
                        return RedirectToAction($"GetEditUser/{model.Id}");
                    }

                }
                catch(Exception e)
                {
                    ViewBag.Error = e.Message;
                    return RedirectToAction($"GetEditUser/{model.Id}");
                }
            }
        }
            
    }
}