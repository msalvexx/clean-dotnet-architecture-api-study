using System.Threading.Tasks;
using ImpromptuInterface;
using Presentation.Protocols;

namespace Presentation.Helpers
{
    public static class HttpHelper
    {
        public static Task<IHttpResponse<object>> BadRequest(object body) =>
            Task.Run(() => new
            {
                Body = body,
                Status = 400
            }.ActLike<IHttpResponse<object>>());
        public static Task<IHttpResponse<object>> Success(object body) =>
            Task.Run(() => new
            {
                Body = body,
                Status = 200
            }.ActLike<IHttpResponse<object>>());
    }
}