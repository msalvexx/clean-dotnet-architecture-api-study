using System.Threading.Tasks;
using ImpromptuInterface;
using Presentation.Exceptions;
using Presentation.Protocols;

namespace Presentation.Controllers.SignUp
{
    public class SignUpController
    {
        public async Task<IHttpResponse<object>> HandleAsync(ISignupRequest request) =>
            await Task.Run(() => new
            {
                Body = new MissingParameterException("Name"),
                Status = 400
            }.ActLike<IHttpResponse<object>>());
    }
}
