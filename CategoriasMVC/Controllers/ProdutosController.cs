using CategoriasMVC.Models;
using CategoriasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CategoriasMVC.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private string token = string.Empty;

        public ProdutosController (IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoVM>>> Index()
        {
            var result = await _produtoService.GetProdutos(GetToken());
            if (result is null)
                return View("Error");
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            ViewBag.CategoriaID = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoVM>> Create(ProdutoVM produto)
        {
            var result = await _produtoService.CreateProduto(produto, this.GetToken());

            if (result != null)
                return RedirectToAction(nameof(Index));
            else
            {
                ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _produtoService.GetProdutoId(id, this.GetToken());
            ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            if (result is null)
                return View("Error");
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoVM>> Edit(int id, ProdutoVM produto)
        {
            var result = await _produtoService.UpdateProduto(id, produto, this.GetToken());

            if (!result)
                return View("Error");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var result = await _produtoService.GetProdutoId(id, this.GetToken());

            if (result is null)
                return View("Error");
            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _produtoService.GetProdutoId(id, this.GetToken());

            if (result is null)
                return View("Error");
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await _produtoService.DeleteProduto(id, this.GetToken());

            if (!result)
                return View("Error");
            return RedirectToAction("Index");
        }

        private string GetToken()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();
            return token;
        }
    }
}
