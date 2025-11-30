using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public class AddStatusCommandHandler : IRequestHandler<AddStatusCommand, Status>
    {
        private readonly IStatusRepository _statusRepository;

        public AddStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<Status> Handle(AddStatusCommand command, CancellationToken cancellationToken)
        {

            var addedStatus = await _statusRepository.AddStatusAsync(command.status);
            return addedStatus;
        }
    }

    public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, Status>
    {
        private readonly IStatusRepository _statusRepository;

        public UpdateStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<Status> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            return await _statusRepository.UpdateStatusAsync(command.statusId, command.status);
        }
    }

    public class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand, bool>
    {
        private readonly IStatusRepository _statusRepository;

        public DeleteStatusCommandHandler(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        public async Task<bool> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
        {
            return await _statusRepository.DeleteStatusAsync(command.statusId);
        }
    }
}
