using TwitterClone.Application.Commands.User;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Resources;

namespace TwitterClone.Application.Handlers.User;

public class UserCreateHandler : HandlerBase<UserCreateCommand, CustomResultData<Guid>>
{
    private readonly IUserRepository _userRepository;

    public UserCreateHandler(
        IHandlerBus handlerBus,
        IUserRepository userRepository) : base(handlerBus)
    {
        _userRepository = userRepository;
    }

    public override async Task<CustomResultData<Guid>> HandleExecution(UserCreateCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.ExistsUserWithThisNickName(nickName: request.NickName))
            AddError(nameof(request.NickName), ValidationMessage.AlreadyExistsAnUserWithThisNickName);

        if (_userRepository.ExistsUserWithThisEmail(email: request.Email))
            AddError(nameof(request.Email), ValidationMessage.AlreadyExistsAnUserWithThisEmail);

        if (IsInvalid)
            return InvalidResponseAsync();

        Domain.Entities.User user = new(request.Name, request.NickName, request.Email, request.Password);

        await _userRepository.AddAsync(user); 

        CustomResultData<Guid> validResult = new(user.Id);

        return ValidResponseAsync(validResult);
    }
}
