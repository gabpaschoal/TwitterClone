using Moq;
using TwitterClone.Application.Commands.User;
using TwitterClone.Application.Handlers;
using TwitterClone.Application.Handlers.User;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Domain.Services;

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

    private static UserLoginCommand MakeValidCommand()
        => new(Email: "", Password: "");
}
