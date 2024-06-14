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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //Her bir Şehirde Kaç Öğrenci Olduğunu Listeliyoruz.
            var degerler = db.TBLOGRENCI.OrderBy(x => x.SEHIR).GroupBy(y => y.SEHIR).Select(z => new
            {
                Şehir = z.Key,
                Toplam = z.Count()
            });
            dataGridView1.DataSource = degerler.ToList();

            //MAx Ortalama Bulma Kodlları

            var max = db.TBLNOTLAR.Max(x => x.ORTALAMA).ToString();
            label1.Text = max;


            //Mİn Ortalama Buloma
            label2.Text = db.TBLNOTLAR.Min(x => x.ORTALAMA).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //1.Yöntem
            label3.Text = db.TBLNOTLAR.Where(x => x.DURUM == false).Max(y => y.ORTALAMA).ToString();

            //2.Yöntem
            var SONUC = db.TBLNOTLAR.Where(x => x.DURUM == false).OrderByDescending(y => y.ORTALAMA).Take(1).Select(z => new
            {
                Ogrenci = z.OGR,
                Ortalama = z.ORTALAMA,
                Durum = z.DURUM
            });
            dataGridView1.DataSource = SONUC.ToList();




            //BURADA SUM,AVERAGE,COUNT METOT UYGULAMALARI LABEL4 ÜZERİNDE UYGULAYACAĞIM

            //Toplam TBL Urun Adedi Bulmaa
            label4.Text = db.TBLURUN.Count().ToString();
            //Toplam Buzdolabı kaç adet
            label4.Text = db.TBLURUN.Count(x => x.AD == "BUZDOLABI").ToString();
            // Toplam STok sayısını Bulur
            label4.Text = db.TBLURUN.Sum(x => x.STOK).ToString();

            //Toplam Fiyatın Ortalmasını Bulma
            label4.Text=db.TBLURUN.Average(x=>x.FIYAT).ToString();

            //Ortalama Buzdolabının Fiyatı
            label4.Text = db.TBLURUN.Where(y => y.AD == "BUZDOLABI").Average(x => x.FIYAT).ToString();

            //En Pahalı Ürünüm HAngisi onun ismini Bulma

            label4.Text = (from deger in db.TBLURUN orderby deger.STOK descending select deger.AD).First();

        }

        private void BtnProcedure_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Kulupler().ToList();
        }
    }
}
