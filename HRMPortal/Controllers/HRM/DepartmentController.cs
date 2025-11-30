using ErrorOr;
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
    public class DepartmentController : ControllerBase
    {
        private readonly ISender _sender;

        public DepartmentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] Department department)
        {
            var command  = new AddDepartmentCommand(department);

            ErrorOr<Department> response = await _sender.Send(command);

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

        [HttpPut("UpdateDepartment/{departmentId}")]
        public async Task<IActionResult> UpdateDepartmentAsync([FromRoute] int departmentId, [FromBody] Department department)
        {

            var command = new UpdateDepartmentCommand(departmentId, department);

            ErrorOr<Department> response  = await _sender.Send(command);
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

        [HttpDelete("DeleteDepartmentById/{departmentId}")]
        public async Task<IActionResult> DeleteDepartmentByIdAsync([FromRoute] int departmentId)
        {
            var response = await _sender.Send(new DeleteDepartmentCommand(departmentId));
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

        [HttpGet("GetAllDepartment")]
        public async Task<IActionResult> GetAllEmployeeAsync()
        {
            var response= await _sender.Send(new DepartmentGetAllDataQuery());
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

        [HttpGet("GetDataByCompanyId/{companyId}")]
        public async Task<IActionResult> GetDepartmentByCompanyIdAsync([FromRoute] int companyId)
        {
            var query = new DepartmentGetDataByCompanyIdQuery(companyId);
            ErrorOr<IEnumerable<Department>> response = await _sender.Send(query);
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

        [HttpGet("GetDataById/{departmentId}")]
        public async Task<IActionResult> GetDepartmentByIdAsync([FromRoute] int departmentId)
        {
            var query = new DepartmentGetDataQuery(departmentId);
            ErrorOr<Department> response = await _sender.Send(query);

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
