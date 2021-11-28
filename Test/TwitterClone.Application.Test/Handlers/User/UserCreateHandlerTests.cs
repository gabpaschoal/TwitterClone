using FluentAssertions;
using Moq;
using System.Linq;
using TwitterClone.Application.Commands.User;
using TwitterClone.Application.Handlers;
using TwitterClone.Application.Handlers.User;
using TwitterClone.Domain.Repositories.Data;
using Xunit;

namespace TwitterClone.Application.Test.Handlers.User;

public class UserCreateHandlerTests
{
    private static UserCreateHandler MakeSut(
        IHandlerBus? handlerBus = null,
        IUserRepository? userRepository = null)
    {
        handlerBus ??= new Mock<IHandlerBus>().Object;
        userRepository ??= new Mock<IUserRepository>().Object;

        return new UserCreateHandler(handlerBus, userRepository);
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

        sut.IsValid.Should().BeFalse();

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

        sut.IsValid.Should().BeFalse();

        resultData.IsValid.Should().BeFalse();
        resultData.FieldErrors.Single().Key.Should().Be(nameof(command.Email));
    }
}
