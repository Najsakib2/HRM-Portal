using FluentValidation;

namespace HRM.Applicatin
{
    public class AddUSerCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUSerCommandValidator()
        {
            RuleFor(x => x.user.ID).NotEmpty().WithMessage("DocumentID is required");
            RuleFor(x => x.user.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.user.FullName).NotEmpty().WithMessage("FullName is required");
        }
    }
}
