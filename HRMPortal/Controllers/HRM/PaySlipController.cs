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
    public class PaySlipController : Controller
    {
        private readonly ISender _sender;

        public PaySlipController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddPaySlip")]
        public async Task<IActionResult> AddPaySlipAsync([FromBody] PaySlip paySlip)
        {
            AddPaySlipCommand command = new AddPaySlipCommand(paySlip);

            var result = await _sender.Send(new AddPaySlipCommand(paySlip));

            return Ok(result);
        }

        [HttpPut("UpdatePaySlip/{paySlipId}")]
        public async Task<IActionResult> UpdatePaySlipAsync([FromRoute] int paySlipId, [FromBody] PaySlip paySlip)
        {
            var result = await _sender.Send(new UpdatePaySlipCommand(paySlipId, paySlip));
            return Ok(result);
        }

        [HttpDelete("DeletePaySlipById/{paySlipId}")]
        public async Task<IActionResult> DeletePaySlipByIdAsync([FromRoute] int paySlipId)
        {
            var result = await _sender.Send(new DeletePaySlipCommand(paySlipId));
            return Ok(result);
        }

        [HttpGet("GetAllPaySlip")]
        public async Task<IActionResult> GetAllPaySlipAsync()
        {
            var result = await _sender.Send(new PaySlipGetAllDataQuery());
            return Ok(result);
        }

        [HttpGet("GetPaySlipById/{paySlipId}")]
        public async Task<IActionResult> GetPaySlipByIdAsync([FromRoute] int paySlipId)
        {
            var result = await _sender.Send(new PaySlipGetDataQuery(paySlipId));
            return Ok(result);
        }
    }
}
