using System;
using System.Threading.Tasks;
using Data.Protocols;
using Data.UseCases;
using Domain.Models;
using Domain.UseCases;
using FluentAssertions;
using Moq;
using Xunit;

namespace Test.Data
{
    public class DbAddAccountTest
    {
        private static Mock<IAddAccountRepository> MakeAddAccountRepository() => new();
        private static Mock<IHasher> MakeHasher() => new();
        private static DbAddAccount MakeSut(Mock<IHasher> hasherMock, Mock<IAddAccountRepository> addAccountRepositoryMock) => new(hasherMock.Object, addAccountRepositoryMock.Object);
        private static IAddAccountModel MakeData() => new AddAccountModel
        {
            Name = "any_name",
            Email = "any_email@mail.com",
            Password = "any_password"
        };
        private static IAccount MakeFakeAccount() => new Account
        {
            Id = "any_id",
            Name = "any_name",
            Email = "any_email@mail.com",
            Password = "hashed_password"
        };

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

        [Fact]
        public void ShouldDbAddAccountThrowIfAddAccountRepositoryThrows()
        {
            var addAccountMock = MakeAddAccountRepository();
            var hasherMock = MakeHasher();
            addAccountMock.Setup(x => x.Add(It.IsAny<IAddAccountModel>())).Throws(new Exception());
            var sut = MakeSut(hasherMock, addAccountMock);
            var data = MakeData();
            Action act = () => sut.Add(data);
            act.Should().Throw<Exception>();
        }

        [Fact]
        public async Task ShouldReturnAnAccountOnSuccess()
        {
            var addAccountMock = MakeAddAccountRepository();
            addAccountMock.Setup(x => x.Add(It.IsAny<IAddAccountModel>())).Returns(Task.FromResult(MakeFakeAccount()));
            var hasherMock = MakeHasher();
            var sut = MakeSut(hasherMock, addAccountMock);
            var data = MakeData();
            var account = await sut.Add(data);
            account.Should().BeEquivalentTo(MakeFakeAccount());
        }
    }
}
