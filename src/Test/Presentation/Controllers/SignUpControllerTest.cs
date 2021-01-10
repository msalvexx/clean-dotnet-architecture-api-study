using System.Threading.Tasks;
using FluentAssertions;
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
            var request = new SignUpRequest
            {
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeEquivalentTo(new MissingParameterException("Name"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoEmailProvided()
        {
            var sut = new SignUpController();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeEquivalentTo(new MissingParameterException("Email"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoPasswordProvided()
        {
            var sut = new SignUpController();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                PasswordConfirmation = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeEquivalentTo(new MissingParameterException("Password"));
        }
    }
}
