using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Contracts.Entities.Result
{
    public class BaseOperationResult
    {
        public bool Success { get; set; }

        public Error Error { get; set; }

        public BaseOperationResult() { }

        public BaseOperationResult(string errorMessage, string errorContext = null)
        {
            Error = new Error(errorMessage, errorContext);
        }

        public BaseOperationResult(Error error)
        {
            Error = error;
        }

        public BaseOperationResult(Exception ex)
        {
            Error = new Error(ex.Message, ex.GetType().Name);
        }

        public static BaseOperationResult SuccessfulOperation
            => new() { Success = true };
    }
}
