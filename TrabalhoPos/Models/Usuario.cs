using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoPos.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public decimal Receita { get; set; }
    }
}