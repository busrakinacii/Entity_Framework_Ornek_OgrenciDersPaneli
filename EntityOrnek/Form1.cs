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
using EntityOrnek.Model;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnDersListesi_Click(object sender, EventArgs e)
        {
            SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-QUL77PV\SQLEXPRESS;Initial Catalog=DbSinavOgrenci;Integrated Security=True;");
            SqlCommand komut = new SqlCommand("Select * From TBLDERSLER", bgl);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt= new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnOgrListele_Click(object sender, EventArgs e)
        {
           DbSinavOgrenciEntities db=new DbSinavOgrenciEntities();
            dataGridView1.DataSource = db.TBLOGRENCI.ToList();
        }
    }
}
