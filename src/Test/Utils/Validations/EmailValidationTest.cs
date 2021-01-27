using System;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using Presentation.Exceptions;
using Utils.Protocols;
using Utils.Validators;
using Xunit;

namespace Test.Utils
{
    public class EmailValidationTest
    {
        public static EmailValidation MakeSut(IEmailValidator emailValidator) => new("Email", emailValidator);
        public static Mock<IEmailValidator> MakeEmailValidatorMock() => new();

        [Fact]
        public static void ShouldThrowInvalidParameterExceptionIfEmailIsInvalid()
        {
            var emailValidatorMock = MakeEmailValidatorMock();
            emailValidatorMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);
            var sut = MakeSut(emailValidatorMock.Object);
            var request = new
            {
                Email = "wrong_email@mail.com",
                Password = "any_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().Throw<InvalidParameterException>().IsSameOrEqualTo(new InvalidParameterException("Email"));
        }

        [Fact]
        public static void ShouldCallIEmailValidatorWithCorrectEmail()
        {
            var emailValidatorMock = MakeEmailValidatorMock();
            emailValidatorMock.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            var sut = MakeSut(emailValidatorMock.Object);
            var request = new
            {
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            sut.Validate(request);
            emailValidatorMock.Verify(x => x.IsValid("any_email@mail.com"));
        }
    }
}
