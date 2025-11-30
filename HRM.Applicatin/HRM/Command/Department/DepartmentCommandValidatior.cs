using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Applicatin
{
    public class AddDepartmentCommandValidator : AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentCommandValidator()
        {
            RuleFor(x => x.department.DepartmentCode).NotEmpty().WithMessage("DepartmentCode is required");
            RuleFor(x => x.department.DepartmentName).NotEmpty().WithMessage("DepartmentName is required");
        }
    }
}
