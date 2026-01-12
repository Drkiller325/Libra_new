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
    public class AddUserCommandValidator : AbstractValidator<AddUserViewModel>
    {
        private readonly IAppDbContext _context;

        public AddUserCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("This Field is Required")
                .NotNull().WithMessage("This Field is Required")
                .MinimumLength(5).WithMessage("Name Should at least be 5 characters Long")
                .MaximumLength(50).WithMessage("Name is too Long")
                .Must(BeValidName).WithMessage("Name can Only contain letters")
                .WithName("Name");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("This Field is Required")
                .NotNull().WithMessage("This Field is Required")
                .MaximumLength(50).WithMessage("email can have maximum 50 characters")
                .Must(BeUniqueEmail).WithMessage("Email already exists")
                .Must(BeValidEmail).WithMessage("Invalid Email")
                .WithName("Email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("This Field is Required")
                .NotNull().WithMessage("This Field is Required")
                .MaximumLength(50).WithMessage("Password can have maximum 50 characters")
                .Must(BeValidPassword).WithMessage("Password must contain at least 8 characters, a letter and a special Character")
                .WithName("Password");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords did not match");

            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("This Field is Required")
                .NotNull().WithMessage("This Field is Required")
                .MaximumLength(10).WithMessage("Phone number must be 8 characters")
                .Length(10)
                .Must(BeDigit)
                .WithName("Telephone");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("This Field is Required")
                .NotNull().WithMessage("This Field is Required")
                .MaximumLength(50).WithMessage("Username can have maximum 50 characters")
                .MinimumLength(5).WithMessage("Username must have at least 5 characters")
                .Must(BeUniqueLogin).WithMessage("Username Already exists")
                .WithName("Login");

            RuleFor(x => x.UserTypeId)
                .NotEmpty().WithMessage("This Field is Required")
                .NotNull().WithMessage("This Field is Required")
                .InclusiveBetween(1, 3).WithMessage("Invalid UserType")
                .WithName("UserType");
        }

        public bool BeValidName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return Regex.IsMatch(name, @"^[a-zA-Z '.-]*[A-Za-z][^-]$");
        }

        public bool BeValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            return Regex.IsMatch(email, @"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$");
        }

        public bool BeValidPassword(string password)
        {
            if(string.IsNullOrEmpty(password)) return false;

            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&/])[A-Za-z\d@$!%*#?&/]{8,}$");
        }

        public bool BeUniqueEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            return !_context.Users.Any(x => x.Email == email);
        }

        public bool BeUniqueLogin(string login)
        {
            if (string.IsNullOrEmpty(login)) return false;

            return !_context.Users.Any(x => x.Login == login);
        }

        public bool BeDigit(string number)
        {
            if (string.IsNullOrEmpty(number)) return false;

            return Regex.IsMatch(number, @"^\d+$");
        }
    }
}
