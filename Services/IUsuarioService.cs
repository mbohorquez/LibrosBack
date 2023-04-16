using ReservasCore6.Models.Request;
using ReservasCore6.Models.Response;

namespace ReservasCore6.Services
{
    public interface IUsuarioService
    {
        UsuarioResponse Auth(AuthRequest model);
    }
}
