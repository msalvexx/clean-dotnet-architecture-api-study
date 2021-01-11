using System;
using System.Threading.Tasks;
using Data.UseCases;
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
        public static Mock<IAddAccount> MakeAddAccountMock() => new();
        public static Mock<IValidator> MakeValidatorMock() => new();
        public static SignUpController MakeSut(Mock<IValidator> validatorMock, Mock<IAddAccount> addAccountMock) => new(validatorMock.Object, addAccountMock.Object);

        [Fact]
        public static async Task ShouldReturn400WhenNoNameProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Name"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("Name"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoEmailProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Email"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("Email"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoPasswordProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                PasswordConfirmation = "any_password"
            };
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Password"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("Password"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoPasswordConfirmationProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("PasswordConfirmation"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new MissingParameterException("PasswordConfirmation"));
        }

        [Fact]
        public static async Task ShouldReturn400WhenPasswordConfirmationNotMatches()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "other_password"
            };
            validatorMock.Setup(x => x.ParameterIsEqual("any_password", "other_password", It.IsAny<string>())).Throws(new InvalidParameterException("PasswordConfirmation"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new InvalidParameterException("PasswordConfirmation"));
        }

        [Fact]
        public static async Task ShouldReturn500IfValidatorThrowsOtherException()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new Exception());
            validatorMock.Setup(x => x.ParameterIsEqual(It.IsAny<object>(), It.IsAny<string[]>(), It.IsAny<string>())).Throws(new Exception());
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(500);
            response.Body.Should().IsSameOrEqualTo(new ServerErrorException());
        }

        [Fact]
        public static async Task ShouldReturn400IfEmailIsInvalid()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "invalid_email@mail.com",
                Password = "any_password"
            };
            validatorMock.Setup(x => x.IsValidEmail("invalid_email@mail.com", "Email")).Throws(new InvalidParameterException("Email"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().IsSameOrEqualTo(new InvalidParameterException("Email"));
        }

        [Fact]
        public static async Task ShouldCallAddAccountWithCorrectValues()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "valid_email@mail.com",
                Password = "any_password"
            };
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            addAccountMock.Verify(x => x.Add(It.Is<IAddAccountModel>(x => x.Password == request.Password && x.Email == request.Email && x.Name == request.Name)), Times.Once);
        }

        [Fact]
        public static async Task ShouldReturnError500IfAddAccountThrows()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            addAccountMock.Setup(x => x.Add(It.IsAny<IAddAccountModel>())).Throws(new Exception());
            var sut = MakeSut(validatorMock, addAccountMock);
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(500);
            response.Body.Should().IsSameOrEqualTo(new ServerErrorException());
        }
    }
}
