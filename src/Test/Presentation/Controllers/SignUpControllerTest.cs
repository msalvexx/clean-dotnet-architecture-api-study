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
        public static async Task ShouldCallIValidatorWithCorrectValues()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            }.ActLike<ISignUpRequest>();
            var response = await sut.HandleAsync(request);
            validatorMock.Verify(x => x.Validate(It.Is<ISignUpRequest>(x => x.Name == request.Name &&
                                                                        x.Email == request.Email &&
                                                                        x.Password == request.Password &&
                                                                        x.PasswordConfirmation == request.PasswordConfirmation)), Times.Once);
        }

        [Fact]
        public static async Task ShouldReturn500IfValidatorThrowsOtherException()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.Validate(It.IsAny<object>())).Throws(new Exception());
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            }.ActLike<ISignUpRequest>();
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(500);
            response.Body.Should().BeOfType<ServerErrorException>();
        }

        [Fact]
        public static async Task ShouldCallAddAccountWithCorrectValues()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new
            {
                Name = "any_name",
                Email = "valid_email@mail.com",
                Password = "any_password"
            }.ActLike<ISignUpRequest>();
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
            var request = new
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password",
                PasswordConfirmation = "any_password"
            }.ActLike<ISignUpRequest>();
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
            var request = new
            {
                Name = "valid_name",
                Email = "valid_email@mail.com",
                Password = "valid_password",
                PasswordConfirmation = "valid_password"
            }.ActLike<ISignUpRequest>();
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(200);
            response.Body.Should().BeEquivalentTo(validAccount);
        }
    }
}
