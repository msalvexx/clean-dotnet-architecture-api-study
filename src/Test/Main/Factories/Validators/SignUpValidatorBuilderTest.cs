using Main.Factories.Validators;
using Moq;
using ObjectsComparer;
using Presentation.Protocols;
using Utils.Protocols;
using Xunit;
using Validations = Utils.Validators;

namespace Test.Main.Factories.Validators
{
    public class SignUpValidatorBuilderTest
    {
        private static Mock<IEmailValidator> MakeEmailValidatorMock() => new();
        private static IValidator[] MakeValidators(Mock<IEmailValidator> emailValidatorMock) => new IValidator[]
            {
                new Validations.RequiredFieldValidation(new[] { "Name", "Email", "Password", "PasswordConfirmation" }),
                new Validations.CompareFieldsValidation("Password", "PasswordConfirmation"),
                new Validations.EmailValidation("Email", emailValidatorMock.Object)
            };
        private static Validations.CompositeValidation MakeSut() => SignUpValidatorBuilder.Create();

        [Fact]
        public void ShouldReturnValidatorCompositeWithCorrectValidators()
        {
            var emailValidatorMock = MakeEmailValidatorMock();
            var validators = MakeValidators(emailValidatorMock);
            var sut = MakeSut();
            var comparer = new Comparer<IValidator[]>();
            Assert.True(comparer.Compare(sut.GetValidators(), validators));
        }
    }
}
