using CategoriasMVC.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CategoriasMVC.Services;

public class ProdutoService : IProdutoService
{
    private const string apiEndPoint = "api/1/produtos/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private ProdutoVM produtoVM;
    private IEnumerable<ProdutoVM> produtosVM;

    public ProdutoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProdutoVM>> GetProdutos(string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(apiEndPoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtosVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoVM>>(apiResponse, _options);
            } else
                return null;
        }
        return produtosVM;
    }

    public async Task<ProdutoVM> GetProdutoId(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.GetAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtoVM = await JsonSerializer
                            .DeserializeAsync<ProdutoVM>
                            (apiResponse, _options);
            }
            else
                return null;
        }
        return produtoVM;
    }

    public async Task<ProdutoVM> CreateProduto(ProdutoVM produto, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        var prod = JsonSerializer.Serialize(produto);
        StringContent content = new StringContent(prod, Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndPoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produto = await JsonSerializer
                            .DeserializeAsync<ProdutoVM>
                            (apiResponse, _options);
            } else
                return null;
        }
        return produto;
    }

    public async Task<bool> UpdateProduto(int id, ProdutoVM produto, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.PutAsJsonAsync(apiEndPoint + id, produto))
        {
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }

    public async Task<bool> DeleteProduto(int id, string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        PutTokenInHeaderAuthorization(token, client);

        using (var response = await client.DeleteAsync(apiEndPoint + id))
        {
            if (response.IsSuccessStatusCode)
                return true;
        }
        return false;
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
