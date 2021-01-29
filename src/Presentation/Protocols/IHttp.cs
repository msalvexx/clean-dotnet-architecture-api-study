namespace Presentation.Protocols
{
    public interface IHttpRequest<TRequest> where TRequest : class
    {
        public TRequest Body { get; set; }
    }

    public interface IHttpResponse
    {
        public int Status { get; set; }
        public object Body { get; set; }
    }

    public class HttpRequest<TRequest> : IHttpRequest<TRequest> where TRequest : class
    {
        public TRequest Body { get; set; }
    }

    public class HttpResponse : IHttpResponse
    {
        public int Status { get; set; }
        public object Body { get; set; }
    }
}
