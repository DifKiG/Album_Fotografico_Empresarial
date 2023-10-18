using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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

        MySqlConnection conn = new MySqlConnection("datasource = localhost; port = 3306; Initial Catalog = 'fotosempres'; username = root; password= ");

        private void Form1_Load(object sender, EventArgs e)
        {
            FillDGV();
        }

        public void FillDGV()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `fotos`", conn);

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataTable table = new DataTable();

            adapter.Fill(table);

            dGVFotograf.DataSource = table;
        }

        private void dGVFotograf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //string connection = "server=localhost; user id=root; password= santiago; database=fotosEmpres";

            //string query = "INSERT INTO fotos(ID,Nombre_del_evento,Descripción,Imagen)VALUES" +
            //  "('" + this.textId.Text + "','" + this.textEvento.Text + "','" + this.textDescrip.Text + "','" + this.Imagen.Text + "')";
            
            //
            


            //MySqlDataReader dr;

            //conn.Open();

            //dr = cmd.ExecuteReader();

            //MessageBox.Show("Guardado con exito");

            //conn.Close();
        }

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Escoger Imagen(*.JPG; *.PNG; *.GIF)|*.jpg; *png; *gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        
    }   
}
