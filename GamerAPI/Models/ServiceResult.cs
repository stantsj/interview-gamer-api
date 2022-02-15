using System.Net;

namespace GamerAPI.Models
{
    public class ServiceResult<T>
    {
        // TODO: service status codes
        public ServiceStatusCode StatusCode { get; set; }
        public T ReturnObject { get; set; }
    }
}
