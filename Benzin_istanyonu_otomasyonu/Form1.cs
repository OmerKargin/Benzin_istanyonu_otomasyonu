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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("provider=microsoft.ace.oledb.12.0;data source=" + Application.StartupPath + "\\benzin_istasyonu.accdb");
        DataTable tablo = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand komut = new OleDbCommand("select * from admin where kullanici_adi='" + textBox1.Text + "' and sifre ='" + textBox2.Text + "'", baglanti);
            //access komutumuzu yazdık komutta veritabanındaki admin tablosunda kullanıcı adı textbox1.text olan şifresi textbox2.text olan veriyi
            // çekmesini istedik
            baglanti.Open();//bağlantıyı açdık

            OleDbDataReader oku = komut.ExecuteReader();//veriyi okutma emrini verdik
            if (oku.Read())//if eğer veriyi okumuşsa yani böyle bir kullanıcı veritabanında kayıtlıysa
            {
                MessageBox.Show("Giriş Başarılı !");//giriş başarılı diye uyari verir
                baglanti.Close();//bağlantıyı kapar
                menu menu = new menu();//yeni bir menü sayfası oluşturur
                menu.Show();//menü sayfasını açar
                this.Hide();////mevcut sayfayı gizler

            }
            else
            {
                MessageBox.Show("Kullanıcı Adınız Yada Şifreniz Yanlış Yazılmıştır");//hayır veri okuyamadıysa uyarı verir
                textBox1.Text = "";
                textBox2.Text = "";
                baglanti.Close();//bağlantıyı kapar
                //verileri temizler
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
