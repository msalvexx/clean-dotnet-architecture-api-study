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
                return await HttpHelper.ServerError();
            }
        }

        public void ValidateOrThrows(ISignupRequest request)
        {
            this.validator.HasRequiredFields(request, new[] { "Name", "Email", "Password", "PasswordConfirmation" });
            this.validator.ParameterIsEqual(request.Password, request.PasswordConfirmation, "PasswordConfirmation");
            this.validator.IsValidEmail(request.Email, "Email");
        }
    }
}
