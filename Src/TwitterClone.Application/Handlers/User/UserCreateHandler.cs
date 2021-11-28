using TwitterClone.Application.Commands.User;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Domain.Services;
using TwitterClone.Resources;

namespace TwitterClone.Application.Handlers.User;

public class UserCreateHandler : HandlerBase<UserCreateCommand, CustomResultData<Guid>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;

    public UserCreateHandler(
        IHandlerBus handlerBus,
        IUserRepository userRepository,
        IEncryptionService encryptionService) : base(handlerBus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
    }

    public override async Task<CustomResultData<Guid>> HandleExecution(UserCreateCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.ExistsUserWithThisNickName(nickName: request.NickName))
            AddError(nameof(request.NickName), ValidationMessage.AlreadyExistsAnUserWithThisNickName);

        if (_userRepository.ExistsUserWithThisEmail(email: request.Email))
            AddError(nameof(request.Email), ValidationMessage.AlreadyExistsAnUserWithThisEmail);

        if (IsInvalid)
            return InvalidResponseAsync();

        var encryptedPassword = _encryptionService.Encrypt(request.Password);
        Domain.Entities.User user = new(request.Name, request.NickName, request.Email, encryptedPassword);

        await _userRepository.AddAsync(user);

        CustomResultData<Guid> validResult = new(user.Id);

        return ValidResponseAsync(validResult);
    }
}
