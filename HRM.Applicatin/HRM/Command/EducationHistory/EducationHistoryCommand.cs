using HRM.Domain;
using MediatR;

namespace HRM.Applicatin
{
    public record AddEducationHistoryCommand(EducationHistory educationHistory) : IRequest<EducationHistory>;
    public record UpdateEducationHistoryCommand(int educationHistoryId, EducationHistory educationHistory) : IRequest<EducationHistory>;
    public record DeleteEducationHistoryCommand(int educationHistoryId) : IRequest<bool>;
}
