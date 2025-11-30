using ErrorOr;
using HRM.Applicatin;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers.HRM
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserDetailsController : Controller
    {
        private readonly ISender _sender;

        public UserDetailsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddEmployeeDetails")]
        public async Task<IActionResult> AddUserDetailsAsync([FromBody] UserDetails userDetails)
        {
            AddUserDetailsCommand command = new AddUserDetailsCommand(userDetails);

            ErrorOr<UserDetails> response = await _sender.Send(new AddUserDetailsCommand(userDetails));

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

        [HttpPut("UpdateEmployeeDetails/{employeeDetailsId}")]
        public async Task<IActionResult> UpdateUserDetailsAsync([FromRoute] int userDetailsId, [FromBody] UserDetails userDetails)
        {
            ErrorOr<UserDetails> response = await _sender.Send(new UpdateUserDetailsCommand(userDetailsId, userDetails));

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

        [HttpDelete("DeleteEmployeeDetailsById/{employeeDetailsId}")]
        public async Task<IActionResult> DeleteEmployeeDetailsByIdAsync([FromRoute] int userDetailsId)
        {
            ErrorOr<bool> response = await _sender.Send(new DeleteUserDetailsCommand(userDetailsId));

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

        [HttpGet("GetAllEmployeeDetails")]
        public async Task<IActionResult> GetAllUserDetailsAsync()
        {
            ErrorOr<IEnumerable<UserDetails>> response = await _sender.Send(new UserDetailsGetAllDataQuery());

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

        [HttpGet("GetEmployeeDetailsById/{employeeDetailsId}")]
        public async Task<IActionResult> GetUserDetailsByIdAsync([FromRoute] int userDetailsId)
        {
            ErrorOr<UserDetails> response = await _sender.Send(new UserDetailsGetDataQuery(userDetailsId));

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

