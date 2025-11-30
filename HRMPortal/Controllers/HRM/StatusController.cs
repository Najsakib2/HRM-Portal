using HRM.Applicatin;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers.HRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly ISender _sender;

        public StatusController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddStatus")]
        public async Task<IActionResult> AddStatusAsync([FromBody] Status status)
        {
            AddStatusCommand command = new AddStatusCommand(status);
            
            var result = await _sender.Send(new AddStatusCommand(status));

            return Ok(result);
        }

        [HttpPut("UpdateStatus/{statusId}")]
        public async Task<IActionResult> UpdateStatusAsync([FromRoute] int statusId, [FromBody] Status status)
        {
            var result = await _sender.Send(new UpdateStatusCommand(statusId, status));
            return Ok(result);
        }

        [HttpDelete("DeleteStatusById/{statusId}")]
        public async Task<IActionResult> DeleteStatusByIdAsync([FromRoute] int statusId)
        {
            var result = await _sender.Send(new DeleteStatusCommand(statusId));
            return Ok(result);
        }

        [HttpGet("GetAllStatus")]
        public async Task<IActionResult> GetAllStatusAsync()
        {
            var result = await _sender.Send(new StatusGetAllDataQuery());
            return Ok(result);
        }

        [HttpGet("GetStatusById/{statusId}")]
        public async Task<IActionResult> GetStatusByIdAsync([FromRoute] int statusId)
        {
            var result = await _sender.Send(new StatusGetDataQuery(statusId));
            return Ok(result);
        }
    }
}
