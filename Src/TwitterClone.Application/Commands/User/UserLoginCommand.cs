using TwitterClone.Application.Responses.User;

namespace TwitterClone.Application.Commands.User;

public record UserLoginCommand(string Email, string Password) : IRequestExecution<UserLoginResponse>;
