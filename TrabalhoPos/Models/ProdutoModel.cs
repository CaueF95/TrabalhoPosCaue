using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrabalhoPos.Models
{

    public class ProdutoModel : Model
    {
        public void Create(Produto p)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Produto VALUES (@UsuarioId, @Nome, @Preco, @Vendido)";

            cmd.Parameters.AddWithValue("@UsuarioId", p.UsuarioID);
            cmd.Parameters.AddWithValue("@Nome", p.Nome);
            cmd.Parameters.AddWithValue("@Preco", p.Preco);
            cmd.Parameters.AddWithValue("@Vendido", p.Vendido);

            cmd.ExecuteNonQuery();
        }

        public List<Produto> Read()
        {
            List<Produto> lista = new List<Produto>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * 
                                FROM Produto
                                WHERE Vendido = 0";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Produto p = new Produto
                {
                    ProdutoID = reader.GetInt32(0),
                    UsuarioID = reader.GetInt32(1),
                    Nome = reader.GetString(2),
                    Preco = reader.GetDecimal(3)
                };

                lista.Add(p);
            }

            return lista;
        }

        public Produto Read(int id)
        {
            Produto p = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * 
                                FROM Produto
                                WHERE ProdutoId = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                p = new Produto
                {
                    ProdutoID = reader.GetInt32(0),
                    UsuarioID = reader.GetInt32(1),
                    Nome = reader.GetString(2),
                    Preco = reader.GetDecimal(3)
                };
            }

            return p;
        }

        public void Comprar(int id, decimal valorReceita, int usuarioId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE Produto SET Vendido = 1 WHERE ProdutoId = @id";
            
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandText = "UPDATE Usuario SET Receita = @receita WHERE UsuarioId = @usuarioId";

            cmd2.Parameters.AddWithValue("@receita", valorReceita);
            cmd2.Parameters.AddWithValue("@usuarioId", usuarioId);
            cmd2.ExecuteNonQuery();
        }
    }
}