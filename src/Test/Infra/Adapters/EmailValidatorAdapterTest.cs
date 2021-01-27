using FluentAssertions;
using Infra.Adapters;
using Xunit;

namespace Test.Infra.Adapters
{
    public class EmailValidatorAdapterTest
    {
        private static EmailValidatorAdapter MakeSut() => new();

        [Fact]
        public void ShouldReturnFalseIfEmailIsInvalid()
        {
            var sut = MakeSut();
            var result = sut.IsValid("any_email@mail");
            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnTrueIfEmailIsValid()
        {
            var sut = MakeSut();
            var result = sut.IsValid("any_email@mail.com");
            result.Should().BeTrue();
        }
    }
}
