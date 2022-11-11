using CategoriasMVC.Models;
using System.Text.Json;

namespace CategoriasMVC.Services
{
    public class Autenticacao : IAutenticacao
    {
        private readonly IHttpClientFactory _clientFactory;
        const string apiEndPointAutentica = "/api/autoriza/login/";
        private readonly JsonSerializerOptions _options;
        private TokenVM userToken;

        public Task<TokenVM> AutenticacaoUsuario(UsuarioVM usuarioVM)
        {
        }
    }
}
