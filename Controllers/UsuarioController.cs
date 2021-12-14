using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario novoUsuario)
        {
            UsuarioService user = new UsuarioService();
            int novoId = user.CreateUsuario(novoUsuario);
            if (novoId != 0)
            {
                ViewData["Mensagem"] = "Cadastro realizado com sucesso";
            }
            else
            {
                ViewData["Mensagem"] = "Falha no cadastro";
            }

            return View();
        }
        public IActionResult Lista()
        {
            UsuarioService service = new UsuarioService();
            return View(service.ListarUsers());
        }
        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            UsuarioService u = new UsuarioService();
            Usuario user = u.ObterPorId(id);
            return View(u);
        }
    }
    
}
