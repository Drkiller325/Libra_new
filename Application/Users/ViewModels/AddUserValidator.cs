using Application.Interfaces;
using Application.Users.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Users.Commands.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserViewModel>
    {

        public AddUserValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("This Field is Required")
                .MinimumLength(5).WithMessage("Name Should at least be 5 characters Long")
                .MaximumLength(50).WithMessage("Name is too Long")
                .Must(BeValidName).WithMessage("Name can Only contain letters")
                .WithName("Name");

            RuleFor(x => x.Email)
                .NotEmpty().NotNull().WithMessage("This Field is Required")
                .MaximumLength(50).WithMessage("email can have maximum 50 characters")
                .EmailAddress().WithMessage("Invalid Email")
                .WithName("Email");

            RuleFor(x => x.Password)
                .NotEmpty().NotNull().WithMessage("This Field is Required")
                .MaximumLength(50).WithMessage("Password can have maximum 50 characters")
                .Must(BeValidPassword).WithMessage("Password must contain at least 8 characters, a letter and a special Character")
                .WithName("Password");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords did not match");

            RuleFor(x => x.Telephone)
                .Matches(@"^\d+$")
                .When(x => !string.IsNullOrEmpty(x.Telephone))
                .WithMessage("Phone number must be 10 characters")
                .WithName("Telephone");

            RuleFor(x => x.Login)
                .NotEmpty().NotNull().WithMessage("This Field is Required")
                .MaximumLength(50).WithMessage("Username can have maximum 50 characters")
                .MinimumLength(5).WithMessage("Username must have at least 5 characters")
                .WithName("Login");

            RuleFor(x => x.UserTypeId)
                .NotEmpty().NotNull().WithMessage("This Field is Required")
                .WithName("UserType");
        }

        public bool BeValidName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return Regex.IsMatch(name, @"^[a-zA-Z '.-]*[A-Za-z][^-]$");
        }


        public bool BeValidPassword(string password)
        {
            if(string.IsNullOrEmpty(password)) return false;

            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&/])[A-Za-z\d@$!%*#?&/]{8,}$");
        }


    }
}
