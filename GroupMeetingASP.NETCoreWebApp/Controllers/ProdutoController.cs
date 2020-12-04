using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View(ProdutoModel.ListarProduto());
        }

        [HttpGet]
        public IActionResult AdicionarProduto()
        {
            return View();
        }
    

        [HttpPost]
        public IActionResult AdicionarProduto([Bind] ProdutoModel produtoModel)
        {
            if (ModelState.IsValid)
            {
                if (ProdutoModel.AdicionarProduto(produtoModel) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(produtoModel);
        }

        [HttpGet]
        public IActionResult EditarProduto(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProdutoModel group = ProdutoModel.ListarProdutoPorId(id);
            if (group == null)
                return NotFound();
            return View(group);
        }

        [HttpPost]
        public IActionResult EditarProduto(int id, [Bind] ProdutoModel produtoModel)
        {
            if (id != produtoModel.IdProduto)
                return NotFound();

            if (ModelState.IsValid)
            {
                ProdutoModel.EditaProduto(produtoModel);
                return RedirectToAction("Index");
            }
            return View(produtoModel);
        }
        [HttpGet]
        public IActionResult DeletarProduto(int id)
        {
            ProdutoModel group = ProdutoModel.ListarProdutoPorId(id);
            if (group==null)
            {
                return NotFound();
            }

            return View(group);
        }
        [HttpPost]
        public IActionResult DeletarProduto(int id, ProdutoModel produtoModel)
        {
            if (ProdutoModel.DeletarProduto(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(produtoModel);
        }
    }
}