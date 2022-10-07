using CategoriasMVC.Models;
using CategoriasMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMVC.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<ActionResult<IEnumerable<CategoriaVM>>> Index()
        {
            var result = await _categoriaService.GetCategorias();
            if (result is null)
                return View("Error");
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaVM>> Create(CategoriaVM categoria)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoriaService.CreateCategoria(categoria);

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Erro ao criar Categoria";
            return View(categoria);
        }

        [HttpGet]
        public async Task<ActionResult<CategoriaVM>> Edit(int id)
        {
            var result = await _categoriaService.GetCategoriaId(id);

            if (result is null)
                return View("Erro");
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaVM>> Edit(int id, CategoriaVM categoria)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoriaService.UpdateCategoria(id, categoria);

                if (result)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Erro ao atualizar Categoria";
            return View(categoria);
        }

        [HttpGet]
        public async Task<ActionResult<CategoriaVM>> Delete(int id)
        {
            var result = await _categoriaService.GetCategoriaId(id);

            if (result is null)
                return View("Erro");  
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoriaService.DeleteCategoria(id);

                if (result)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Erro ao remover Categoria";
            return View(nameof(Index));
        }
    }
}
