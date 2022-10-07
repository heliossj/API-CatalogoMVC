using CategoriasMVC.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services
{
    public class CategoriaService : ICategoriaService
    {
        private const string apiEndPoint = "api/1/categorias/";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private CategoriaVM categoriaVM;
        private IEnumerable<CategoriaVM> categoriasVM;

        public CategoriaService(IHttpClientFactory clientFactory)
        {
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CategoriaVM>> GetCategorias()
        {
            var client = _clientFactory.CreateClient("CategoriasApi");

            using (var response = await client.GetAsync(apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    categoriasVM = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaVM>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoriasVM;
        }

        public async Task<CategoriaVM> GetCategoriaId(int id)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");

            using (var response = await client.GetAsync(apiEndPoint+id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaVM>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoriaVM;
        }

        public async Task<CategoriaVM> CreateCategoria(CategoriaVM categoriaVM)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");

            var categoria = JsonSerializer.Serialize(categoriaVM);
            StringContent content = new StringContent(categoria, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(apiEndPoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaVM>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return categoriaVM;
        }

        public async Task<bool> UpdateCategoria(int id, CategoriaVM categoriaVM)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");
            using (var response = await client.PutAsJsonAsync(apiEndPoint+id, categoriaVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteCategoria(int id)
        {
            var client = _clientFactory.CreateClient("CategoriasApi");
            using (var response = await client.DeleteAsync(apiEndPoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
