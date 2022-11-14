using CategoriasMVC.Models;
namespace CategoriasMVC.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoVM>> GetProduto(string token);
    Task<ProdutoVM> GetProdutoId(int id, string token);
    Task<ProdutoVM> CreateProduto(ProdutoVM produto, string token);
    Task<bool> UpdateProduto(int id, ProdutoVM produto, string token);
    Task<bool> DeleteProduto(int id, string token);
}
