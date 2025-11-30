using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Applicatin
{
    public class GetHolidaysByIdQueryValidator : AbstractValidator<HolidaysGetDataQuery>
    {
        public GetHolidaysByIdQueryValidator()
        {
            RuleFor(item => item.id).Must(value => (value is int intValue && intValue > 0))
                    .WithMessage("ID must be greater then zero")
                    .NotEmpty().WithMessage("ID cannot be empty.");
        }
    }
}
