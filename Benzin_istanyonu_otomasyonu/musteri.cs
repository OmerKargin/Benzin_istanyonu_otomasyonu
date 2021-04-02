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
    public partial class musteri : Form
    {
        public musteri()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("provider=microsoft.ace.oledb.12.0;data source=" + Application.StartupPath + "\\benzin_istasyonu.accdb");
        DataTable tablo = new DataTable();
        public void listele()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            maskedTextBox1.Text = "";
            comboBox1.Text = "";

            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            tablo.Clear();
            baglan.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter
          ("select * from musteri", baglan);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            baglan.Close();
        }

        private void musteri_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (baglan.State == ConnectionState.Open)
            {

                baglan.Close();
            }
            baglan.Open();

            OleDbCommand kmt;

            kmt = new OleDbCommand
            ("INSERT INTO musteri(tc,adi_soyadi,dogum_tarihi,tel,email,uyelik_durumu,plaka) values('" + maskedTextBox3.Text + "','" + textBox1.Text + "','" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "')", baglan);
            kmt.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Kayıt Başarılı");
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OleDbCommand kmt;
            baglan.Open();
            kmt = new OleDbCommand("Delete from musteri where tc = '" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'", baglan);
            kmt.ExecuteNonQuery();
            kmt.Dispose();
            baglan.Close();
            MessageBox.Show("İşleminiz başarılı");
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            OleDbCommand kmt;
            baglan.Open();
            kmt = new OleDbCommand("UPDATE musteri SET tc='" + maskedTextBox3.Text + "',adi_soyadi='" + textBox1.Text + "',dogum_tarihi='" + maskedTextBox1.Text + "',tel='" + maskedTextBox2.Text + "',email='" + textBox2.Text + "',uyelik_durumu='" + comboBox1.Text + "',plaka='" + textBox3.Text + "'where tc='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'", baglan);
            kmt.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("İşleminiz başarılı");
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            tablo.Clear();
            baglan.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter
          ("select * from musteri where tc='" + textBox4.Text + "'", baglan);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].Visible = false;
            baglan.Close();
            textBox4.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            maskedTextBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            maskedTextBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
        }
    }
}
