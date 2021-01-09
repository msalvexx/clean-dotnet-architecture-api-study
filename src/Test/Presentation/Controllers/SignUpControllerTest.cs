using System.Threading.Tasks;
using FluentAssertions;
using ImpromptuInterface;
using Presentation.Controllers.SignUp;
using Presentation.Exceptions;
using Xunit;

namespace Test.Presentation.Controllers
{
    public class SignUpControllerTest
    {
        [Fact]
        public static async Task ShouldReturn400WhenNoNameProvided()
        {
            var sut = new SignUpController();
            var request = (
                Email: "any_email@mail.com",
                Password: "any_password",
                PasswordConfirmation: "any_password"
            ).ActLike<ISignupRequest>();
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeEquivalentTo(new MissingParameterException("Name"));
        }
    }
}
