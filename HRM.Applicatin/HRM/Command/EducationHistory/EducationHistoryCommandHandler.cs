using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public class AddEducationHistoryCommandHandler : IRequestHandler<AddEducationHistoryCommand, EducationHistory>
    {
        private readonly IEducationHistoryRepository _educationHistoryRepository;

        public AddEducationHistoryCommandHandler(IEducationHistoryRepository educationHistoryRepository)
        {
            _educationHistoryRepository = educationHistoryRepository;
        }

        public async Task<EducationHistory> Handle(AddEducationHistoryCommand command, CancellationToken cancellationToken)
        {

            var addedEducationHistory = await _educationHistoryRepository.AddEducationHistoryAsync(command.educationHistory);
            return addedEducationHistory;
        }
    }

    public class UpdateEducationHistoryCommandHandler : IRequestHandler<UpdateEducationHistoryCommand, EducationHistory>
    {
        private readonly IEducationHistoryRepository _educationHistoryRepository;

        public UpdateEducationHistoryCommandHandler(IEducationHistoryRepository educationHistoryRepository)
        {
            _educationHistoryRepository = educationHistoryRepository;
        }

        public async Task<EducationHistory> Handle(UpdateEducationHistoryCommand command, CancellationToken cancellationToken)
        {
            return await _educationHistoryRepository.UpdateEducationHistoryAsync(command.educationHistoryId, command.educationHistory);
        }
    }

    public class DeleteEducationHistoryCommandHandler : IRequestHandler<DeleteEducationHistoryCommand, bool>
    {
        private readonly IEducationHistoryRepository _educationHistoryRepository;

        public DeleteEducationHistoryCommandHandler(IEducationHistoryRepository educationHistoryRepository)
        {
            _educationHistoryRepository = educationHistoryRepository;
        }
        public async Task<bool> Handle(DeleteEducationHistoryCommand command, CancellationToken cancellationToken)
        {
            return await _educationHistoryRepository.DeleteEducationHistoryAsync(command.educationHistoryId);
        }
    }
}
