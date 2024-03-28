using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.ApplicationConstants
{
    public class ApplicationConstant
    {

    }
    public class CommonMessage
    {
        public const string admin = "ADMIN";
        public const string customer = "CUSTOMER";

        public const string RegisterOperationSuccess = "Registration done Successfully";
        public const string RegisterOperationFailed = "Registration Failed";

        public const string LoginOperationSuccess = "Successfully logged in";
        public const string LoginOperationFailed = "Login Failed";

        public const string CreateOperationSuccess = "Record Created Successfully";
        public const string UpdateOperationSuccess = "Record Updated Successfully";
        public const string DeleteOperationSuccess = "Record Deleted Successfully";

        public const string CreateOperationFailed = " Created Operation Failed";
        public const string UpdateOperationFailed = " Updated Operation Failed";
        public const string DeleteOperationFailed = " Deleted Operation Failed";

        public const string RecordNotFound = "Record Not Found";
        public const string SystemError = "Something went wrong";
         
    }
}
