using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReefStories.Shared.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "Not Authorized",
                404 => "Resourse Not Found",
                500 => "Internal Server Error",
                _ => null

            };
        }
    }
}
