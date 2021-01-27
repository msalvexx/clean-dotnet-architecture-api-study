using System;
using System.Threading.Tasks;
using Domain.UseCases;
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

        public async Task<IHttpResponse<object>> HandleAsync(ISignUpRequest request)
        {
            try
            {
                this.validator.Validate(request);
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
    }
}
