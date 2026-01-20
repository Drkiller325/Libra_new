using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Poses.ViewModels
{
    public class AddPosValidator : AbstractValidator<AddPosViewModel>
    {
        public AddPosValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull().WithMessage("This field is Required")
                .MinimumLength(5).WithMessage("Name must be at least 5 charachters")
                .MaximumLength(20).WithMessage("Name can't exceed 20 characters")
                .Must(BeValidName).WithMessage("Name can only contain letters");

            RuleFor(x => x.Telephone)
                .NotNull().NotEmpty().WithMessage("This field is Required")
                .Matches(@"^\d+$").WithMessage("Telephone must contain only Numbers")
                .MaximumLength(10).WithMessage("Telephone number can't exceed 10 numbers");
            
            RuleFor(x => x.Cellphone)
                .NotNull().NotEmpty().WithMessage("This field is Required")
                .Matches(@"^\d+$").WithMessage("Telephone must contain only Numbers")
                .MaximumLength(10).WithMessage("Telephone number can't exceed 10 numbers");

            RuleFor(x => x.Address)
                .NotNull().NotEmpty().WithMessage("This field is Required")
                .MinimumLength(5).WithMessage("Address must have at least 5 characters")
                .MaximumLength(50).WithMessage("Address cant exceed 20 characters");

            RuleFor(x => x.Model)
                .NotEmpty().NotNull().WithMessage("This field is Required")
                .MinimumLength(5).WithMessage("Model must be at least 5 charachters")
                .MaximumLength(20).WithMessage("Model can't exceed 20 characters")
                .Must(BeValidName).WithMessage("Model can only contain letters");

            RuleFor(x => x.Brand)
                .NotEmpty().NotNull().WithMessage("This field is Required")
                .MinimumLength(5).WithMessage("Brand must be at least 5 charachters")
                .MaximumLength(20).WithMessage("Brand can't exceed 20 characters")
                .Must(BeValidName).WithMessage("Brand can only contain letters");

            RuleFor(x => x.MorningOpening)
                .NotEmpty().NotNull().WithMessage("This field is Required");
            
            RuleFor(x => x.MorningClosing)
                .NotEmpty().NotNull().WithMessage("This field is Required");
            
            RuleFor(x => x.AfternoonOpening)
                .NotEmpty().NotNull().WithMessage("This field is Required");
            
            RuleFor(x => x.AfternoonClosing)
                .NotEmpty().NotNull().WithMessage("This field is Required");

            RuleFor(x => x.CityId)
                .NotEmpty().NotNull().WithMessage("This field is Required");

            RuleFor(x => x.ConnectionTypeId)
                .NotEmpty().NotNull().WithMessage("This field is Required");

            RuleFor(x => x.ClosingDays)
                .NotEmpty().NotNull().WithMessage("This field is Required");
        }

        public bool BeValidName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            return Regex.IsMatch(name, @"^[a-zA-Z '.-]*[A-Za-z][^-]$");
        }
    }
}
