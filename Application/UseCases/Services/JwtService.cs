using ChemicalLaboratory.Application.UseCases.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface IJwtService
{
    string GenerateToken(UserReadDTO user);
}

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(UserReadDTO user)
    {
        // 1. payload
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("name", user.Login),
            new Claim("role", user.SystemRole ?? "Пользователь") // Если есть роли
        };

        // 2. Берем секретный ключ из appsetting.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 3. Формируем токен
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1), 
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
