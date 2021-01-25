using System;
using System.Threading.Tasks;
using Domain.Models;
using Domain.UseCases;
using FluentAssertions;
using ImpromptuInterface;
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
            response.Body.Should().BeOfType<MissingParameterException>();
            response.Body.As<MissingParameterException>().Message.Should().Be("Missing Parameter: Name");
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoEmailProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Email"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeOfType<MissingParameterException>();
            response.Body.As<MissingParameterException>().Message.Should().Be("Missing Parameter: Email");
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoPasswordProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("Password"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                PasswordConfirmation = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeOfType<MissingParameterException>();
            response.Body.As<MissingParameterException>().Message.Should().Be("Missing Parameter: Password");
        }

        [Fact]
        public static async Task ShouldReturn400WhenNoPasswordConfirmationProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new MissingParameterException("PasswordConfirmation"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeOfType<MissingParameterException>();
            response.Body.As<MissingParameterException>().Message.Should().Be("Missing Parameter: PasswordConfirmation");
        }

        [Fact]
        public static async Task ShouldReturn400WhenPasswordConfirmationNotMatches()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.ParameterIsEqual("any_password", "other_password", It.IsAny<string>())).Throws(new InvalidParameterException("PasswordConfirmation"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "other_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeOfType<InvalidParameterException>();
            response.Body.As<InvalidParameterException>().Message.Should().Be("Invalid Parameter: PasswordConfirmation");
        }

        [Fact]
        public static async Task ShouldReturn500IfValidatorThrowsOtherException()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.HasRequiredFields(It.IsAny<object>(), It.IsAny<string[]>())).Throws(new Exception());
            validatorMock.Setup(x => x.ParameterIsEqual(It.IsAny<object>(), It.IsAny<string[]>(), It.IsAny<string>())).Throws(new Exception());
            validatorMock.Setup(x => x.IsValidEmail(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(500);
            response.Body.Should().BeOfType<ServerErrorException>();
        }

        [Fact]
        public static async Task ShouldReturn400IfEmailIsInvalid()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.IsValidEmail("invalid_email@mail.com", "Email")).Throws(new InvalidParameterException("Email"));
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "invalid_email@mail.com",
                Password = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeOfType<InvalidParameterException>();
            response.Body.As<InvalidParameterException>().Message.Should().Be("Invalid Parameter: Email");
        }

        [Fact]
        public static async Task ShouldCallAddAccountWithCorrectValues()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "valid_email@mail.com",
                Password = "any_password"
            };
            var response = await sut.HandleAsync(request);
            addAccountMock.Verify(x => x.Add(It.Is<IAddAccountModel>(x => x.Password == request.Password && x.Email == request.Email && x.Name == request.Name)), Times.Once);
        }

        [Fact]
        public static async Task ShouldReturnError500IfAddAccountThrows()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            addAccountMock.Setup(x => x.Add(It.IsAny<IAddAccountModel>())).Throws(new Exception());
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(500);
            response.Body.Should().BeOfType<ServerErrorException>();
        }

        [Fact]
        public static async Task ShouldReturn200IfValidDataIsProvided()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var validAccount = new
            {
                Id = "valid_id",
                Name = "valid_name",
                Email = "valid_email@mail.com",
                Password = "valid_password"
            }.ActLike<IAccount>();
            addAccountMock.Setup(x => x.Add(It.IsAny<IAddAccountModel>())).Returns(Task.Run(() => validAccount));
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new SignUpRequest
            {
                Name = "valid_name",
                Email = "valid_email@mail.com",
                Password = "valid_password",
                PasswordConfirmation = "valid_password"
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(200);
            response.Body.Should().BeEquivalentTo(validAccount);
        }
    }
}
