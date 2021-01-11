using System;
using System.Threading.Tasks;
using Data.UseCases;
using ImpromptuInterface;
using Presentation.Exceptions;
using Presentation.Helpers;
using Presentation.Protocols;

namespace Presentation.Controllers.SignUp
{
    public class SignUpController
    {
        private readonly IValidator validator;
        private readonly IAddAccount addAccount;
        public SignUpController(IValidator validator, IAddAccount addAccount)
        {
            this.validator = validator;
            this.addAccount = addAccount;
        }
        public async Task<IHttpResponse<object>> HandleAsync(ISignupRequest request)
        {
            try
            {
                this.ValidateOrThrows(request);
                var account = await this.addAccount.Add(new
                {
                    request.Name,
                    request.Email,
                    request.Password
                }.ActLike<IAddAccountModel>());
                return await HttpHelper.Success(account);
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
