using HRM.Domain;
using MediatR;
namespace HRM.Applicatin
{
    public class HolidaysGetDataQueryHandler : IRequestHandler<HolidaysGetDataQuery, Holidays>
    {
        public readonly IHolidaysRepository _holidaysRepository;

        public HolidaysGetDataQueryHandler(IHolidaysRepository holidaysRepository)
        {
            _holidaysRepository = holidaysRepository;
        }
        public async Task<Holidays> Handle(HolidaysGetDataQuery query, CancellationToken cancellationToken)
        {
            return await _holidaysRepository.HolidaysGetDataAsync(query.id);
        }
    }

    public class HolidaysGetAllDataQueryHandler : IRequestHandler<HolidaysGetAllDataQuery, IEnumerable<Holidays>>
    {
        public readonly IHolidaysRepository _holidaysRepository;

        public HolidaysGetAllDataQueryHandler(IHolidaysRepository holidaysRepository)
        {
            _holidaysRepository = holidaysRepository;
        }

        public async Task<IEnumerable<Holidays>> Handle(HolidaysGetAllDataQuery query, CancellationToken cancellationToken)
        {
            return await _holidaysRepository.HolidaysGetAllDataAsync();
        }
    }
}

