using System.Threading.Tasks;
using ImpromptuInterface;
using Presentation.Exceptions;
using Presentation.Protocols;

namespace Presentation.Helpers
{
    public static class HttpHelper
    {
        public static Task<IHttpResponse> BadRequest(object body) =>
            Task.Run(() => new
            {
                Body = body,
                Status = 400
            }.ActLike<IHttpResponse>());

        public static Task<IHttpResponse> Success(object body) =>
            Task.Run(() => new
            {
                Body = body,
                Status = 200
            }.ActLike<IHttpResponse>());

        public static Task<IHttpResponse> ServerError() =>
            Task.Run(() => new
            {
                Body = new ServerErrorException(),
                Status = 500
            }.ActLike<IHttpResponse>());
    }
}
