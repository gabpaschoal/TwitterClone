using TwitterClone.Application.Commands.User;
using TwitterClone.Application.Responses.User;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Domain.Services;
using TwitterClone.Resources;

namespace TwitterClone.Application.Handlers.User;

public class UserSignInHandler : HandlerBase<UserSignInCommand, CustomResultData<UserSignInResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly ITokenService _tokenService;

    public UserSignInHandler(
        IHandlerBus handlerBus,
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        ITokenService tokenService) : base(handlerBus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _tokenService = tokenService;
    }

    public override Task<CustomResultData<UserSignInResponse>> HandleExecution(UserSignInCommand request, CancellationToken cancellationToken)
    {
        var encryptedPassword = _encryptionService.Encrypt(request.Password);

        Domain.Entities.User user = _userRepository.GetUserByEmailAndPassword(email: request.Email, password: encryptedPassword);

        if (user is null)
            AddError(nameof(request.Password), ValidationMessage.UserNotFound);

        if (IsInvalid)
            return InvalidResponse();

        var tokenResponse = _tokenService.TokenGenerator(user);

        UserSignInResponse loginResponse = new()
        {
            Email = user.Email.Trim(),
            Name = user.Name.Trim(),
            NickName = user.NickName.Trim(),
            CreatedAt = DateTime.Now,
            Token = tokenResponse.Token,
            ExpireAt = tokenResponse.ExpirationDate
        };

        CustomResultData<UserSignInResponse> response = new(loginResponse);

        return ValidResponse(response);
    }

}
