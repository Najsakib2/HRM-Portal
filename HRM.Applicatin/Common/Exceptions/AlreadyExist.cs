
using HRM.Applicatin.Common.Exceptions;

namespace HRM.Application.Common.Exceptions
{
    public class AlreadyExist : AppException
    {
        public AlreadyExist(string message) : base(message) { }
    }
}
