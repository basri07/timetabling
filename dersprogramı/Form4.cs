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
using System.IO;
using System.Data.OleDb;

namespace dersprogramı
{
    public partial class Form4 : MetroForm
    {
        public Form4()
        {
            InitializeComponent();

        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=vt1.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        OleDbDataAdapter adtr1 = new OleDbDataAdapter();
        OleDbDataAdapter adtr2 = new OleDbDataAdapter();
        OleDbDataAdapter adtr3 = new OleDbDataAdapter();


        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();

        string FilePath5 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gamsdir\\projdir\\_Ders.csv";
        void csv()
        {
            string delimiter = ",";
            string tablename = "export";
            string filename = (FilePath5);


            DataSet dataset = new DataSet();
            StreamReader sr = new StreamReader(filename);

            dataset.Tables.Add(tablename);
            dataset.Tables[tablename].Columns.Add("Hoca");
            dataset.Tables[tablename].Columns.Add("Ders");
            dataset.Tables[tablename].Columns.Add("Sınıf");
            dataset.Tables[tablename].Columns.Add("Oturum");
            dataset.Tables[tablename].Columns.Add("Gün");
            dataset.Tables[tablename].Columns.Add("Saat");
            string allData = sr.ReadToEnd();
            string[] rows = allData.Split("\r".ToCharArray());
            foreach (string r in rows)
            {
                string[] items = r.Split(delimiter.ToCharArray());
                dataset.Tables[tablename].Rows.Add(items);
            }
            this.dataGridView1.DataSource = dataset.Tables[0].DefaultView;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            csv();
            yaz();
            doldur();
            metroGrid1.Columns[0].Visible = true;
            metroGrid1.Rows[5].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[10].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[15].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[20].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[25].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[30].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[35].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[40].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Rows[45].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Columns[3].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Columns[6].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Columns[9].DefaultCellStyle.BackColor = Color.Bisque;
            metroGrid1.Columns[3].Width = 30;
            metroGrid1.Columns[6].Width = 30;
            metroGrid1.Columns[9].Width = 30;



        }
        void doldur()
        {
            baglanti.Open();
            OleDbDataAdapter adtr3 = new OleDbDataAdapter("Select * from prog", baglanti);
            adtr3.Fill(ds3, "ders");
            metroGrid1.DataSource = ds3.Tables["ders"];
            adtr3.Dispose();
            baglanti.Close();

        }
        void yaz()
        {
            int rowcount = dataGridView1.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {

                adtr1 = new OleDbDataAdapter("SElect *from ders where nu like '%" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "%'", baglanti);
                ds1 = new DataSet();
                baglanti.Open();
                adtr1.Fill(ds1, "Tablo1");
                dataGridView2.DataSource = ds1.Tables["Tablo1"];
                baglanti.Close();

                string ders = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                string hoca = dataGridView2.CurrentRow.Cells[9].Value.ToString() + " "  +dataGridView2.CurrentRow.Cells[7].Value.ToString();
                string sınıf = dataGridView1.Rows[i].Cells[2].Value.ToString();


                if (sınıf == "1")
                {
                    
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE prog SET 1d = '"+ ders  + "',1h = '" + hoca + "' where saat ='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + dataGridView1.Rows[i].Cells[5].Value.ToString() + "'";
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                }

                if (sınıf == "2")
                {
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE prog SET 2d = '" + ders + "',2h = '" + hoca + "' where saat ='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + dataGridView1.Rows[i].Cells[5].Value.ToString() + "'";
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                }
                if (sınıf == "3")
                {
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE prog SET 3d = '" + ders + "',3h = '" + hoca + "' where saat ='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + dataGridView1.Rows[i].Cells[5].Value.ToString() + "'";
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                }
                if (sınıf == "4")
                {
                    komut.Connection = baglanti;
                    komut.CommandText = "UPDATE prog SET 4d = '" + ders + "',4h = '" + hoca + "' where saat ='" + dataGridView1.Rows[i].Cells[4].Value.ToString() + dataGridView1.Rows[i].Cells[5].Value.ToString() + "'";
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    komut.Dispose();
                    baglanti.Close();
                }
               
            }
        }
    }
}
