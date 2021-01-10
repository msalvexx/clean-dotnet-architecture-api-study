using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using Presentation.Controllers.SignUp;
using Presentation.Exceptions;
using Presentation.Protocols;
using Xunit;

namespace Test.Presentation.Controllers
{
    public class SignUpControllerTest
    {
        public static Mock<IValidator> MakeValidatorMock()
        {
            var validatorMock = new Mock<IValidator>();
            return validatorMock;
        }
        public static SignUpController MakeSut(Mock<IValidator> validatorMock) => new(validatorMock.Object);

        [Fact]
        public static async Task ShouldReturn400WhenNoNameProvided()
        {
            var validatorMock = MakeValidatorMock();
            var request = new SignUpRequest
            {
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            validatorMock.Setup(x => x.AssertHasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Name"));
            var sut = MakeSut(validatorMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("Name"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoEmailProvided()
        {
            var validatorMock = MakeValidatorMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            validatorMock.Setup(x => x.AssertHasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Email"));
            var sut = MakeSut(validatorMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("Email"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoPasswordProvided()
        {
            var validatorMock = MakeValidatorMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                PasswordConfirmation = "any_password"
            };
            validatorMock.Setup(x => x.AssertHasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Password"));
            var sut = MakeSut(validatorMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("Password"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoPasswordConfirmationProvided()
        {
            var validatorMock = MakeValidatorMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            validatorMock.Setup(x => x.AssertHasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("PasswordConfirmation"));
            var sut = MakeSut(validatorMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("PasswordConfirmation"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenPasswordConfirmationNotMatches()
        {
            var validatorMock = MakeValidatorMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "other_password"
            };
            validatorMock.Setup(x => x.AssertParameterIsEqual("any_password", "other_password", It.IsAny<string>())).Throws(new InvalidParameterException("PasswordConfirmation"));
            var sut = MakeSut(validatorMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new InvalidParameterException("PasswordConfirmation"));
        }
    }
}
