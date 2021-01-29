using System.Threading.Tasks;

namespace Presentation.Protocols
{
    public interface IController<TRequest> where TRequest : class
    {
        Task<IHttpResponse> HandleAsync(IHttpRequest<TRequest> request);
    }
}
