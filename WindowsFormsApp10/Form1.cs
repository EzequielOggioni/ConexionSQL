using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        List<Provincia> provincias;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            provincias = new List<Provincia>();
            SqlConnection cn = new SqlConnection(
                Properties.Settings.Default.CadenaASQL);

            SqlCommand cm = new SqlCommand("Select id, nombre from provincias", cn);
            cm.Connection = cn;
            cm.CommandText = "Select id, nombre from provincias";
            cn.Open();

            SqlDataReader dr =  cm.ExecuteReader();
            
            while( dr.Read())
            {
                 provincias.Add(new Provincia((int)((decimal)dr["id"]), 
                     dr["nombre"].ToString()));
            }

            cn.Close();

            this.cmbPcia.DataSource = provincias;

            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbPcia.SelectedIndex > -1) {
                SqlConnection cn = new SqlConnection(
                   Properties.Settings.Default.CadenaASQL);

                SqlCommand cm = new SqlCommand();
                cm.Connection = cn;
                

                string consulta = "insert into localidades (ProvinciaId, nombre ) values ( ";
                consulta += ((Provincia)cmbPcia.SelectedItem).Id.ToString() + ",'" + txtLocalidad.Text + "')";
                cn.Open();
                cm.CommandText = consulta;

                cm.ExecuteNonQuery();



                cn.Close();
            }
        }
    }
}
