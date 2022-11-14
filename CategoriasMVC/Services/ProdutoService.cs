using CategoriasMVC.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CategoriasMVC.Services;

public class ProdutoService : IProdutoService
{
    private const string apiEndPoint = "api/1/produtos/";
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _clientFactory;

    private CategoriaVM categoriaVM;
    private IEnumerable<CategoriaVM> categoriasVM;

    public ProdutoService(IHttpClientFactory clientFactory)
    {
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _clientFactory = clientFactory;
    }

    public Task<IEnumerable<ProdutoVM>> GetProduto(string token)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");

        return null;

    }

    public Task<ProdutoVM> GetProdutoId(int id, string token)
    {
        throw new NotImplementedException();
    }

    public Task<ProdutoVM> CreateProduto(ProdutoVM produto, string token)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduto(int id, ProdutoVM produto, string token)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduto(int id, string token)
    {
        throw new NotImplementedException();
    }

    private static void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
