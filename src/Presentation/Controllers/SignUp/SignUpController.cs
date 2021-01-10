using System.Threading.Tasks;
using ImpromptuInterface;
using Presentation.Exceptions;
using Presentation.Protocols;

namespace Presentation.Controllers.SignUp
{
    public class SignUpController
    {
        public async Task<IHttpResponse<object>> HandleAsync(ISignupRequest request)
        {
            var requiredFields = new[] { "Name", "Email", "Password", "PasswordConfirmation" };
            foreach (var field in requiredFields)
            {
                var hasField = request.GetType().GetProperty(field).GetValue(request) != null;
                if (!hasField)
                {
                    return await Task.Run(() => new
                    {
                        Body = new MissingParameterException(field),
                        Status = 400
                    }.ActLike<IHttpResponse<object>>());
                }
            }
            return new
            {
                Body = "Ok",
                Status = 200
            }.ActLike<IHttpResponse<object>>();
        }
    }
}
