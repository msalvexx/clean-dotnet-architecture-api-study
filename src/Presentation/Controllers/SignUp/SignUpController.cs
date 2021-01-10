using System.Threading.Tasks;
using Presentation.Exceptions;
using Presentation.Helpers;
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
                    return await HttpHelper.BadRequest(new MissingParameterException(field));
                }
            }
            if (!request.Password.Equals(request.PasswordConfirmation, System.StringComparison.Ordinal))
            {
                return await HttpHelper.BadRequest(new InvalidParameterException("PasswordConfirmation"));
            }
            return await HttpHelper.Success("Ok");
        }
    }
}
