using FluentAssertions;
using Moq;
using System;
using System.Linq;
using TwitterClone.Application.Commands.User;
using TwitterClone.Application.Handlers;
using TwitterClone.Application.Handlers.User;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Domain.Services;
using TwitterClone.Domain.Services.Responses;
using Xunit;

namespace TwitterClone.Application.Test.Handlers.User;

public class UserSignInHandlerTests
{
    private static UserSignInHandler MakeSut(
       IHandlerBus? handlerBus = null,
       IUserRepository? userRepository = null,
       IEncryptionService? encryptionService = null,
       ITokenService? tokenService = null)
    {
        handlerBus ??= new Mock<IHandlerBus>().Object;
        userRepository ??= new Mock<IUserRepository>().Object;
        encryptionService ??= new Mock<IEncryptionService>().Object;
        tokenService ??= new Mock<ITokenService>().Object;

        return new UserSignInHandler(handlerBus, userRepository, encryptionService, tokenService);
    }

    private static Domain.Entities.User MakeUser()
    {
        Domain.Entities.User user = new(name: "Gustav", nickName: "G986", email: "gustav@mail.com", password: "123");
        return user;
    }

    private static UserSignInCommand MakeValidCommand() => new(Email: "mail@valid.com", Password: "valid_password");

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

    [Fact(DisplayName = "Should GetUserByEmailAndPassword be called with the email and the encrypted password and TokenGenerator with the user got")]
    public void Should_GetUserByEmailAndPassword_be_called_with_the_email_and_the_encrypted_password_and_TokenGenerator_with_the_user_got()
    {
        UserSignInCommand command = MakeValidCommand();
        string encryptedPassword = "encryptedPassword";
        Mock<IEncryptionService> encryptionServiceMock = new();
        encryptionServiceMock.Setup(x => x.Encrypt(It.IsAny<string>())).Returns(encryptedPassword);

        TokenResponse tokenResponse = new() { ExpirationDate = DateTime.Now, Token = "validToken" };
        Mock<ITokenService> tokenServiceMock = new();
        tokenServiceMock.Setup(x => x.TokenGenerator(It.IsAny<Domain.Entities.User>())).Returns(tokenResponse);

        Domain.Entities.User user = MakeUser();
        Mock<IUserRepository> userRepositoryMock = new();
        userRepositoryMock.Setup(x => x.GetUserByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(user);

        UserSignInHandler sut = MakeSut(userRepository: userRepositoryMock.Object,
                          encryptionService: encryptionServiceMock.Object,
                          tokenService: tokenServiceMock.Object);

        _ = sut.HandleExecution(command, cancellationToken: System.Threading.CancellationToken.None).Result;

        userRepositoryMock.Verify(x => x.GetUserByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        userRepositoryMock.Verify(x => x.GetUserByEmailAndPassword(
                                                It.Is<string>(x => x.Equals(command.Email)),
                                                It.Is<string>(x => x.Equals(encryptedPassword)))
        );

        tokenServiceMock.Verify(x => x.TokenGenerator(It.IsAny<Domain.Entities.User>()), Times.Once);
        tokenServiceMock.Verify(x => x.TokenGenerator(It.Is<Domain.Entities.User>(x => x.Equals(user))));
    }
}
