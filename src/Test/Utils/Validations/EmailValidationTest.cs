using System;
using FluentAssertions;
using FluentAssertions.Common;
using Presentation.Exceptions;
using Utils.Validators;
using Xunit;

namespace Test.Utils
{
    public class EmailValidationTest
    {
        public static EmailValidation MakeSut() => new("Email");

        [Fact]
        public static void ShouldThrowInvalidParameterExceptionIfEmailIsInvalid()
        {
            var sut = MakeSut();
            var request = new
            {
                Email = "any_email",
                Password = "any_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().Throw<InvalidParameterException>().IsSameOrEqualTo(new InvalidParameterException("Email"));
        }
    }
}
