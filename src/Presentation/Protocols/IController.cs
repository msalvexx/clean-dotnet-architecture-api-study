using System.Threading.Tasks;

namespace Presentation.Protocols
{
    public interface IController
    {
        Task<IHttpResponse> HandleAsync(IHttpRequest request);
    }
}
