using CategoriasMVC.Models;

namespace CategoriasMVC.Services
{
    public interface IAutenticacao
    {
        Task<TokenVM> AutenticacaoUsuario(UsuarioVM usuarioVM);
    }
}
