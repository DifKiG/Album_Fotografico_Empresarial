using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



/* <Sumario>
 * Autor: Diego Fernando Quijano Giron 
 * Fecha de creacion: 30/09/23
 * Descripción: Creación de Crud y conexión con MySQL
 */


namespace Album_Fotografico_Empresarial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dGVFotograf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            string connection = "server=localhost; user id=root; password= santiago; database=fotosEmpres";

            string query = "INSERT INTO fotos(ID,Nombre_del_evento,Descripción,Imagen)VALUES" +
                "('" + this.textId.Text + "','" + this.textEvento.Text + "','" + this.textDescrip.Text + "','" + this.Imagen.Text + "')";
            
            MySqlConnection conn = new MySqlConnection(connection);

            MySqlCommand cmd = new MySqlCommand(query, conn);

            MySqlDataReader dr;

            conn.Open();

            dr = cmd.ExecuteReader();

            MessageBox.Show("Guardado con exito");

            conn.Close();
        }
    }   
}
