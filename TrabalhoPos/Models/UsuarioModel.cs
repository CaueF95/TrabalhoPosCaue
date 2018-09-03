using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TrabalhoPos.Models
{
    public class Model : IDisposable
    {
        protected SqlConnection conn;

        public Model()
        {
            string strConn = @"Data Source=localhost;
                                Initial Catalog=BDVendaDireta;
                                Integrated Security=true";
            // User Id = sa; Password=dba;

            conn = new SqlConnection(strConn);
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }
    }

    public class UsuarioModel : Model
    {
        public void Create(Usuario u)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Usuario VALUES (@nome, @email, @senha, @receita)";

            cmd.Parameters.AddWithValue("@nome", u.Nome);
            cmd.Parameters.AddWithValue("@email", u.Email);
            cmd.Parameters.AddWithValue("@senha", u.Senha);
            cmd.Parameters.AddWithValue("@receita", u.Receita);

            cmd.ExecuteNonQuery();
        }

        public Usuario Login(string email, string senha)
        {
            Usuario u = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * 
                                FROM Usuario
                                WHERE Email = @email
                                AND Senha = @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                u = new Usuario
                {
                    UsuarioID = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Email = reader.GetString(2),
                    Senha = reader.GetString(3),
                    Receita = reader.GetDecimal(4),
                };
            }

            return u;
        }
    }


}