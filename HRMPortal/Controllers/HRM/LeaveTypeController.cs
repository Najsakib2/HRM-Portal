using MediatR;
using HRM.Domain;
using Microsoft.AspNetCore.Mvc;
using HRM.Applicatin;
using Microsoft.AspNetCore.Authorization;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveTypeController : Controller
    {
        private readonly ISender _sender;

        public LeaveTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddLeaveType")]
        public async Task<IActionResult> AddLeaveTypeAsync([FromBody] LeaveType leaveType)
        {
            AddLeaveTypeCommand command = new AddLeaveTypeCommand(leaveType);

            var result = await _sender.Send(new AddLeaveTypeCommand(leaveType));

            return Ok(result);
        }

        [HttpPut("UpdateLeaveType/{leaveTypeId}")]
        public async Task<IActionResult> UpdateLeaveTypeAsync([FromRoute] int leaveTypeId, [FromBody] LeaveType leaveType)
        {
            var result = await _sender.Send(new UpdateLeaveTypeCommand(leaveTypeId, leaveType));
            return Ok(result);
        }

        [HttpDelete("DeleteLeaveTypeById/{leaveTypeId}")]
        public async Task<IActionResult> DeleteLeaveTypeByIdAsync([FromRoute] int leaveTypeId)
        {
            var result = await _sender.Send(new DeleteLeaveTypeCommand(leaveTypeId));
            return Ok(result);
        }

        [HttpGet("GetAllLeaveType")]
        public async Task<IActionResult> GetAllLeaveTypeAsync()
        {
            var result = await _sender.Send(new LeaveTypeGetAllDataQuery());
            return Ok(result);
        }

        [HttpGet("GetLeaveTypeById/{leaveTypeId}")]
        public async Task<IActionResult> GetLeaveTypeByIdAsync([FromRoute] int leaveTypeId)
        {
            var result = await _sender.Send(new LeaveTypeGetDataQuery(leaveTypeId));
            return Ok(result);
        }
    }
}
