namespace GamerAPI.Models
{
    public class ServiceResult<T>
    {
        public ServiceStatusCode StatusCode { get; set; }
        public T ReturnObject { get; set; }
    }
}
