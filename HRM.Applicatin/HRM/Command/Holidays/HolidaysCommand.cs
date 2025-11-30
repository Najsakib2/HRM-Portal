using HRM.Domain;
using MediatR;


namespace HRM.Applicatin
{
    public record AddHolidaysCommand(Holidays holidays) : IRequest<Holidays>;
    public record UpdateHolidaysCommand(int holidayId, Holidays holidays) : IRequest<Holidays>;
    public record DeleteHolidaysCommand(int holidayId) : IRequest<bool>;
}
