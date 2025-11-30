using ErrorOr;

namespace HRM.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Common
        {
            public static Error DataNotFound => Error.NotFound(
                code: "NotFound",
                description: "Data Not Found"
                );

            public static Error CommonError(string errorMessge) => Error.Failure(
                code: "Unknown error",
                description: $"Some unknown error occured, message : {errorMessge}"
                );

            public static Error FileNotFound => Error.NotFound(
                code: "NotFound",
                description: "File Not Found"
                );

            public static Error DataNotFoundCustomMessage(string customMessage) => Error.NotFound(
                code: "NotFound",
                description: "Data not found " + customMessage
                );

            public static Error Exist => Error.Conflict(
                code: "Exist",
                description: "Data Already Exist"
                );
  
            public static Error EmailExist => Error.Conflict(
                code: "Exist",
                description: "Email Already Exist !"
                );
            public static Error DataSaveFailed => Error.Failure(
                code: "Save: SaveFailed",
                description: "Failed saving Data"
                );

            public static Error IsUsed(string customName) => Error.Failure(
                code: "IsUsed",
                description: customName + " is in use, cannot delete."
                );
        }
    }
}
