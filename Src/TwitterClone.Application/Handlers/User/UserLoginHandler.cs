using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwitterClone.Application.Commands.User;
using TwitterClone.Application.Responses.User;
using TwitterClone.Domain.Constants;
using TwitterClone.Domain.Repositories.Data;
using TwitterClone.Domain.Services;
using TwitterClone.Domain.Services.Models;
using TwitterClone.Resources;

namespace TwitterClone.Application.Handlers.User;

public class UserLoginHandler : HandlerBase<UserLoginCommand, CustomResultData<UserLoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly JWTEncriptionModel _jwtEncriptionModel;
    public UserLoginHandler(
        IHandlerBus handlerBus,
        IUserRepository userRepository,
        IEncryptionService encryptionService,
        JWTEncriptionModel jwtEncriptionModel) : base(handlerBus)
    {
        _userRepository = userRepository;
        _encryptionService = encryptionService;
        _jwtEncriptionModel = jwtEncriptionModel;
    }

    public override Task<CustomResultData<UserLoginResponse>> HandleExecution(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var encryptedPassword = _encryptionService.Encrypt(request.Password);

        Domain.Entities.User user = _userRepository.GetUserByEmailAndPassword(email: request.Email, password: encryptedPassword);

        if (user is null)
            AddError(nameof(request.Password), ValidationMessage.UserNotFound);

        if (IsInvalid)
            return InvalidResponse();

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtEncriptionModel.Key);
        var expirationDate = DateTime.UtcNow.AddHours(_jwtEncriptionModel.HoursToExpire);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimsConstants.UserId, user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expirationDate,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        UserLoginResponse loginResponse = new()
        {
            Email = user.Email.Trim(),
            Name = user.Name.Trim(),
            NickName = user.NickName.Trim(),
            CreatedAt = DateTime.Now,
            Token = tokenHandler.WriteToken(token),
            ExpireAt = expirationDate
        };

        CustomResultData<UserLoginResponse> response = new(loginResponse);

        return ValidResponse(response);
    }
}
