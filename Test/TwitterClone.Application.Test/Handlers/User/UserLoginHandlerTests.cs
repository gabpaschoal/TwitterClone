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

public class UserLoginHandlerTests
{
    private static UserLoginHandler MakeSut(
       IHandlerBus? handlerBus = null,
       IUserRepository? userRepository = null,
       IEncryptionService? encryptionService = null,
       ITokenService? tokenService = null)
    {
        handlerBus ??= new Mock<IHandlerBus>().Object;
        userRepository ??= new Mock<IUserRepository>().Object;
        encryptionService ??= new Mock<IEncryptionService>().Object;
        tokenService ??= new Mock<ITokenService>().Object;

        return new UserLoginHandler(handlerBus, userRepository, encryptionService, tokenService);
    }

    private static Domain.Entities.User MakeUser()
    {
        Domain.Entities.User user = new(name: "Gustav", nickName: "G986", email: "gustav@mail.com", password: "123");
        return user;
    }

    private static UserLoginCommand MakeValidCommand()
        => new(Email: "mail@valid.com", Password: "valid_password");

    [Fact(DisplayName = "Should be invalid when dont find the user")]
    public void Should_be_invalid_when_dont_find_the_user()
    {
        var command = MakeValidCommand();
        Mock<IUserRepository> userRepositoryMock = new();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Domain.Entities.User user = null;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        userRepositoryMock.Setup(x => x.GetUserByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>()))
#pragma warning disable CS8604 // Possible null reference argument.
                .Returns(user);
#pragma warning restore CS8604 // Possible null reference argument.

        var sut = MakeSut(userRepository: userRepositoryMock.Object);

        var resultData = sut.HandleExecution(command, cancellationToken: System.Threading.CancellationToken.None).Result;

        resultData.IsValid.Should().BeFalse();
        resultData.FieldErrors.Single().Key.Should().Be(nameof(command.Password));
    }
}
