using System;
using System.Threading.Tasks;
using Presentation.Exceptions;
using Presentation.Helpers;
using Presentation.Protocols;

namespace Presentation.Controllers.SignUp
{
    public class SignUpController
    {
        private readonly IValidator validator;
        public SignUpController(IValidator validator) => this.validator = validator;
        public async Task<IHttpResponse<object>> HandleAsync(ISignupRequest request)
        {
            try
            {
                this.ValidateOrThrows(request);
                return await HttpHelper.Success("Ok");
            }
            catch (Exception ex)
            {
                if (ex is MissingParameterException or InvalidParameterException)
                {
                    return await HttpHelper.BadRequest(ex);
                }
                return null;
            }
        }

        public void ValidateOrThrows(ISignupRequest request)
        {
            var requiredFields = new[] { "Name", "Email", "Password", "PasswordConfirmation" };
            this.validator.AssertHasRequiredFields(request, requiredFields);
            this.validator.AssertParameterIsEqual(request.Password, request.PasswordConfirmation, "PasswordConfirmation");
        }
    }
}
