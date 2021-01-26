using System;
using FluentAssertions;
using FluentAssertions.Common;
using Presentation.Exceptions;
using Utils.Validators;
using Xunit;

namespace Test.Utils
{
    public class CompareFieldsValidationTest
    {
        private static CompareFieldsValidation MakeSut() => new("Password", "PasswordConfirmation");

        [Fact]
        public void ShouldThrowInvalidParameterExceptionIfParametersDiferent()
        {
            var sut = MakeSut();
            var request = new
            {
                Password = "Matheus",
                PasswordConfirmation = "wrong_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().Throw<InvalidParameterException>().IsSameOrEqualTo(new InvalidParameterException("ParameterName"));
        }

        [Fact]
        public void ShouldThrowInvalidParameterExceptionIfAnyParameterIsMissing()
        {
            var sut = MakeSut();
            var request = new
            {
                PasswordConfirmation = "wrong_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().Throw<InvalidParameterException>().IsSameOrEqualTo(new InvalidParameterException("ParameterName"));
        }

        [Fact]
        public void ShouldNotThrowIfParametersEqual()
        {
            var sut = MakeSut();
            var request = new
            {
                Password = "any_password",
                PasswordConfirmation = "any_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().NotThrow<InvalidParameterException>();
        }
    }
}
