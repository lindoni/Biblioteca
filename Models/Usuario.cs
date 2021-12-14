using System;

namespace Biblioteca.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }

        public string Senha { get; set; }
        public int Tipo { get; set; }
        public static int admin = 0;
        public static int padrao = 1;

    }
}