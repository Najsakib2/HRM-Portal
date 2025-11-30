using HRM.Applicatin;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers.HRM
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HolidaysController : Controller
    {
        private readonly ISender _sender;

        public HolidaysController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddHolidays")]
        public async Task<IActionResult> AddHolidaysAsync([FromBody] Holidays holidays)
        {
            AddHolidaysCommand command = new AddHolidaysCommand(holidays);

            var result = await _sender.Send(new AddHolidaysCommand(holidays));

            return Ok(result);
        }

        [HttpPut("UpdateHolidays/{holidaysId}")]
        public async Task<IActionResult> UpdateHolidaysAsync([FromRoute] int holidaysId, [FromBody] Holidays holidays)
        {
            var result = await _sender.Send(new UpdateHolidaysCommand(holidaysId, holidays));
            return Ok(result);
        }

        [HttpDelete("DeleteHolidaysById/{holidaysId}")]
        public async Task<IActionResult> DeleteHolidaysByIdAsync([FromRoute] int holidaysId)
        {
            var result = await _sender.Send(new DeleteHolidaysCommand(holidaysId));
            return Ok(result);
        }

        [HttpGet("GetAllHolidays")]
        public async Task<IActionResult> GetAllHolidaysAsync()
        {
            var result = await _sender.Send(new HolidaysGetAllDataQuery());
            return Ok(result);
        }

        [HttpGet("GetHolidaysById/{holidaysId}")]
        public async Task<IActionResult> GetHolidaysByIdAsync([FromRoute] int holidaysId)
        {
            var result = await _sender.Send(new HolidaysGetDataQuery(holidaysId));
            return Ok(result);
        }
    }
}
