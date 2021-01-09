namespace Presentation.Protocols
{
    public interface IHttpResponse<T>
    {
        public int Status { get; set; }
        public T Body { get; set; }
    }
}
