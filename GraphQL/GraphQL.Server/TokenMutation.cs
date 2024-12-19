using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotChocolate.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace GraphQL.Server;

[MutationType]
public sealed class TokenMutation
{
    public string CreateAccessToken()
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Enumerable.Range(0,32).Select(i => (char)i).ToArray()));
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim(ClaimTypes.Role, "Admin")
            ]),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
            Audience = "api://graphql-api",
            Issuer = "https://localhost:5001",
            NotBefore = DateTime.UtcNow,
            IssuedAt = DateTime.UtcNow,
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}