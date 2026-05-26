using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatalogoJogosAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            
            if (model.Usuario == "admin" && model.Senha == "admin123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                
                var key = Encoding.ASCII.GetBytes("MinhaChaveSuperSecretaParaOProjetoJWT2026!!");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.Usuario)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2), 
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                
                return Ok(new { token = tokenHandler.WriteToken(token) });
            }

            return Unauthorized(new { message = "Usuário ou senha incorretos." });
        }
    }

    public class LoginModel
    {
        public string Usuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}