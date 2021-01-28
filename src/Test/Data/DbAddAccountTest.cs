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
        private static Mock<IAddAccountRepository> MakeAddAccountRepository() => new();
        private static Mock<IHasher> MakeHasher() => new();
        private static DbAddAccount MakeSut(Mock<IHasher> hasherMock, Mock<IAddAccountRepository> addAccountRepositoryMock) => new(hasherMock.Object, addAccountRepositoryMock.Object);

        private static IAddAccountModel MakeData() => new
        {
            Name = "any_name",
            Email = "any_email@mail.com",
            Password = "any_password"
        }.ActLike<IAddAccountModel>();

        [Fact]
        public void ShouldCallHasherWithCorrectValue()
        {
            var addAccountMock = MakeAddAccountRepository();
            var hasherMock = MakeHasher();
            var sut = MakeSut(hasherMock, addAccountMock);
            var data = MakeData();
            sut.Add(data);
            hasherMock.Verify(x => x.Generate(data.Password), Times.Once);
        }

        [Fact]
        public void ShouldDbAddAccountThrowIfHasherThrows()
        {
            var addAccountMock = MakeAddAccountRepository();
            var hasherMock = MakeHasher();
            hasherMock.Setup(x => x.Generate(It.IsAny<string>())).Throws(new Exception());
            var sut = MakeSut(hasherMock, addAccountMock);
            var data = MakeData();
            Action act = () => sut.Add(data);
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void ShouldCallIAddAccountRepositoryWithCorrectValues()
        {
            var addAccountMock = MakeAddAccountRepository();
            var hasherMock = MakeHasher();
            hasherMock.Setup(x => x.Generate(It.IsAny<string>())).Returns("hashed_value");
            var sut = MakeSut(hasherMock, addAccountMock);
            var data = MakeData();
            sut.Add(data);
            var expectedData = new AddAccountModel
            {
                Name = "any_name",
                Email = "any_email@mail.com",
                Password = "hashed_value"
            };
            addAccountMock.Verify(x => x.Add(It.Is<IAddAccountModel>(x => x.Equals(expectedData))), Times.Once);
        }
    }
}
