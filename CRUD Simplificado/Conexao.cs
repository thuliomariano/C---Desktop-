using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CRUD_Simplificado
{
    public class Conexao
    {
        SqlConnection connection;

        public Conexao()
        {
            connection = new SqlConnection();
            connection.ConnectionString = @"Data Source=WIDDIAN;Initial Catalog=Pessoa;Integrated Security=True";


        }

        public SqlConnection Conectar()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            return connection;
        }

        public void Desconectar()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }


        }




    }
}
