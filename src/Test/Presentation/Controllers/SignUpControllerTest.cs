using System;
using System.Collections.Generic;
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
            var request = new HttpRequest
            {
                Body = new SignUpRequest
                {
                    Name = "any_name",
                    Email = "any_email@mail.com",
                    Password = "any_password",
                    PasswordConfirmation = "any_password"
                }
            };
            var response = await sut.HandleAsync(request);
            validatorMock.Verify(x => x.Validate(It.IsAny<object>()), Times.Once);
        }

        [Theory]
        [MemberData(nameof(GetValidationExceptions))]
        public static async Task ShouldReturn400IfValidatorThrows(Exception e)
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.Validate(It.IsAny<object>())).Throws(e);
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new HttpRequest
            {
                Body = new SignUpRequest
                {
                    Name = "any_name",
                    Email = "any_email@mail.com",
                    Password = "any_password"
                }
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(400);
            response.Body.Should().BeOfType(e.GetType());
        }

        public static IEnumerable<object[]> GetValidationExceptions =>
            new List<object[]>
            {
                new object[] { new InvalidParameterException("Field") },
                new object[] { new MissingParameterException("Field") }
            };


        [Fact]
        public static async Task ShouldReturn500IfValidatorThrowsOtherException()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            validatorMock.Setup(x => x.Validate(It.IsAny<object>())).Throws(new Exception());
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new HttpRequest
            {
                Body = new SignUpRequest
                {
                    Name = "any_name",
                    Email = "any_email@mail.com",
                    Password = "any_password"
                }
            };
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
            var request = new HttpRequest
            {
                Body = new SignUpRequest
                {
                    Name = "any_name",
                    Email = "valid_email@mail.com",
                    Password = "any_password"
                }
            };
            var response = await sut.HandleAsync(request);
            addAccountMock.Verify(x => x.Add(It.IsAny<IAddAccountModel>()), Times.Once);
        }

        [Fact]
        public static async Task ShouldReturnError500IfAddAccountThrows()
        {
            var validatorMock = MakeValidatorMock();
            var addAccountMock = MakeAddAccountMock();
            addAccountMock.Setup(x => x.Add(It.IsAny<IAddAccountModel>())).Throws(new Exception());
            var sut = MakeSut(validatorMock, addAccountMock);
            var request = new HttpRequest
            {
                Body = new SignUpRequest
                {
                    Name = "any_name",
                    Email = "any_email@mail.com",
                    Password = "any_password",
                    PasswordConfirmation = "any_password"
                }
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
            var request = new HttpRequest
            {
                Body = new SignUpRequest
                {
                    Name = "valid_name",
                    Email = "valid_email@mail.com",
                    Password = "valid_password",
                    PasswordConfirmation = "valid_password"
                }
            };
            var response = await sut.HandleAsync(request);
            response.Status.Should().Be(200);
            response.Body.Should().BeEquivalentTo(validAccount);
        }
    }
}
