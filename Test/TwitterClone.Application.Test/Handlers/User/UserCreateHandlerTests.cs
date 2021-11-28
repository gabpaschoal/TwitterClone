using FluentAssertions;
using Moq;
using System.Linq;
using TwitterClone.Application.Commands.User;
using TwitterClone.Application.Handlers;
using TwitterClone.Application.Handlers.User;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Domain.Services;
using Xunit;

namespace TwitterClone.Application.Test.Handlers.User;

public class UserCreateHandlerTests
{
    private static UserCreateHandler MakeSut(
        IHandlerBus? handlerBus = null,
        IUserRepository? userRepository = null,
        IEncryptionService? encryptionService = null)
    {
        handlerBus ??= new Mock<IHandlerBus>().Object;
        userRepository ??= new Mock<IUserRepository>().Object;
        encryptionService ??= new Mock<IEncryptionService>().Object;

        return new UserCreateHandler(handlerBus, userRepository, encryptionService);
    }

    private static UserCreateCommand MakeValidCommand()
        => new(Name: "Gustav", NickName: "G986", Email: "gustav@mail.com", Password: "123", PasswordConfirmation: "123");

    [Fact(DisplayName = "Should be invalid when NickName is already in use")]
    public void Should_be_invalid_when_NickName_is_already_in_use()
    {
        var command = MakeValidCommand();

        Mock<IUserRepository> userRepositoryMock = new();
        userRepositoryMock.Setup(x => x.ExistsUserWithThisNickName(command.NickName)).Returns(true);

        var sut = MakeSut(userRepository: userRepositoryMock.Object);

        var resultData = sut.HandleExecution(command, cancellationToken: System.Threading.CancellationToken.None).Result;

        resultData.IsValid.Should().BeFalse();
        resultData.FieldErrors.Single().Key.Should().Be(nameof(command.NickName));
    }

    [Fact(DisplayName = "Should be invalid when Email is already in use")]
    public void Should_be_invalid_when_Email_is_already_in_use()
    {
        var command = MakeValidCommand();

        Mock<IUserRepository> userRepositoryMock = new();
        userRepositoryMock.Setup(x => x.ExistsUserWithThisEmail(command.Email)).Returns(true);

        var sut = MakeSut(userRepository: userRepositoryMock.Object);

        var resultData = sut.HandleExecution(command, cancellationToken: System.Threading.CancellationToken.None).Result;

        resultData.IsValid.Should().BeFalse();
        resultData.FieldErrors.Single().Key.Should().Be(nameof(command.Email));
    }

    [Fact(DisplayName = "Should call AddAsync one time when validations result valid")]
    public void Should_call_AddAsync_one_time_when_validations_result_valid()
    {
        var command = MakeValidCommand();
        string encryptedStringMocked = "mockedString";

        Mock<IUserRepository> userRepositoryMock = new();
        Mock<IEncryptionService> encryptionServiceMock = new();
        encryptionServiceMock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encryptedStringMocked);

        var sut = MakeSut(userRepository: userRepositoryMock.Object, encryptionService: encryptionServiceMock.Object);
        var resultData = sut.HandleExecution(command, cancellationToken: System.Threading.CancellationToken.None).Result;

        resultData.IsValid.Should().BeTrue();
        encryptionServiceMock.Verify(x => x.Encrypt(It.IsAny<string>()), Times.Once);
        userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.Entities.User>()), Times.Once);
        userRepositoryMock.Verify(x => x.AddAsync(It.Is<Domain.Entities.User>(x =>
                                                        x.Name.Equals(command.Name)
                                                            && x.NickName.Equals(command.NickName)
                                                            && x.Email.Equals(command.Email)
                                                            && x.Password.Equals(encryptedStringMocked))));
    }
}
