using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public int CreateUsuario(Usuario novoUsuario)
        {
            using (var context = new BibliotecaContext())
            {
                context.Add(novoUsuario);
                context.SaveChanges();
                return novoUsuario.Id;
            }
        }
        public void Atualizar(Usuario novoUsuario)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario novo = bc.Usuarios.Find(novoUsuario.Id);

                novo.Login = novoUsuario.Login;
                novo.Nome = novoUsuario.Nome;
                novo.Senha = novoUsuario.Senha;
                novo.Tipo = novoUsuario.Tipo;

                bc.SaveChanges();
            }
        }
        public ICollection<Usuario> ListarUsers()
        {
            using (var context = new BibliotecaContext())
            {
                ICollection<Usuario> resultado =
                    context.Usuarios.ToList();

                return resultado;
            }
        }
        public Usuario ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }
        public void editarUsuario(Usuario EditU)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario antigoU = bc.Usuarios.Find(EditU.Id);

                antigoU.Login = EditU.Login;
                antigoU.Nome = EditU.Nome;
                antigoU.Senha = EditU.Senha;
                antigoU.Tipo = EditU.Tipo;

                bc.SaveChanges();
            }
        }
        public void excluirUsuario(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                bc.Usuarios.Remove(bc.Usuarios.Find(id));
                bc.SaveChanges();
            }
        }
    }
}