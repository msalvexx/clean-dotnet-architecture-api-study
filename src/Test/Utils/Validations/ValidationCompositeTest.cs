using Moq;
using Presentation.Protocols;
using Utils.Validators;
using Xunit;

namespace Test.Utils
{

    public class CompositeValidationTest
    {
        private static Mock<IValidator> MakeValidatorMock<T>() where T : IValidator => new();

        private static CompositeValidation MakeSut(IValidator[] validators) => new(validators);

        [Fact]
        public void ShouldCallValidateForeachValidatorInArray()
        {
            var compareFieldsMock = MakeValidatorMock<CompareFieldsValidation>();
            var emailValidationMock = MakeValidatorMock<EmailValidation>();
            var sut = MakeSut(new[] { compareFieldsMock.Object, emailValidationMock.Object });
            var data = new
            {
                Field = "any_field"
            };
            sut.Validate(data);
            compareFieldsMock.Verify(x => x.Validate(It.IsAny<object>()), Times.Once);
            emailValidationMock.Verify(x => x.Validate(It.IsAny<object>()), Times.Once);
        }
    }
}
