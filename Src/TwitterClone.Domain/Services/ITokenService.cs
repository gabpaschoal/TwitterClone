using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Services.Responses;

namespace TwitterClone.Domain.Services;

public interface ITokenService
{
    TokenResponse TokenGenerator(User user);
}
