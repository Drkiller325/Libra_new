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
                .MaximumLength(20).WithMessage("Name can't exceed 20 characters");

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

            RuleFor(x => x.Modeel)
                .NotEmpty().NotNull().WithMessage("This field is Required")
                .MinimumLength(5).WithMessage("Model must be at least 5 charachters")
                .MaximumLength(20).WithMessage("Model can't exceed 20 characters");

            RuleFor(x => x.Brand)
                .NotEmpty().NotNull().WithMessage("This field is Required")
                .MinimumLength(5).WithMessage("Brand must be at least 5 charachters")
                .MaximumLength(20).WithMessage("Brand can't exceed 20 characters");

            RuleFor(x => x.MorningOpening)
                .NotNull().WithMessage("This field is Required");
            
            RuleFor(x => x.MorningClosing)
                .NotNull().WithMessage("This field is Required");
            
            RuleFor(x => x.AfternoonOpening)
                .NotNull().WithMessage("This field is Required");
            
            RuleFor(x => x.AfternoonClosing)
                .NotNull().WithMessage("This field is Required");

            RuleFor(x => x.CityId)
                .NotEmpty().NotNull().WithMessage("This field is Required");

            RuleFor(x => x.ConnectionTypeId)
                .NotEmpty().NotNull().WithMessage("This field is Required");

            RuleFor(x => x.ClosingDays)
                .NotEmpty().NotNull().WithMessage("This field is Required");
        }

    }
}
