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

            pictureBox1.Image = Image.FromStream(ms);

            textId.Text = dGVFotograf.CurrentRow.Cells[0].Value.ToString();
            textEvento.Text = dGVFotograf.CurrentRow.Cells[1].Value.ToString();
            textDescrip.Text = dGVFotograf.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            MySqlCommand command = new MySqlCommand("INSERT INTO fotos(ID, Nombre_del_evento, Descripción, Imagen) VALUES (@id, @nombre, @desc, @imag )", conn);

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = textId.Text;
            command.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = textEvento.Text;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = textDescrip.Text;
            command.Parameters.Add("@imag", MySqlDbType.Blob).Value = img;

            ExecMyQuery(command, "Datos Insertados");
        }

        public void ExecMyQuery(MySqlCommand mcomd, string myMsg)
        {
            conn.Open();
            if(mcomd.ExecuteNonQuery() == 1)
            {
                MessageBox.Show(myMsg);

            } 
            else
            {
                MessageBox.Show("Query Not Executed");
            }
            conn.Close();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            byte[] img = ms.ToArray();
            MySqlCommand command = new MySqlCommand("UPDATE fotos SET Nombre_del_evento = @nombre, Descripción = @desc, Imagen=@imag WHERE ID = @id", conn);

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = textId.Text;
            command.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = textEvento.Text;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = textDescrip.Text;
            command.Parameters.Add("@imag", MySqlDbType.Blob).Value = img;

            ExecMyQuery(command, "Datos cargados");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
                        
            MySqlCommand command = new MySqlCommand("DELETE FROM fotos WHERE ID = @id", conn);

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = textId.Text;

            ExecMyQuery(command, "Datos Eliminados");
        }
    }

}
