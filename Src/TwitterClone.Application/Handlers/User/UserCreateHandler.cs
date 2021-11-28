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

    public override Task<CustomResultData<Guid>> HandleExecution(UserCreateCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.ExistsNickName(nickName: request.NickName))
            AddError(nameof(request.NickName), ValidationMessage.AlreadyExistsAnUserWithThisNickName);

        if (IsInvalid)
            return InvalidResponse();

        Domain.Entities.User user = new(request.Name, request.NickName, request.Email, request.Password);

        CustomResultData<Guid> validResult = new(user.Id);

        return ValidResponse(validResult);
    }
}
