using System;
using FluentAssertions;
using Moq;
using Presentation.Exceptions;
using Presentation.Protocols;
using Xunit;
using Validators = Utils.Validators;

namespace Test.Utils
{

    public class CompositeValidationTest
    {
        private static Mock<IValidator> MakeValidatorMock<T>() where T : IValidator => new();

        private static Validators.CompositeValidation MakeSut(IValidator[] validators) => new(validators);

        [Fact]
        public void ShouldCallValidateForeachValidatorInArray()
        {
            var compareFieldsMock = MakeValidatorMock<Validators.CompareFieldsValidation>();
            var emailValidationMock = MakeValidatorMock<Validators.EmailValidation>();
            var sut = MakeSut(new[] { compareFieldsMock.Object, emailValidationMock.Object });
            var data = new
            {
                Field = "any_field"
            };
            sut.Validate(data);
            compareFieldsMock.Verify(x => x.Validate(It.IsAny<object>()), Times.Once);
            emailValidationMock.Verify(x => x.Validate(It.IsAny<object>()), Times.Once);
        }

        [Fact]
        public void ShouldThrowIfAnyValidatorThrows()
        {
            var compareFieldsMock = MakeValidatorMock<Validators.CompareFieldsValidation>();
            var emailValidationMock = MakeValidatorMock<Validators.EmailValidation>();
            compareFieldsMock.Setup(x => x.Validate(It.IsAny<object>())).Throws(new InvalidParameterException("Field"));
            var sut = MakeSut(new[] { compareFieldsMock.Object, emailValidationMock.Object });
            var data = new
            {
                Field = "any_field"
            };
            Action act = () => sut.Validate(data);
            act.Should().Throw<Exception>();
        }
    }
}
