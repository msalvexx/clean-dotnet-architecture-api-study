namespace Presentation.Protocols
{
    public interface IHttpRequest
    {
        public object Body { get; set; }
    }

    public interface IHttpResponse
    {
        public int Status { get; set; }
        public object Body { get; set; }
    }

    public class HttpRequest : IHttpRequest
    {
        public object Body { get; set; }
    }

    public class HttpResponse : IHttpResponse
    {
        public int Status { get; set; }
        public object Body { get; set; }
    }
}
