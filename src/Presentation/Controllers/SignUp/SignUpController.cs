using System;
using System.Threading.Tasks;
using Domain.UseCases;
using ImpromptuInterface;
using Presentation.Exceptions;
using Presentation.Helpers;
using Presentation.Protocols;

namespace Presentation.Controllers.SignUp
{
    public class SignUpController : IController
    {
        private readonly IValidator validator;
        private readonly IAddAccount addAccount;

        public SignUpController(IValidator validator, IAddAccount addAccount)
        {
            this.validator = validator;
            this.addAccount = addAccount;
        }

        public async Task<IHttpResponse> HandleAsync(IHttpRequest request)
        {
            try
            {
                dynamic body = request.Body;
                this.validator.Validate(body);
                var account = await this.addAccount.Add(new
                {
                    body.Name,
                    body.Email,
                    body.Password
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
