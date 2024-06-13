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
        DbSinavOgrenciEntities db=new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //Her bir Şehirde Kaç Öğrenci Olduğunu Listeliyoruz.
            var degerler = db.TBLOGRENCI.OrderBy(x => x.SEHIR).GroupBy(y => y.SEHIR).Select(z => new
            {
                Şehir = z.Key,
                Toplam = z.Count()
            });
            dataGridView1.DataSource = degerler.ToList();
        }
    }
}
