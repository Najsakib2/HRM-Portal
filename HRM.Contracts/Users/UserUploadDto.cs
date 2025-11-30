using Microsoft.AspNetCore.Http;

namespace HRM.Contracts.Users
{
    public class UserAddEditDto
    {
        public IFormFile? Image { get; set; }
        public IFormFile? Signature { get; set; }
        public string JsonData { get; set; } = string.Empty;
    }

}
