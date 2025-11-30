using FluentValidation;

namespace HRM.Applicatin
{
    public class SignInQueryValidator : AbstractValidator<SignInQuery>
    {
        public SignInQueryValidator()
        {
            RuleFor(x => x.email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.password).NotEmpty().WithMessage("Password is required");
        }
    }
}
