using System;
using System.Threading.Tasks;
using Domain.UseCases;
using Presentation.Exceptions;
using Presentation.Helpers;
using Presentation.Protocols;

namespace Presentation.Controllers.SignUp
{
    public class SignUpController : IController<ISignUpRequest>
    {
        private readonly IValidator validator;
        private readonly IAddAccount addAccount;

        public SignUpController(IValidator validator, IAddAccount addAccount)
        {
            this.validator = validator;
            this.addAccount = addAccount;
        }

        public async Task<IHttpResponse> HandleAsync(IHttpRequest<ISignUpRequest> request)
        {
            try
            {
                this.validator.Validate(request.Body);
                var account = await this.addAccount.Add(new AddAccountModel
                {
                    Name = request.Body.Name,
                    Email = request.Body.Email,
                    Password = request.Body.Password
                });
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
