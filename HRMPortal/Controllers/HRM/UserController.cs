using Azure;
using ErrorOr;
using HRM.Applicatin;
using HRM.Application;
using HRM.Contracts.Users;
using HRM.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAsync([FromForm] UserAddEditDto request)
        {
            var userDeserial = JsonConvert.DeserializeObject<Users>(request.JsonData);

            var user = new AddUserCommand(userDeserial, request.Image, request.Signature);

            ErrorOr<Users> response = await _sender.Send(user);
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

        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromForm] UserAddEditDto request)
        {
            var user = JsonConvert.DeserializeObject<Users>(request.JsonData);

            ErrorOr<Users> response = await _sender.Send(new UpdateUserCommand(user, request.Image, request.Signature));
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

        [Authorize]
        [HttpDelete("DeleteUserById/{userId}")]
        public async Task<IActionResult> DeleteDoctorByIdAsync([FromRoute] int userId)
        {
            ErrorOr<bool> response = await _sender.Send(new DeleteUserCommand(userId));
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

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUserAsync()
        {
            ErrorOr<IEnumerable<QueryUserDto>> response = await _sender.Send(new UserGetAllDataQuery());
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

        [Authorize]
        [HttpGet("GetUserId/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int userId)
        {
            ErrorOr<QueryUserDto> response = await _sender.Send(new UserGetDataQuery(userId));
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

        [Authorize]
        [HttpGet("GetDataByCompanyId/{companyId}")]
        public async Task<IActionResult> GetUserDataByCompanyID([FromRoute] int companyId)
        {
            ErrorOr<IEnumerable<QueryUserDto>> response = await _sender.Send(new UserGetDataByCompanyIdQuery(companyId));
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
