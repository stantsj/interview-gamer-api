using System.Net;

namespace GamerAPI.Models
{
    public class ServiceResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T ReturnObject { get; set; }
    }
}
