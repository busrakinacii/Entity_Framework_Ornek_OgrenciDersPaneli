using EntityOrnek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void BtnLinqEntity_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                var deger = db.TBLNOTLAR.Where(x => x.SINAV1 < 50);
                dataGridView1.DataSource = deger.ToList();

            }
            else if (radioButton1.Checked == true)
            {
                var liste1 = db.TBLOGRENCI.Where(x => x.AD == "ali");
                dataGridView1.DataSource = liste1.ToList();
            }
            else if (radioButton2.Checked == true)
            {
                var liste2 = db.TBLOGRENCI.Where(x => x.AD == TxtAdSoyad.Text || x.SOYAD == TxtAdSoyad.Text);
                dataGridView1.DataSource = liste2.ToList();
            }
            else if (radioButton3.Checked == true)
            {
                var liste3 = db.TBLOGRENCI.Select(x => new { SOYADLAR = x.SOYAD });
                dataGridView1.DataSource = liste3.ToList();
            }
            else if (radioButton4.Checked == true)
            {
                var liste4 = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToUpper(), Soyad = x.SOYAD.ToLower() });
                dataGridView1.DataSource = liste4.ToList();
            }
            else if (radioButton5.Checked == true)
            {
                var liste5 = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToUpper(), Soyad = x.SOYAD.ToLower() }).Where(x => x.Ad != "Ali");
                dataGridView1.DataSource = liste5.ToList();
            }
            else if (radioButton6.Checked == true)
            {
                var liste6 = db.TBLNOTLAR.Select(x =>
                new
                {
                    OgrenciAd = x.OGR,
                    Ortalama = x.ORTALAMA,
                    Durum = x.DURUM == true ? "Geçti" : "Kaldı"
                });
                dataGridView1.DataSource = liste6.ToList();
            }
            else if (radioButton7.Checked == true)
            {
                var liste7 = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR), (x, y) => new
                {
                    y.AD,
                    x.ORTALAMA,
                    Durum = x.DURUM == true ? "Geçti" : "Kaldı",

                });
                dataGridView1.DataSource = liste7.ToList();  
            }
            else if(radioButton9.Checked==true)
            {
                var liste9 = db.TBLOGRENCI.OrderBy(x => x.ID).Take(3).ToList();
                dataGridView1.DataSource = liste9.ToList();
            }
            else if(radioButton10.Checked==true)
            {
                var liste10 = db.TBLOGRENCI.OrderByDescending(x => x.ID).Take(3).ToList();
                dataGridView1.DataSource = liste10.ToList();
            }
            else if(radioButton11.Checked == true)
            {
                var liste11 = db.TBLOGRENCI.OrderBy(x => x.AD).ToList();
                dataGridView1.DataSource = liste11.ToList();
            }
            else
            {
                var liste12 = db.TBLOGRENCI.OrderBy(x => x.ID).Skip(5).ToList();
                dataGridView1.DataSource = liste12.ToList();
            }
        }
    }
}
