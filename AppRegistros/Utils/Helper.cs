using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using AnvizDemo;
using static AnvizDemo.AnvizNew;
using System.Data.SqlClient;

namespace AppRegistros.Utils
{
    public class Helper
    {
        public static SQLiteConnection CrearConexion()
        {
            if (!File.Exists("./reg.db"))
            {
                SQLiteConnection.CreateFile("reg.db");
            }
            string connectionString = "Data Source=reg.db;";
            SQLiteConnection conexion = new SQLiteConnection(connectionString);
            conexion.Open();
            return conexion;
        }
    }
}
