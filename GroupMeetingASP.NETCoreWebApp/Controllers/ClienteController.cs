using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class ClienteController : Controller
    {
        
        public IActionResult Index()
        {
            return View(ClienteModel.ListarClientes());
        }

        [HttpGet]
        public IActionResult AdicionarCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdicionarCliente([Bind] ClienteModel clienteModel)
        {
            if (ModelState.IsValid)
            {
                if (ClienteModel.AdicionarCliente(clienteModel) > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(clienteModel);
        }

        [HttpGet]
        public IActionResult EditarCliente(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ClienteModel group = ClienteModel.ListarClientePorId(id);
            if (group == null)
                return NotFound();
            return View(group);
        }

        [HttpPost]
        public IActionResult EditarCliente(int id, [Bind] ClienteModel clienteModel)
        {
            if (id != clienteModel.IdCliente)
                return NotFound();

            if (ModelState.IsValid)
            {
                ClienteModel.EditaCliente(clienteModel);
                return RedirectToAction("Index");
            }
            return View(clienteModel);
        }
        [HttpGet]
        public IActionResult DeletarCliente(int id)
        {
            ClienteModel group = ClienteModel.ListarClientePorId(id);
            if (group==null)
            {
                return NotFound();
            }

            return View(group);
        }
        [HttpPost]
        public IActionResult DeletarCliente(int id,ClienteModel clienteModel)
        {
            if (ClienteModel.DeletarCliente(id) > 0)
            {
                return RedirectToAction("Index");
            }
            return View(clienteModel);
        }
    }
}