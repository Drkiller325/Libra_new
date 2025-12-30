using MediatR;
using Microsoft.Owin.Security;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Users.ViewModels;
using FluentValidation;
using Application.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private IAuthenticationManager authenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IValidator<LoginUserViewModel> _loginValidator;

        public AccountController(IMediator mediator, IAppDbContext context, IValidator<LoginUserViewModel> loginValidator)
        {
            _mediator = mediator;
            _loginValidator = loginValidator;
        }

        [AllowAnonymous]
        // GET: Account
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}