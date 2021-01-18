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
            Action act = () => sut.ParameterIsEqual("any", "other", "ParameterName");
            act.Should().Throw<InvalidParameterException>().IsSameOrEqualTo(new InvalidParameterException("ParameterName"));
        }

        [Fact]
        public void ShouldNotThrowIfParametersEqual()
        {
            var sut = new ValidatorAdapter();
            Action act = () => sut.ParameterIsEqual("any", "any", "ParameterName");
            act.Should().NotThrow<InvalidParameterException>();
        }
    }
}
