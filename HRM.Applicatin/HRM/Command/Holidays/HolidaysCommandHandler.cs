using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public class AddHolidaysCommandHandler : IRequestHandler<AddHolidaysCommand, Holidays>
    {
        private readonly IHolidaysRepository _holidaysRepository;

        public AddHolidaysCommandHandler(IHolidaysRepository holidaysRepository)
        {
            _holidaysRepository = holidaysRepository;
        }

        public async Task<Holidays> Handle(AddHolidaysCommand command, CancellationToken cancellationToken)
        {

            var addedHolidays = await _holidaysRepository.AddHolidaysAsync(command.holidays);
            return addedHolidays;
        }
    }

    public class UpdateHolidaysCommandHandler : IRequestHandler<UpdateHolidaysCommand, Holidays>
    {
        private readonly IHolidaysRepository _holidaysRepository;

        public UpdateHolidaysCommandHandler(IHolidaysRepository holidaysRepository)
        {
            _holidaysRepository = holidaysRepository;
        }

        public async Task<Holidays> Handle(UpdateHolidaysCommand command, CancellationToken cancellationToken)
        {
            return await _holidaysRepository.UpdateHolidaysAsync(command.holidayId, command.holidays);
        }
    }

    public class DeleteHolidaysCommandHandler : IRequestHandler<DeleteHolidaysCommand, bool>
    {
        private readonly IHolidaysRepository _holidaysRepository;

        public DeleteHolidaysCommandHandler(IHolidaysRepository holidaysRepository)
        {
            _holidaysRepository = holidaysRepository;
        }
        public async Task<bool> Handle(DeleteHolidaysCommand command, CancellationToken cancellationToken)
        {
            return await _holidaysRepository.DeleteHolidaysAsync(command.holidayId);
        }
    }
}
