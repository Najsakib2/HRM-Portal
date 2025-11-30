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

    public class EducationHistoryController : ControllerBase
    {
        private readonly ISender _sender;
        public EducationHistoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddEducationHistory")]
        public async Task<IActionResult> AddEmployeeAsync([FromBody] EducationHistory educationHistory)
        {

            AddEducationHistoryCommand command = new AddEducationHistoryCommand(educationHistory);  

            var result = await _sender.Send(new AddEducationHistoryCommand(educationHistory));

            return Ok(result);
        }

        [HttpPut("UpdateEducationHistory/{educationHistoryId}")]
        public async Task<IActionResult> UpdateEducationHistoryAsync([FromRoute] int educationHistoryId, [FromBody] EducationHistory educationHistory)
        {
            var result = await _sender.Send(new UpdateEducationHistoryCommand(educationHistoryId, educationHistory));
            return Ok(result);
        }

        [HttpDelete("DeleteEducationHistoryById/{educationHistoryId}")]
        public async Task<IActionResult> DeleteEducationHistoryByIdAsync([FromRoute] int educationHistoryId)
        {
            var result = await _sender.Send(new DeleteEducationHistoryCommand(educationHistoryId));
            return Ok(result);
        }

        [HttpGet("GetAllEducationHistory")]
        public async Task<IActionResult> GetAllEducationHistoryAsync()
        {
            var result = await _sender.Send(new EducationHistoryGetAllDataQuery());
            return Ok(result);
        }

        [HttpGet("GetEducationHistoryById/{educationHistoryId}")]
        public async Task<IActionResult> GetEducationHistoryByIdAsync([FromRoute] int educationHistoryId)
        {
            var result = await _sender.Send(new EducationHistoryGetDataQuery(educationHistoryId));
            return Ok(result);
        }
    }
}
