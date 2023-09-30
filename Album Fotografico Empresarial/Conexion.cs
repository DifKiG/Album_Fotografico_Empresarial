using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Album_Fotografico_Empresarial
{
    class Conexion
    {
        public static MySqlConnection conexion()
        {
            string servidor = "127.0.0.1";
            string bd = "Registro empresarial fotos";
            string usuario = "root";
            string password = "santiago";
            string puerto = "3306";

            string cadenaConexion = "Database=" + bd + "; Data Source =" + servidor + "; User Id= " +
                usuario + "; password=" + password + "; port =" + puerto;

            try
            {
                MySqlConnection conexionBD = new MySqlConnection(cadenaConexion);
                return conexionBD;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }


            
            
        }
    }
}
