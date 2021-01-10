using System;
using FluentAssertions;
using FluentAssertions.Common;
using Presentation.Exceptions;
using Utils;
using Xunit;

namespace Test.Utils
{
    public class ValidatorAdapterTest
    {
        [Fact]
        public void ShouldThrowInvalidParameterExceptionIfParametersDiferent()
        {
            var sut = new ValidatorAdapter();
            Action act = () => sut.AssertParameterIsEqual("any", "other", "ParameterName");
            act.Should().Throw<InvalidParameterException>().IsSameOrEqualTo(new InvalidParameterException("ParameterName"));
        }

        [Fact]
        public void ShouldNotThrowIfParametersEqual()
        {
            var sut = new ValidatorAdapter();
            Action act = () => sut.AssertParameterIsEqual("any", "any", "ParameterName");
            act.Should().NotThrow<InvalidParameterException>();
        }

        [Fact]
        public void ShouldThrowMissingParameterExceptionIfOneParameterNotExistsInRequest()
        {
            var sut = new ValidatorAdapter();
            var request = new
            {
                Email = "any_email",
                Password = "any_password"
            };
            Action act = () => sut.AssertHasRequiredFields(request, new[] { "Name", "Email", "Password" });
            act.Should().Throw<MissingParameterException>().IsSameOrEqualTo(new MissingParameterException("Name"));
        }

        [Fact]
        public void ShouldThrowMissingParametersExceptionIfMultipleParametersNotExistsInRequest()
        {
            var sut = new ValidatorAdapter();
            var request = new
            {
                Password = "any_password"
            };
            Action act = () => sut.AssertHasRequiredFields(request, new[] { "Name", "Email", "Password" });
            act.Should().Throw<MissingParameterException>().IsSameOrEqualTo(new MissingParameterException(new[] { "Name", "Email" }));
        }

        [Fact]
        public void ShouldNotThrowMissingParametersExceptionIfRequestContainsRequiredParameters()
        {
            var sut = new ValidatorAdapter();
            var request = new
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "any_password"
            };
            Action act = () => sut.AssertHasRequiredFields(request, new[] { "Name", "Email", "Password" });
            act.Should().NotThrow<MissingParameterException>();
        }
    }
}
