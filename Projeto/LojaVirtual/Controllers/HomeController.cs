using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LojaVirtual.DataBase;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private LojaVirtualContext _banco;
        public HomeController(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index([FromForm]NewsletterEmail pNewsletter)
        {

            if (ModelState.IsValid)
            {
                _banco.NewsletterEmails.Add(pNewsletter);
                _banco.SaveChanges();

                TempData["MSG_S"] = "E-mail cadastrado! Fique atento as novidades no seu e-mail!";

                return RedirectToAction(nameof(Index));
            }
            else
                return View();            

        }
        public IActionResult Contato()
        {
            return View();
        }
        public IActionResult ContatoAcao()
        {
            try
            {
                Contato pContato = new Contato
                {
                    Nome = HttpContext.Request.Form["nome"],
                    Email = HttpContext.Request.Form["email"],
                    Texto = HttpContext.Request.Form["texto"]
                };

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(pContato);
                bool isValid = Validator.TryValidateObject(pContato, contexto, listaMensagens, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(pContato);

                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br />");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = pContato;
                }

            }
            catch (Exception e)
            {
                ViewData["MSG_E"] = "Ops! Tivemos um erro, tente novamente mais tarde!";

                //TODO - Implementar Log
            }




            return View("Contato");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastroCliente([FromForm]Cliente pCliente)
        {
            if (ModelState.IsValid)
            {
                _banco.Clientes.Add(pCliente);
                _banco.SaveChanges();

                TempData["MSG_S"] = "Cadastro Realizado com Sucesso!";

                //TODO - Implementar redirecionamento diferenciados (Painel, Carrinho de compras e etc).
                return RedirectToAction(nameof(CadastroCliente));
            }

            return View();
        }
        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}