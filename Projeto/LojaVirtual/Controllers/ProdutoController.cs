using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            Produto pProduto = GetProduto();

            return View(pProduto);
        }

        private Produto GetProduto()
        {
            return new Produto()
            {
                Id = 1,
                Nome = "Playstation V",
                Descricao = "Game Play Avançada",
                Valor = 3000.00M
            };
        }
    }
}