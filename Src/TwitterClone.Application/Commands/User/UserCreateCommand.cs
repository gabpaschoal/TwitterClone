namespace TwitterClone.Application.Commands.User;

public record UserCreateCommand(
        string Name,
        string NickName,
        string Email,
        string Password,
        string PasswordConfirmation)
    : IRequestExecution<Guid>;