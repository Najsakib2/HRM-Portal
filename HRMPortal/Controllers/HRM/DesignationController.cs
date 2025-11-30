using Microsoft.AspNetCore.Mvc;
using HRM.Applicatin;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using ErrorOr;
using Azure;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly ISender _sender;
        public DesignationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddDesignation")]
        public async Task<IActionResult> AddDesignationAsync([FromBody] Designation designation)
        {
            AddDesignationCommand command = new AddDesignationCommand(designation);

            ErrorOr<Designation> response = await _sender.Send(new AddDesignationCommand(designation));

            return response.Match(
               success => Ok(success),
               error =>
               {
                   var firstError = error.First();
                   return Problem(
                       detail: firstError.Description,
                       title: firstError.Code
                   );
               }
          );
        }

        [HttpPut("UpdateDesignation/{designationId}")]
        public async Task<IActionResult> UpdateDesignationAsync([FromRoute] int designationId, [FromBody] Designation designation)
        {
            ErrorOr<Designation> response = await _sender.Send(new UpdateDesignationCommand(designationId, designation));
            return response.Match(
               success => Ok(success),
               error =>
               {
                   var firstError = error.First();
                   return Problem(
                       detail: firstError.Description,
                       title: firstError.Code
                   );
               }
            );
        }

        [HttpDelete("DeleteDesignationById/{designationId}")]
        public async Task<IActionResult> DeleteDesignationByIdAsync([FromRoute] int designationId)
        {
            ErrorOr<bool> response = await _sender.Send(new DeleteDesignationCommand(designationId));
            return response.Match(
               success => Ok(success),
               error =>
               {
                   var firstError = error.First();
                   return Problem(
                       detail: firstError.Description,
                       title: firstError.Code
                   );
               }
            );
        }

        [HttpGet("GetAllDesignation")]
        public async Task<IActionResult> GetAllDesignationAsync()
        {
            ErrorOr<IEnumerable<Designation>> response = await _sender.Send(new DesignationGetAllDataQuery());

            return response.Match(
                 success => Ok(success),
                 error =>
                 {
                     var firstError = error.First();
                     return Problem(
                         detail: firstError.Description,
                         title: firstError.Code
                     );
                 }
            );
        }

        [HttpGet("GetDesignationById/{designationId}")]
        public async Task<IActionResult> GetDesignationByIdAsync([FromRoute] int designationId)
        {
            var query = new DesignationGetDataQuery(designationId);

            ErrorOr<Designation> respone = await _sender.Send(query);

            return respone.Match(
                success => Ok(success),
                error =>
                {
                    var firstError = error.First();
                    return Problem(
                        detail: firstError.Description,
                        title: firstError.Code
                    );
                }
             );
        }

        [HttpGet("GetDataByCompanyId/{companyId}")]
        public async Task<IActionResult> GetDesignationByCompanyIdAsync([FromRoute] int companyId)
        {
            var query = new DesignationGetDataByCompanyIdQuery(companyId);
            ErrorOr<IEnumerable<Designation>> response = await _sender.Send(query);
            return response.Match(
               success => Ok(success),
               error =>
               {
                   var firstError = error.First();
                   return Problem(
                       detail: firstError.Description,
                       title: firstError.Code
                   );
               }
            );
        }
    }
}
