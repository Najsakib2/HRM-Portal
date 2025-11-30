using HRM.Applicatin;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeAttendanceController : Controller
    {
        private readonly ISender _sender;

        public EmployeeAttendanceController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddEmployeeAttendance")]
        public async Task<IActionResult> AddEmployeeAttendanceAsync([FromBody] EmployeeAttendance employeeAttendance)
        {
            AddEmployeeAttendanceCommand command = new AddEmployeeAttendanceCommand(employeeAttendance);

            var result = await _sender.Send(new AddEmployeeAttendanceCommand(employeeAttendance));

            return Ok(result);
        }

        [HttpPut("UpdateEmployeeAttendance/{employeeAttendanceId}")]
        public async Task<IActionResult> UpdateEmployeeAttendanceAsync([FromRoute] int employeeAttendanceId, [FromBody] EmployeeAttendance employeeAttendance)
        {
            var result = await _sender.Send(new UpdateEmployeeAttendanceCommand(employeeAttendanceId, employeeAttendance));
            return Ok(result);
        }

        [HttpDelete("DeleteEmployeeAttendanceById/{employeeAttendanceId}")]
        public async Task<IActionResult> DeleteEmployeeAttendanceByIdAsync([FromRoute] int employeeAttendanceId)
        {
            var result = await _sender.Send(new DeleteEmployeeAttendanceCommand(employeeAttendanceId));
            return Ok(result);
        }

        [HttpGet("GetAllEmployeeAttendance")]
        public async Task<IActionResult> GetAllEmployeeAttendanceAsync()
        {
            var result = await _sender.Send(new EmployeeAttendanceGetAllDataQuery());
            return Ok(result);
        }

        [HttpGet("GetEmployeeAttendanceById/{employeeAttendanceId}")]
        public async Task<IActionResult> GetEmployeeAttendanceByIdAsync([FromRoute] int employeeAttendanceId)
        {
            var result = await _sender.Send(new EmployeeAttendanceGetDataQuery(employeeAttendanceId));
            return Ok(result);
        }
    }
}
