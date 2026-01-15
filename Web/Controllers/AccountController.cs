using Application.Interfaces;
using Application.Users.Queries;
using Application.Users.ViewModels;
using FluentValidation;
using MediatR;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginUserViewModel model)
        {
            var validatorResponse = _loginValidator.Validate(model);

            if (!validatorResponse.IsValid)
            {
                foreach (var error in validatorResponse.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            else
            {

                var user = await _mediator.Send(new GetUserByUsernameAndPasswordQuery
                {
                    Login = model.Login,
                    Password = model.Password,
                });

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Username and Password");
                    return View();
                }
                else if (user.IsEnabled == false)
                {
                    ModelState.AddModelError("", "Your account is Deactivated.");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserType, ClaimValueTypes.String));

                    authenticationManager.SignOut();
                    authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

}