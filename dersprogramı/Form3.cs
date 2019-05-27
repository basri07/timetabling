using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.OleDb;

namespace dersprogramı
{
    public partial class Form3 : MetroForm

    {
        public Form3()
        {
            InitializeComponent();
        }
        //Veri Tabanı Değişkenlerini Tanımlama Bölümü
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=vt1.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        OleDbDataAdapter adtr1 = new OleDbDataAdapter();
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();

        //DataGridWiev de dersleri listeleme bölümü
        void listele()
        {
            metroGrid1.ClearSelection();
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from ders", baglanti);
            adtr.Fill(ds, "ders");
            metroGrid1.DataSource = ds.Tables["ders"];
            adtr.Dispose();
            baglanti.Close();
            metroGrid1.Columns[0].Visible = false;
            metroGrid1.Columns[3].Visible = false;
            metroGrid1.Columns[4].Visible = false;
            metroGrid1.Columns[5].Visible = false;
            metroGrid1.Columns[6].Visible = false;
            metroGrid1.Columns[8].Visible = false;
            metroGrid1.Columns[1].HeaderText = "Ders Kodu";
            metroGrid1.Columns[2].HeaderText = "Dersin Adı";
            metroGrid1.Columns[7].HeaderText = "Dersin Hocası";

            metroGrid1.ClearSelection();

        }
        void combo()
        {
            metroComboBox2.Items.Clear();
            OleDbDataReader oku;
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "Select isim from hoca";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                metroComboBox2.Items.Add(oku[0]);


            }
            baglanti.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listele();
            combo();
        }


        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty) 
            { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
            else
            {
                if (metroTextBox2.Text == string.Empty) 
                { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
                else
                {
                    if (metroTextBox3.Text == string.Empty)
                    { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
                    else
                    {
                        if (metroComboBox1.Text == string.Empty) 
                        { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
                        else
                        {
                            if (metroComboBox2.Text == string.Empty) 
                            { MessageBox.Show("Boş Alan Bırakmayınız", "Bilgilendirme Penceresi"); }
                            else
                            {
                                adtr = new OleDbDataAdapter("SElect *from ders where derskod like '%" + metroTextBox1.Text + "%'", baglanti);
                                ds = new DataSet();
                                baglanti.Open();
                                adtr.Fill(ds, "Tablo1");
                                dataGridView2.DataSource = ds.Tables["Tablo1"];
                                int kayitsayisi;
                                kayitsayisi = dataGridView2.RowCount;
                                baglanti.Close();

                                adtr1 = new OleDbDataAdapter("SElect *from hoca where isim like '%" + metroComboBox2.Text + "%'", baglanti);
                                ds1 = new DataSet();
                                baglanti.Open();
                                adtr1.Fill(ds1, "Tablo1");
                                dataGridView1.DataSource = ds1.Tables["Tablo1"];
                                label1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                                label2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                                baglanti.Close();

                                if (kayitsayisi == 1)
                                {
                                    if (metroCheckBox2.Checked == true)
                                    {
                                        
                                        if (metroCheckBox1.Checked == true)
                                        {
                                            baglanti.Close();
                                            komut.Connection = baglanti;
                                            komut.CommandText = "Insert Into ders(derskod,ad,saat,sinif,oturum,grup,hoca,hocano,hocaa) Values ('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "(1.Grup)','" + metroTextBox3.Text + "','" + metroComboBox1.Text + "','2','" + metroCheckBox2.Checked + "','" + metroComboBox2.Text + "','" + label1.Text + "','" + label2.Text + "')";

                                            baglanti.Open();
                                            komut.ExecuteNonQuery();
                                            komut.Dispose();
                                            baglanti.Close();

                                            komut.Connection = baglanti;
                                            komut.CommandText = "Insert Into ders(derskod,ad,saat,sinif,oturum,grup,hoca,hocano,hocaa) Values ('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "(2.Grup)','" + metroTextBox3.Text + "','" + metroComboBox1.Text + "','2','" + metroCheckBox2.Checked + "','" + metroComboBox2.Text + "','" + label1.Text + "','" + label2.Text + "')";

                                            baglanti.Open();
                                            komut.ExecuteNonQuery();
                                            komut.Dispose();
                                            baglanti.Close();
                                            ds.Clear();
                                            listele();
                                        }
                                        else
                                        {
                                            baglanti.Close();
                                            komut.Connection = baglanti;
                                            komut.CommandText = "Insert Into ders(derskod,ad,saat,sinif,oturum,grup,hoca,hocano,hocaa) Values ('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "(1.Grup)','" + metroTextBox3.Text + "','" + metroComboBox1.Text + "','1','" + metroCheckBox2.Checked + "','" + metroComboBox2.Text + "','" + label1.Text + "','" + label2.Text + "')";

                                            baglanti.Open();
                                            komut.ExecuteNonQuery();
                                            komut.Dispose();
                                            baglanti.Close();

                                            komut.Connection = baglanti;
                                            komut.CommandText = "Insert Into ders(derskod,ad,saat,sinif,oturum,grup,hoca,hocano,hocaa) Values ('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "(2.Grup)','" + metroTextBox3.Text + "','" + metroComboBox1.Text + "','1','" + metroCheckBox2.Checked + "','" + metroComboBox2.Text + "','" + label1.Text + "','" + label2.Text + "')";

                                            baglanti.Open();
                                            komut.ExecuteNonQuery();
                                            komut.Dispose();
                                            baglanti.Close();
                                            MessageBox.Show("Kayıt Tamamlandı!");

                                            ds.Clear();
                                            listele();
                                        }

                                    }
                                    else
                                    {
                                        if (metroCheckBox1.Checked == true)
                                        {
                                            baglanti.Close();
                                            komut.Connection = baglanti;
                                            komut.CommandText = "Insert Into ders(derskod,ad,saat,sinif,oturum,grup,hoca,hocano,hocaa) Values ('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "','" + metroTextBox3.Text + "','" + metroComboBox1.Text + "','2','" + metroCheckBox2.Checked + "','" + metroComboBox2.Text + "','" + label1.Text + "','" + label2.Text + "')";

                                            baglanti.Open();
                                            komut.ExecuteNonQuery();
                                            komut.Dispose();
                                            baglanti.Close();
                                            MessageBox.Show("Kayıt Tamamlandı!");
                                            ds.Clear();
                                            listele();
                                        }
                                        else
                                        {
                                            baglanti.Close();
                                            komut.Connection = baglanti;
                                            komut.CommandText = "Insert Into ders(derskod,ad,saat,sinif,oturum,grup,hoca,hocano,hocaa) Values ('" + metroTextBox1.Text + "','" + metroTextBox2.Text + "','" + metroTextBox3.Text + "','" + metroComboBox1.Text + "','1','" + metroCheckBox2.Checked + "','" + metroComboBox2.Text + "','" + label1.Text + "','" + label2.Text + "')";

                                            baglanti.Open();
                                            komut.ExecuteNonQuery();
                                            komut.Dispose();
                                            baglanti.Close();
                                            MessageBox.Show("Kayıt Tamamlandı!");
                                            ds.Clear();
                                            listele();
                                            metroTextBox1.Text = string.Empty;
                                            metroTextBox2.Text = string.Empty;
                                            metroTextBox3.Text = string.Empty;
                                            metroComboBox1.Text = string.Empty;
                                            metroComboBox2.Text = string.Empty;

                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Aynı ders kodu zaten ekli.", "Bilgilendirme Penceresi");
                                }
                            }
                        }
                    }
                }
            }
        }

       

        private void metroButton2_Click(object sender, EventArgs e)
        {
            {
                if (textBox1.Text != "")
                {
                    DialogResult q;
                    q = MessageBox.Show("Silmek istediğinizden emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (q == DialogResult.Yes)
                    {
                        baglanti.Open();
                        komut.Connection = baglanti;
                        komut.CommandText = "Delete from ders where nu=" + textBox1.Text + "";
                        komut.ExecuteNonQuery();
                        komut.Dispose();
                        baglanti.Close();
                        ds.Clear();
                        listele();
                    }
                }
            }
        }

        private void metroGrid1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = metroGrid1.CurrentRow.Cells[0].Value.ToString();
            metroTextBox1.Text = metroGrid1.CurrentRow.Cells[1].Value.ToString();
            metroTextBox2.Text = metroGrid1.CurrentRow.Cells[2].Value.ToString();
            metroTextBox3.Text = metroGrid1.CurrentRow.Cells[3].Value.ToString();
            metroComboBox1.Text = metroGrid1.CurrentRow.Cells[4].Value.ToString();
            metroComboBox2.Text = metroGrid1.CurrentRow.Cells[7].Value.ToString();
            label1.Text = metroGrid1.CurrentRow.Cells[8].Value.ToString();
            if (metroGrid1.CurrentRow.Cells[5].Value.ToString() == "2")
            { metroCheckBox1.Checked = true; }
            else
            { metroCheckBox1.Checked = false; }
            if (metroGrid1.CurrentRow.Cells[6].Value.ToString() == "False")
            { metroCheckBox2.Checked = false; }
            else
            { metroCheckBox2.Checked = true; }
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            int x;
            x = Convert.ToInt32(metroTextBox3.Text);
            if (x % 2 == 1)
            {
                MessageBox.Show("Sadece ders saati çift olan dersler iki oturuma bölünebilir", "Bilgilendirme Penceresi");
                metroCheckBox1.Checked = false;

            }
            else
            {
                
            }
        
        }

        private void metroTextBox3_TextChanged(object sender, EventArgs e)
        {
            metroCheckBox1.Checked = false;
        }

        private void metroComboBox2_Enter(object sender, EventArgs e)
        {
            combo();
            
        }
    }
    
}
