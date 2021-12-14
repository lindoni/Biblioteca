using System.Collections.Generic;
using System.Linq;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Biblioteca.Controllers
{
    public class Autenticacao
    {
    public static void CheckLogin(Controller controller)
        {
            if (string.IsNullOrEmpty(controller.HttpContext.Session.GetString("Login")))
            {
                controller.Request.HttpContext.Response.Redirect("/Home/Login");
            }
        }
    
    public static bool verificaLoginSenha(string Login, string Senha, Controller controller) //verfifica/valida Login e Senha
    {
        using (BibliotecaContext bc = new BibliotecaContext())
        {
            verificaSeUsuarioAdminExiste(bc);
            Senha = Criptografo.TextoCriptografado(Senha);

            IQueryable<Usuario> UsuarioEncontrado = bc.Usuarios.Where(u => u.Login == Login && u.Senha == Senha); // consulta na tabela Usuario onde o login/senha digitado tem que ser igual ao login/senha da tabela
            List<Usuario> ListaUsuarioEncontrado = UsuarioEncontrado.ToList(); // cria uma lista com os usuario encontrados
            
            if (ListaUsuarioEncontrado.Count == 0)
            {
                return false;
            }

            else
            {
                controller.HttpContext.Session.SetString("Login", ListaUsuarioEncontrado[0].Login);
                controller.HttpContext.Session.SetString("Nome", ListaUsuarioEncontrado[0].Nome);
                controller.HttpContext.Session.SetInt32("Tipo", ListaUsuarioEncontrado[0].Tipo);
                return true;
            }
        }
    }

    public static void verificaSeUsuarioAdminExiste(BibliotecaContext bc)

    {
        IQueryable<Usuario> userEncontrado = bc.Usuarios.Where(u => u.Login == "admin");
        if (userEncontrado.ToList().Count == 0) // caso nenhum usuario seja encontrado na lista da tabela Usuarios
        {
            Usuario admin = new Usuario(); // um novo usuário será criado com esses dados abaixo
            admin.Login = "admin";
            admin.Senha = Criptografo.TextoCriptografado("123");
            admin.Tipo = Usuario.admin;
            admin.Nome = "Administrador";

            bc.Usuarios.Add(admin);
            bc.SaveChanges();
        }
    }

    public static void verificaSeUsuarioEAdmin(Controller controller)
    {

        if (!(controller.HttpContext.Session.GetInt32("Tipo") == Usuario.admin))
        {
            controller.Request.HttpContext.Response.Redirect("/Home/Login");
        }
    }
}
}