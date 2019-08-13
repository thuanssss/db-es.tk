using DatabaseEnsoulSharp.Services.Interface;

namespace DatabaseEnsoulSharp.Services
{
    public class ResponseServerService : IResponseServerService
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }

        public ResponseServerService ResponseError(string message = null)
        {
            return new ResponseServerService()
            {
                Status = false,
                Message = message ?? "An error occurred!"
            };
        }

        public ResponseServerService ResponseSuccess(string message = null, dynamic data = null)
        {
            return new ResponseServerService()
            {
                Status = true,
                Message = message,
                Data = null
            };
        }
    }
}