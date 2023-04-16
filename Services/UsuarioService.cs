using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReservasCore6.Controllers;
using ReservasCore6.Data;
using ReservasCore6.Models;
using ReservasCore6.Models.Common;
using ReservasCore6.Models.Request;
using ReservasCore6.Models.Response;
using ReservasCore6.Tools;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace ReservasCore6.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AplicationDbContext _context;
        private readonly Jwt _jwt;

        public UsuarioService( AplicationDbContext contexto, IOptions<Jwt> jwt)
        {
            _context = contexto;
            _jwt = jwt.Value;
        }
        public UsuarioResponse Auth(AuthRequest model)
        {
            UsuarioResponse usuarioResponse = new UsuarioResponse();
            string password = Encrypt.GetSHA256(model.Password);
            var usuario =  _context.Usuario.Where(x => x.Email == model.Email && x.Password == password).FirstOrDefault();
            if (usuario == null) return null;
            usuarioResponse.Email = usuario.Email;
            usuarioResponse.Token = GetToken(usuario);

            return usuarioResponse; ;
                                               
        }

        private string GetToken(Usuario model)
        {
            var tokenHnadler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            tokenDescriptor.Subject.AddClaim(new Claim("id", model.IdUsuario.ToString()));
            tokenDescriptor.Subject.AddClaim(new Claim("nombre", model.Nombre.ToString()));

            var token = tokenHnadler.CreateToken(tokenDescriptor);
            return tokenHnadler.WriteToken(token);

        }
    }
}
