using TwitterClone.Application.Responses.User;

namespace TwitterClone.Application.Commands.User;

public record UserSignInCommand(string Email, string Password) : IRequestExecution<UserSignInResponse>;
