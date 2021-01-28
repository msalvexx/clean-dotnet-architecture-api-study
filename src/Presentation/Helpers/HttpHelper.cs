using System.Threading.Tasks;
using Presentation.Exceptions;
using Presentation.Protocols;

namespace Presentation.Helpers
{
    public static class HttpHelper
    {
        public static Task<IHttpResponse> BadRequest(object body) =>
            Task.Run<IHttpResponse>(() => new HttpResponse
            {
                Body = body,
                Status = 400
            });

        public static Task<IHttpResponse> Success(object body) =>
            Task.Run<IHttpResponse>(() => new HttpResponse
            {
                Body = body,
                Status = 200
            });

        public static Task<IHttpResponse> ServerError() =>
            Task.Run<IHttpResponse>(() => new HttpResponse
            {
                Body = new ServerErrorException(),
                Status = 500
            });
    }
}
