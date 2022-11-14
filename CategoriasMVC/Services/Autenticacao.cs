using CategoriasMVC.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services
{
    public class Autenticacao : IAutenticacao
    {
        private readonly IHttpClientFactory _clientFactory;
        const string apiEndPointAutentica = "/api/autoriza/login/";
        private readonly JsonSerializerOptions _options;
        private TokenVM userToken;

        public Autenticacao(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }   

        public async Task<TokenVM> AutenticacaoUsuario(UsuarioVM usuarioVM)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");
            var usuario = JsonSerializer.Serialize(usuarioVM);
            StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndPointAutentica, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    userToken = await JsonSerializer
                        .DeserializeAsync<TokenVM>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return userToken;
        }
    }
}
