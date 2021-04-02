using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Benzin_istanyonu_otomasyonu
{
    public partial class satis : Form
    {
        public satis()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("provider=microsoft.ace.oledb.12.0;data source=" + Application.StartupPath + "\\benzin_istasyonu.accdb");
        DataTable tablo = new DataTable();


        public void listele()
        {
            textBox1.Text = "";
            textBox2.Text = "0";
            textBox3.Text = "0";
            comboBox1.Text = "";
            comboBox2.Text = "";
            label9.Text = "0";
            label8.Text = "0";
            tablo.Clear();
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter
          ("select * from satis", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            baglanti.Close();
        }
        private void satis_Load(object sender, EventArgs e)
        {
            listele();
            //access bağlantıyı sağladık
            OleDbCommand komut = new OleDbCommand("select * from musteri", baglanti);
            //access komutumuzu yazdık komutta veritabanındaki admin tablosunda kullanıcı adı textbox1.text olan şifresi textbox2.text olan veriyi
            // çekmesini istedik
            baglanti.Open();//bağlantıyı açdık

            OleDbDataReader oku = komut.ExecuteReader();//veriyi okutma emrini verdik
          while (oku.Read())//if eğer veriyi okumuşsa yani böyle bir kullanıcı veritabanında kayıtlıysa
            {

                comboBox1.Items.Add(oku.GetValue(1));

            }
            baglanti.Close();
            ///////////////////////////////////////////////////////////////
            //access bağlantıyı sağladık
            OleDbCommand komut2 = new OleDbCommand("select * from depo", baglanti);
           
            baglanti.Open();//bağlantıyı açdık

            OleDbDataReader oku2 = komut2.ExecuteReader();//veriyi okutma emrini verdik
            while (oku2.Read())//if eğer veriyi okumuşsa yani böyle bir kullanıcı veritabanında kayıtlıysa
            {

                comboBox2.Items.Add(oku2.GetValue(1));

            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=benzin_istasyonu.accdb;Persist Security Info=True");
                //access bağlantıyı sağladık
                OleDbCommand komut = new OleDbCommand("select * from musteri where tc='" + comboBox1.Text + "'", baglanti);
                //access komutumuzu yazdık komutta veritabanındaki admin tablosunda kullanıcı adı textbox1.text olan şifresi textbox2.text olan veriyi
                // çekmesini istedik
                baglanti.Open();//bağlantıyı açdık

                OleDbDataReader oku = komut.ExecuteReader();//veriyi okutma emrini verdik
                if (oku.Read())//if eğer veriyi okumuşsa yani böyle bir kullanıcı veritabanında kayıtlıysa
                {

                    textBox1.Text = oku.GetValue(7).ToString();

                }
            }
            catch 
            {

                ;
            }
          
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=benzin_istasyonu.accdb;Persist Security Info=True");
                //access bağlantıyı sağladık
                OleDbCommand komut = new OleDbCommand("select * from depo where akaryakit_adi='" + comboBox2.Text + "'", baglanti);
                //access komutumuzu yazdık komutta veritabanındaki admin tablosunda kullanıcı adı textbox1.text olan şifresi textbox2.text olan veriyi
                // çekmesini istedik
                baglanti.Open();//bağlantıyı açdık

                OleDbDataReader oku = komut.ExecuteReader();//veriyi okutma emrini verdik
                if (oku.Read())//if eğer veriyi okumuşsa yani böyle bir kullanıcı veritabanında kayıtlıysa
                {

                    textBox2.Text = oku.GetValue(3).ToString();
                    label8.Text = oku.GetValue(2).ToString();
                }
            }
            catch
            {

                ;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int depodaolan, satilan,sonkalan = 0;
            depodaolan = int.Parse(textBox2.Text);
            satilan = int.Parse(textBox3.Text);
            if (depodaolan>satilan)
            {

                if (baglanti.State == ConnectionState.Open)
                {

                    baglanti.Close();
                }
                baglanti.Open();

                OleDbCommand kmt;

                kmt = new OleDbCommand
                ("INSERT INTO satis(arac_plaka,akaryakit_turu,satilan_litre,fiyat,tarih) values('" + textBox1.Text + "','" + comboBox2.Text + "','" + textBox3.Text + "','"+label9.Text+"','"+DateTime.Now.ToShortDateString()+"')", baglanti);
                kmt.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış İşlemi Başarılı");
                // DEPODAN GEREKLİ MİKTARİ DÜŞÜRELİM

                sonkalan = depodaolan - satilan;

                OleDbCommand kmt2;
                baglanti.Open();
                kmt2 = new OleDbCommand("UPDATE depo SET depo_kalan='" + sonkalan.ToString() + "'where akaryakit_adi='" + comboBox2.Text + "'", baglanti);
                kmt2.ExecuteNonQuery();
                baglanti.Close();
                listele();

            }
            else
            {
                textBox3.Text = "0";
                MessageBox.Show("Depodaki Kapasiteden Fazla Satmayı Deniyorsunuz ! ");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                label9.Text = (int.Parse(label8.Text) * int.Parse(textBox3.Text)).ToString();
            }
            catch (Exception hata)
            {
                textBox3.Text = "1";
                MessageBox.Show(hata.Message);
                
            }
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
