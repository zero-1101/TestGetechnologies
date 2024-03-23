using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGetechnologies.Shared
{
    public class ResponseApi<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public T? Value { get; set; } = default;

        public ResponseApi<T> CreateSuccessResponse(T? value)
        {
            return new ResponseApi<T> { IsSuccess = true, Message = "Ok", Value = value };
        }

        public ResponseApi<T> CreateInternalServerErrorResponse()
        {
            return new ResponseApi<T> { IsSuccess = false, Message = "Internal Server Error", Value = default };
        }

        public ResponseApi<T> CreateNotFoundResponse()
        {
            return new ResponseApi<T> { IsSuccess = false, Message = "Not Found", Value = default };
        }

        public ResponseApi<T> CreateBadRequestResponse(T Errors)
        {
            return new ResponseApi<T> { IsSuccess = false, Message = $"Bad Request", Value = Errors };
        }
    }
}
