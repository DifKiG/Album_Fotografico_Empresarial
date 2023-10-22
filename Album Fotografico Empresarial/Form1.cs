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
            FillDGV("");
        }

        public void FillDGV(string valueToSearch)
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM `fotos` WHERE CONCAT(ID, Nombre_del_evento, Descripción) LIKE '%" + valueToSearch + "%'", conn);

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
            catch(Exception ex)
            {
                MessageBox.Show("Error al hacer la consulta: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
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
                MessageBox.Show("Consulta no realizada");
            }
            conn.Close();

            FillDGV("");
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand("SELECT FROM fotos WHERE ID = @id", conn);

            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = textId.Text;
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Datos no encontrados");
            }
            else
            {
                textId.Text = table.Rows[0][0].ToString();
                textEvento.Text = table.Rows[0][1].ToString();
                textDescrip.Text = table.Rows[0][2].ToString();

                byte[] img = (byte[])table.Rows[0][3];
                MemoryStream ms = new MemoryStream(img);
                pictureBox1.Image = Image.FromStream(ms);

            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        public void ClearFields()
        {
            textId.Text =  "";
            textEvento.Text = "";
            textDescrip.Text = "";

            pictureBox1.Image = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FillDGV(textBox1.Text);
        }
    }
}
