using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwitterClone.Domain.Constants;
using TwitterClone.Domain.Entities;
using TwitterClone.Domain.Services;
using TwitterClone.Domain.Services.Models;
using TwitterClone.Domain.Services.Responses;

namespace TwitterClone.Application.Services;

public class TokenService : ITokenService
{
    private readonly JWTEncriptionModel _jwtEncriptionModel;

    public TokenService(IOptions<JWTEncriptionModel> jwtEncriptionModel)
    {
        _jwtEncriptionModel = jwtEncriptionModel.Value;
    }

    public TokenResponse TokenGenerator(User user)
    {
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

        var securyToken = tokenHandler.CreateToken(tokenDescriptor);

        var response = new TokenResponse
        {
            Token = tokenHandler.WriteToken(securyToken),
            ExpirationDate = expirationDate
        };

        return response;
    }
}
