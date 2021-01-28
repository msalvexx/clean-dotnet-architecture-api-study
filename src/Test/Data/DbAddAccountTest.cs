using System;
using Data.Protocols;
using Data.UseCases;
using Domain.UseCases;
using FluentAssertions;
using ImpromptuInterface;
using Moq;
using Xunit;

namespace Test.Data
{
    public class DbAddAccountTest
    {
        private static Mock<IHasher> MakeHasher() => new();
        private static DbAddAccount MakeSut(Mock<IHasher> hasherMock) => new(hasherMock.Object);

        private static IAddAccountModel MakeData() => new
        {
            Name = "any_name",
            Email = "any_email@mail.com",
            Password = "any_password"
        }.ActLike<IAddAccountModel>();

        [Fact]
        public void ShouldCallHasherWithCorrectValue()
        {
            var hasherMock = MakeHasher();
            var sut = MakeSut(hasherMock);
            var data = MakeData();
            sut.Add(data);
            hasherMock.Verify(x => x.Generate(data.Password), Times.Once);
        }

        [Fact]
        public void ShouldDbAddAccountThrowIfHasherThrows()
        {
            var hasherMock = MakeHasher();
            hasherMock.Setup(x => x.Generate(It.IsAny<string>())).Throws(new Exception());
            var sut = MakeSut(hasherMock);
            var data = MakeData();
            Action act = () => sut.Add(data);
            act.Should().Throw<Exception>();
        }
    }
}
