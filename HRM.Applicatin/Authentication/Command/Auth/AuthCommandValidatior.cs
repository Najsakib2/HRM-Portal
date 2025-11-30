using FluentValidation;

namespace HRM.Applicatin
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.signUpDto.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.signUpDto.UserDto.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
