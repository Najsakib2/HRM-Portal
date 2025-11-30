using MediatR;

namespace HRM.Applicatin
{
    public record SignUpCommand(CommandCompanyDto signUpDto) : IRequest<CommandCompanyDto>;
}
