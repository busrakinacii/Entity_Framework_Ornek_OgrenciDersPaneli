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
using System.ComponentModel.Design.Serialization;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void BtnDersListesi_Click(object sender, EventArgs e)
        {
            SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-QUL77PV\SQLEXPRESS;Initial Catalog=DbSinavOgrenci;Integrated Security=True;");
            SqlCommand komut = new SqlCommand("Select * From TBLDERSLER", bgl);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        void listele()
        {
            dataGridView1.DataSource = db.TBLOGRENCI.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }
        //public void deger()
        // {
        //     int id = Convert.ToInt32(TxtOgrID.Text);
        //      var x= db.TBLOGRENCI.Find(id);
        // }

        private void BtnOgrListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            var query = from item in db.TBLNOTLAR
                        select new { item.NOTID, item.TBLOGRENCI.AD, item.TBLOGRENCI.SOYAD, item.TBLDERSLER.DERSAD, item.SINAV1, item.SINAV2, item.SINAV3, item.ORTALAMA, item.DURUM };

            dataGridView1.DataSource = query.ToList();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = TxtOgrAd.Text;
            t.SOYAD = TxtOgrSoyad.Text;
            db.TBLOGRENCI.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Listeye Kaydedilmiştir.");
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrID.Text);
            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sistemden Silinmiştir.");
            listele();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(TxtOgrID.Text);
            var x = db.TBLOGRENCI.FirstOrDefault(a => a.ID == id);
            x.AD = TxtOgrAd.Text;
            x.SOYAD = TxtOgrSoyad.Text;
            x.FOTOGRAF = TxtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Güncellenmiştir..:)");
            listele();
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == TxtOgrAd.Text && x.SOYAD == TxtOgrSoyad.Text).ToList();
        }

        private void TxtOgrAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtOgrAd.Text;
            var x = from item in db.TBLOGRENCI
                    where item.AD.Contains(aranan)
                    select item;
            dataGridView1.DataSource = x.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                //Asc
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            else if (radioButton1.Checked == true)
            {
                //desc /Description
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;

            }
            else if (radioButton3.Checked == true)
            {
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = liste3;

            }
            else if (radioButton4.Checked == true)
            {
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.Where(p => p.ID == 5).ToList();
                dataGridView1.DataSource = liste4;
            }
            else if (radioButton5.Checked == true)
            {
                //A harfi ile başlayanlar 
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(p => p.AD.StartsWith("A")).ToList();
                dataGridView1.DataSource = liste5;
            }
            else if (radioButton6.Checked == true)
            {
                //A harfi ile bitenler
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(p => p.AD.EndsWith("A")).ToList();
                dataGridView1.DataSource = liste6;
            }

            else if (radioButton7.Checked == true)
            {
                bool deger = db.TBLOGRENCI.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (radioButton8.Checked == true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton9.Checked == true)
            {
                var sinav1top = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show("Öğrencilerin Toplam Sınav1 Puanları:" + sinav1top.ToString(), "Sınav 1 Toplamı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton10.Checked == true)
            {
                var sinav1ort = db.TBLNOTLAR.Average(p => p.SINAV1);
                MessageBox.Show("Öğrencilerin Toplam Sınav1 Puan Ortalamaları:" + sinav1ort.ToString(), "Sınav 1 Ortalama", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (radioButton11.Checked == true)
            {
                var maxsinav1 = db.TBLNOTLAR.Max(p => p.SINAV1);
                MessageBox.Show("Sınav1 Max Puan" + maxsinav1.ToString(), "Sınav 1 Max Puan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton12.Checked == true)
            {
                var minsinav1 = db.TBLNOTLAR.Min(p => p.SINAV1);
                MessageBox.Show("Sınav1 Min Puan" + minsinav1.ToString(), "Sınav 1 Min Puan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton13.Checked == true)
            {
                var ort = db.TBLNOTLAR.Average(p => p.SINAV1);
                List<TBLNOTLAR> list = db.TBLNOTLAR.Where(p => p.SINAV1 > ort).ToList();
                dataGridView1.DataSource = list;

            }
            else
            {
                var max = db.TBLNOTLAR.Max(p => p.SINAV1);
                dataGridView1.DataSource = db.NOTLISTESI().Where(p => p.SINAV1 == max).ToList();

            }
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.TBLNOTLAR
                        join d2 in db.TBLOGRENCI
                        on d1.OGR equals d2.ID
                        join d3 in db.TBLDERSLER
                        on d1.DERS equals d3.DERSID

                        select new
                        {
                            ÖĞRENCİ = d2.AD + " " + d2.SOYAD,
                            DERS_ADI=d3.DERSAD,
                            SINAV1 = d1.SINAV1,
                            SINAV2 = d1.SINAV2,
                            SINAV3 = d1.SINAV3,
                            ORTALAMA = d1.ORTALAMA,
                            DURUM = d1.DURUM
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
