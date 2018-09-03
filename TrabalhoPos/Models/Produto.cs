using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrabalhoPos.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        public int UsuarioID { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Vendido { get; set; }

    }
}