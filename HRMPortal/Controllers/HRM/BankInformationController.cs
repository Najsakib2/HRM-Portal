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

    public class BankInformationController : ControllerBase
    {
        private readonly ISender _sender;

        public BankInformationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddBankInformation")]
        public async Task<IActionResult> AddBankInformationAsync([FromBody] BankInformation bankInformation)
        {
            AddBankInformationCommand command = new AddBankInformationCommand(bankInformation);

            var result = await _sender.Send(new AddBankInformationCommand(bankInformation));

            return Ok(result);
        }

        [HttpPut("UpdateBankInformation/{bankInformationId}")]
        public async Task<IActionResult> UpdateBankInformationAsync([FromRoute] int bankInformationId, [FromBody] BankInformation bankInformation)
        {
            var result = await _sender.Send(new UpdateBankInformationCommand(bankInformationId, bankInformation));
            return Ok(result);
        }

        [HttpDelete("DeleteBankInformationById/{bankInformationId}")]
        public async Task<IActionResult> DeleteBankInformationByIdAsync([FromRoute] int bankInformationId)
        {
            var result = await _sender.Send(new DeleteBankInformationCommand(bankInformationId));
            return Ok(result);
        }

        [HttpGet("GetAllBankInformation")]
        public async Task<IActionResult> GetAllBankInformationAsync()
        {
            var result = await _sender.Send(new BankInformationGetAllDataQuery());
            return Ok(result);
        }

        [HttpGet("GetBankInformationById/{bankInformationId}")]
        public async Task<IActionResult> GetBankInformationByIdAsync([FromRoute] int bankInformationId)
        {
            var result = await _sender.Send(new BankInformationGetDataQuery(bankInformationId));
            return Ok(result);
        }
    }
}
