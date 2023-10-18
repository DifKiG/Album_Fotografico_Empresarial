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
using System.IO;
using System.Drawing.Imaging;



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

            dGVFotograf.RowTemplate.Height = 60;

            dGVFotograf.AllowUserToAddRows = false;

            dGVFotograf.DataSource = table;

            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();

            imgCol = (DataGridViewImageColumn)dGVFotograf.Columns[3];

            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dGVFotograf.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

       /*private void dGVFotograf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }*/

        

        private void btnSeleccionarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Escoger Imagen(*.JPG; *.PNG; *.GIF)|*.jpg; *png; *gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(opf.FileName);
            }
        }

        private void dGVFotograf_Click(object sender, EventArgs e)
        {
            

            Byte[] img = (Byte[])dGVFotograf.CurrentRow.Cells[3].Value;

            MemoryStream ms = new MemoryStream(img);

            //img.Save(ms, ImageFormat.Jpeg);

            pictureBox1.Image = Image.FromStream(ms);

            textId.Text = dGVFotograf.CurrentRow.Cells[0].Value.ToString();
            textEvento.Text = dGVFotograf.CurrentRow.Cells[1].Value.ToString();
            textDescrip.Text = dGVFotograf.CurrentRow.Cells[2].Value.ToString();
        }
    }   
}
