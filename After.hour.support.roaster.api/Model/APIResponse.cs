using System.Net;

namespace After.hour.support.roaster.api.Model
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}
