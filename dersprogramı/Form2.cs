using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dersprogramı
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        //Veri Tabanı Değişkenlerini Tanımlama Bölümü
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=vt1.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();


        public static Size size { get; private set; }

        private void Form2_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            listele();

        }

        private void metroRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            metroButton1.Location = new Point(125, 462);
            metroButton2.Location = new Point(23, 462);
            this.Width = 321;
            this.Height = 522; 
        }
        //DataGridWiev de hocaları listeleme bölümü
        void temizle()
        {   metroTextBox1.Text = "";
            metroTextBox2.Text = "";
            metroComboBox1.Text = "";
            metroRadioButton1.Checked = true;
        }
        void listele()
        {
            metroGrid1.ClearSelection();
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from hoca", baglanti);
            adtr.Fill(ds, "hoca");
            metroGrid1.DataSource = ds.Tables["hoca"];
            adtr.Dispose();
            baglanti.Close();
            metroGrid1.Columns[0].Visible = false;
            metroGrid1.Columns[3].Visible = false;
            metroGrid1.Columns[4].Visible = false;
            metroGrid1.Columns[5].Visible = false;
            metroGrid1.Columns[6].Visible = false;
            metroGrid1.Columns[7].Visible = false;
            metroGrid1.Columns[8].Visible = false;
            metroGrid1.Columns[9].Visible = false;
            metroGrid1.Columns[10].Visible = false;
            metroGrid1.Columns[11].Visible = false;
            metroGrid1.Columns[12].Visible = false;
            metroGrid1.Columns[13].Visible = false;
            metroGrid1.Columns[1].HeaderText = "Ünvanı";
            metroGrid1.Columns[2].HeaderText = "İsim";
            metroGrid1.ClearSelection();

        }

        private void metroRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            metroButton1.Location = new Point(125, 356);
            metroButton2.Location = new Point(23, 356);
            metroCheckBox1.Checked = true;
            metroCheckBox2.Checked = true;
            metroCheckBox3.Checked = true;
            metroCheckBox4.Checked = true;
            metroCheckBox5.Checked = true;

            this.Width = 321;  
            this.Height = 400; 
        }


        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty) // sadece textbox1 e bakıyor
            { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
            else
            {
                if (metroTextBox2.Text == string.Empty)
                { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
                else
                {
                    if (metroComboBox1.Text == string.Empty)
                    { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
                    else
                    {
                        adtr = new OleDbDataAdapter("SElect *from hoca where numara like '%" + metroTextBox2.Text + "%'", baglanti);
                        ds = new DataSet();
                        baglanti.Open();
                        adtr.Fill(ds, "Tablo1");
                        dataGridView1.DataSource = ds.Tables["Tablo1"];
                        int kayitsayisi;
                        kayitsayisi = dataGridView1.RowCount;
                        baglanti.Close();
                        if (kayitsayisi == 1)
                        {
                            komut.Connection = baglanti;
                            komut.CommandText = "Insert Into hoca(ünvan,isim,numara,pazartesi1,pazartesi2,sali1,sali2,carsamba1,carsamba2,persembe1,persembe2,cuma1,cuma2) Values ('" + metroComboBox1.Text + "','" + metroTextBox1.Text + "','" + metroTextBox2.Text + "','" + metroCheckBox1.Checked + "','" + metroCheckBox1.Checked + "','" + metroCheckBox2.Checked + "','" + metroCheckBox2.Checked + "','" + metroCheckBox3.Checked + "','" + metroCheckBox3.Checked + "','" + metroCheckBox4.Checked + "','" + metroCheckBox4.Checked + "','" + metroCheckBox5.Checked + "','" + metroCheckBox5.Checked + "')";
                            baglanti.Open();
                            komut.ExecuteNonQuery();
                            komut.Dispose();
                            baglanti.Close();

                            ds.Clear();
                            listele();
                            MessageBox.Show("Kayıt Eklendi", "Bilgilendirme Penceresi");
                            temizle();
                        }
                        else
                        {
                            MessageBox.Show("Aynı sicil numarası zaten ekli.", "Bilgilendirme Penceresi");
                            
                        }

                    }

                }
            }
        }

                    
            

                
            
        

  

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                DialogResult q;
                q = MessageBox.Show(this, "\n\nSilmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                ;
                if (q == DialogResult.Yes)
                {
                    baglanti.Open();
                    komut.Connection = baglanti;
                    komut.CommandText = "Delete from hoca where nu=" + textBox1.Text + "";
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                    ds.Clear();
                    listele();
                }

     

        }
    }

        private void metroGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = metroGrid1.CurrentRow.Cells[0].Value.ToString();
            metroComboBox1.Text = metroGrid1.CurrentRow.Cells[1].Value.ToString();
            metroTextBox1.Text = metroGrid1.CurrentRow.Cells[2].Value.ToString();
            metroTextBox2.Text = metroGrid1.CurrentRow.Cells[3].Value.ToString();
            if (metroGrid1.CurrentRow.Cells[4].Value.ToString() == "True")
            { metroCheckBox1.Checked = true; }
            else
            { metroCheckBox1.Checked = false; }
            if (metroGrid1.CurrentRow.Cells[6].Value.ToString() == "True")
            { metroCheckBox2.Checked = true; }
            else
            { metroCheckBox2.Checked = false; }
            if (metroGrid1.CurrentRow.Cells[8].Value.ToString() == "True")
            { metroCheckBox3.Checked = true; }
            else
            { metroCheckBox3.Checked = false; }
            if (metroGrid1.CurrentRow.Cells[10].Value.ToString() == "True")
            { metroCheckBox4.Checked = true; }
            else
            { metroCheckBox4.Checked = false; }
            if (metroGrid1.CurrentRow.Cells[12].Value.ToString() == "True")
            { metroCheckBox5.Checked = true; }
            else
            { metroCheckBox5.Checked = false; }
            metroRadioButton2.Checked = true;
            groupBox1.Visible = true;
            metroButton1.Location = new Point(125, 462);
            metroButton2.Location = new Point(23, 462);
        }
    }
}
