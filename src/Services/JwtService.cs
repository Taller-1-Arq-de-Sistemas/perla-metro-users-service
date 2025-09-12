using PerlaMetroUsersService.Services.Interfaces;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;

namespace PerlaMetroUsersService.Services;

public sealed class JwtService : IJwtService
{
    private readonly string _secret;
    public JwtService(IConfiguration config)
    {
        _secret = config.GetValue<string>("JWT_SECRET")
            ?? throw new InvalidOperationException("JWT secret not configured.");
    }
    public string GenerateToken(string email, string role)
    {
        var claims = new List<Claim>{
                new (ClaimTypes.Email, email),
                new (ClaimTypes.Role, role),
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var jwtHandler = new JsonWebTokenHandler();
        var token = jwtHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds
        });

        return token;
    }

    public async Task<(string email, string role)> ValidateToken(string token)
    {
        var jwtHandler = new JsonWebTokenHandler();
        var claims = await jwtHandler.ValidateTokenAsync(token, new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret))
        });

        var email = claims.ClaimsIdentity?.FindFirst(ClaimTypes.Email)?.Value;
        var role = claims.ClaimsIdentity?.FindFirst(ClaimTypes.Role)?.Value;

        return (
            email ?? throw new InvalidOperationException("Invalid token: email claim missing"),
            role ?? throw new InvalidOperationException("Invalid token: role claim missing")
        );
    }
}
