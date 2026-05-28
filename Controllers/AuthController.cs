using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;

namespace CatalogoJogosAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private static readonly Dictionary<string, string> _usuarios = new Dictionary<string, string>
        {
            { "admin", "admin123" } 
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            
            if (_usuarios.TryGetValue(model.Usuario, out var senhaCorreta) && senhaCorreta == model.Senha)
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

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Usuario) || string.IsNullOrWhiteSpace(model.Senha))
            {
                return BadRequest(new { message = "Usuário e senha são obrigatórios." });
            }

            if (_usuarios.ContainsKey(model.Usuario))
            {
                return BadRequest(new { message = "Este usuário já está cadastrado!" });
            }

            
            _usuarios.Add(model.Usuario, model.Senha);
            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        }
    }

    public class LoginModel
    {
        public string Usuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}