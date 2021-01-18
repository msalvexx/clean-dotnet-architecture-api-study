using System;
using FluentAssertions;
using FluentAssertions.Common;
using Presentation.Exceptions;
using Utils.Validators;
using Xunit;

namespace Test.Utils
{
    public class RequiredFieldValidatorTest
    {
        private static RequiredFieldValidator MakeSut() => new(new[] { "Name", "Email", "Password" });

        [Fact]
        public void ShouldThrowMissingParameterExceptionIfOneParameterNotExistsInRequest()
        {
            var sut = MakeSut();
            var request = new
            {
                Email = "any_email",
                Password = "any_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().Throw<MissingParameterException>().IsSameOrEqualTo(new MissingParameterException("Name"));
        }

        [Fact]
        public void ShouldThrowMissingParametersExceptionIfMultipleParametersNotExistsInRequest()
        {
            var sut = MakeSut();
            var request = new
            {
                Password = "any_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().Throw<MissingParameterException>().IsSameOrEqualTo(new MissingParameterException(new[] { "Name", "Email" }));
        }

        [Fact]
        public void ShouldNotThrowMissingParametersExceptionIfRequestContainsRequiredParameters()
        {
            var sut = MakeSut();
            var request = new
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            Action act = () => sut.Validate(request);
            act.Should().NotThrow<MissingParameterException>();
        }
    }
}
