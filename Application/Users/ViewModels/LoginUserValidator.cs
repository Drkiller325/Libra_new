using Application.Users.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Commands.LoginUser
{
    public class LoginUserValidator : AbstractValidator<LoginUserViewModel>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Username cannot be empty")
                .NotNull().WithMessage("Username cannot be null")
                .MinimumLength(4).WithMessage("Username must have at least 4 characters")
                .MaximumLength(10).WithMessage("Username can't exceed 20 characters");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .NotNull().WithMessage("Password cannot be null")
                .MinimumLength(4).WithMessage("Password must have at least 8 characters");
        }
    }
}
