using CategoriasMVC.Models;

namespace CategoriasMVC.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaVM>> GetCategorias();
        Task<CategoriaVM> GetCategoriaId(int id);
        Task<CategoriaVM> CreateCategoria(CategoriaVM categoria);
        Task<bool> UpdateCategoria(int id, CategoriaVM categoria);
        Task<bool> DeleteCategoria(int id);
    }
}
