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
using System.IO;
using System.Diagnostics;




namespace dersprogramı
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Veri Tabanı Değişkenlerini Tanımlama Bölümü
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=vt1.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adtr = new OleDbDataAdapter();
        DataSet ds = new DataSet();

        //DataGridWiev de hocaları listeleme bölümü
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from hoca", baglanti);
            adtr.Fill(ds, "hoca");
            dataGridView1.DataSource = ds.Tables["hoca"];
            adtr.Dispose();
            baglanti.Close();
        }
        //DataGridWiev de dersleri listeleme bölümü
        void listele2()
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from ders", baglanti);
            adtr.Fill(ds, "ders");
            dataGridView2.DataSource = ds.Tables["ders"];
            adtr.Dispose();
            baglanti.Close();
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            listele2();




            /////////////////
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("Select * from gams", baglanti);
            adtr.Fill(ds, "gams");
            dataGridView3.DataSource = ds.Tables["gams"];
            adtr.Dispose();
            baglanti.Close();
            metroTextBox1.Text = dataGridView3.Rows[0].Cells[0].Value.ToString();

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Form2 Yeni = new Form2();

            Yeni.Show();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Form3 Yeni = new Form3();

            Yeni.Show();
        }
        string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gamsdir\\projdir\\tumtum.txt";
        string FilePath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gamsdir\\projdir\\gams.gms";
        string FilePath3 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gamsdir\\projdir\\";
        string FilePath4 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gamsdir\\projdir\\_Ders.txt";
        string FilePath5 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gamsdir\\projdir\\_Ders.csv";
        private void metroTile3_Click(object sender, EventArgs e)
        {
            
           
            if (Directory.Exists(@FilePath3))
            {
                Directory.Delete(@FilePath3, true);
                Directory.CreateDirectory(FilePath3);
            }

            if(!Directory.Exists(@FilePath3))
            {
                Directory.CreateDirectory(@FilePath3);
            }

            TextWriter sw2 = new StreamWriter(FilePath);
            int rowcount = dataGridView2.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {
                label2.Text = dataGridView2.Rows[i].Cells[5].Value.ToString();
                if (label2.Text == "1")
                {
                    sw2.WriteLine(dataGridView2.Rows[i].Cells[8].Value.ToString() + "." + dataGridView2.Rows[i].Cells[0].Value.ToString() + "." + dataGridView2.Rows[i].Cells[4].Value.ToString() + "." + dataGridView2.Rows[i].Cells[5].Value.ToString() + " " + dataGridView2.Rows[i].Cells[3].Value.ToString());
                }
                else
                {
                    sw2.WriteLine(dataGridView2.Rows[i].Cells[8].Value.ToString() + "." + dataGridView2.Rows[i].Cells[0].Value.ToString() + "." + dataGridView2.Rows[i].Cells[4].Value.ToString() + ".1 " + (Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value) / 2).ToString());
                    sw2.WriteLine(dataGridView2.Rows[i].Cells[8].Value.ToString() + "." + dataGridView2.Rows[i].Cells[0].Value.ToString() + "." + dataGridView2.Rows[i].Cells[4].Value.ToString() + ".2 " + (Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value) / 2).ToString());
                }

            }
            sw2.Flush();
            sw2.Close();

            TextWriter sw = new StreamWriter(FilePath2);
            int rowcount2 = dataGridView1.Rows.Count;

            //hoca numaraları ekleme

            sw.WriteLine("SETS");
            sw.Write("h hoca /");
            for (int i = 0; i < rowcount2 - 1; i++)
            {
                if (i != rowcount2 - 2)
                {
                    sw.Write(dataGridView1.Rows[i].Cells[0].Value.ToString() + ",");
                }
                else
                {
                    sw.Write(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
            sw.WriteLine("/");
            sw.Write("d ders /");
            for (int i = 0; i < rowcount - 1; i++)
            {
                if (i != rowcount - 2)
                {
                    sw.Write(dataGridView2.Rows[i].Cells[0].Value.ToString() + ",");
                }
                else
                {
                    sw.Write(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
            }
            sw.WriteLine("/");
            sw.WriteLine("j sinif /1*4/");
            sw.WriteLine("p periot /1*2/");
            sw.WriteLine("g gun /0*9/");
            sw.WriteLine("s saat /1*4/");
            sw.WriteLine("alias(s,ss);");
            sw.WriteLine("PARAMETER TUM(h,d,j,p)");
            sw.WriteLine("/");
            sw.Write("$include \"");sw.Write(FilePath); sw.WriteLine("\"");
            sw.WriteLine("/");
            //sw.WriteLine("KAT(h,d,j,p,g,s)");
            //sw.WriteLine("/");
            // sw.WriteLine("$include \"D:\\kati.txt\"");
            sw.WriteLine(";");
            sw.WriteLine("VARIABLE Z;");
            sw.WriteLine("BINARY VARIABLE");
            sw.WriteLine("T(h,d,j,p,g,s)");
            sw.WriteLine(",U(h,d,j,p,g,s)");
            sw.WriteLine(";");
            sw.WriteLine("T.fx(h,d,j,p,g,s) $ (TUM(h,d,j,p)=0)=0;");
            sw.WriteLine("*T.fx(h,d,j,p,g,s) = KAT(h,d,j,p,g,s) = TUM(h,d,j,p);");
            sw.WriteLine("EQUATIONS");
            sw.WriteLine("OBJ");
            sw.WriteLine(",K1");
            sw.WriteLine(",K2");
            sw.WriteLine(",K3");
            sw.WriteLine(",K4");
            //sw.WriteLine(",K5");
            sw.WriteLine(",K6");
            sw.WriteLine(",K7");
            sw.WriteLine(";");
            sw.WriteLine("OBJ.. Z =E= SUM ((h,d,j,p,g,s), T(h,d,j,p,g,s));");
            sw.WriteLine("K1(h,d,j,p).. SUM ((g,s), T(h,d,j,p,g,s)) =E= TUM(h,d,j,p);");
            sw.WriteLine("K2(h,g,s).. SUM ((d,j,p), T(h,d,j,p,g,s)) =L= 1;");
            sw.WriteLine("K3(d,p,g,s).. SUM ((h,j), T(h,d,j,p,g,s)) =L= 1;");
            sw.WriteLine("K4(j,g,s).. SUM ((h,d,p), T(h,d,j,p,g,s)) =L= 1;");
            //sw.WriteLine("K5(h,d,j,p,g,s)..        KAT(h,d,j,p,g,s) =L= T(h,d,j,p,g,s);");
            sw.WriteLine("K6(h,d,j,p,g,s)$(ord(s)<=(4-TUM(h,d,j,p)+1)).. SUM(ss $((ord(ss)>=ord(s)) and (ord(ss)<=(ord(s)+TUM(h,d,j,p)-1))), T(h,d,j,p,g,ss)) =G= TUM(h,d,j,p)*U(h,d,j,p,g,s);");
            sw.WriteLine("K7(h,d,j,p).. SUM((g,s) $(ord(s)<=card(s)-TUM(h,d,j,p)+1), U(h,d,j,p,g,s)) =E= 1;");
            sw.WriteLine("MODEL ProgramDeneme  /ALL/;");
            sw.WriteLine("ProgramDeneme.optfile=1;");
            sw.WriteLine("ProgramDeneme.optcr=0;");
            sw.WriteLine("ProgramDeneme.reslim=72000;");
            sw.WriteLine("ProgramDeneme.iterlim=1e9;");
            sw.WriteLine("ProgramDeneme.limrow=0;");
            sw.WriteLine("ProgramDeneme.limcol=0;");
            sw.WriteLine("$onecho > cplex.opt");
            sw.WriteLine("workmem 10000");
            sw.WriteLine("nodefileind 3");
            sw.WriteLine("$offecho");
            sw.WriteLine("SOLVE ProgramDeneme USING MIP MAXIMIZING Z;");
            sw.WriteLine("display  T.l;");
            sw.WriteLine("file outfile /_Ders.txt/;");
            sw.WriteLine("put outfile;");
            sw.WriteLine("put 'Objective_Value '; put Z.l; put /;");
            sw.WriteLine("put 'Lower_Bound '; put ProgramDeneme.ObjEst; put /;");
            sw.WriteLine("put 'Number_of_Iteration '; put ProgramDeneme.iterusd; put /;");
            sw.WriteLine("put 'IsOptimum '; put ProgramDeneme.modelstat; put /;");
            sw.WriteLine("put 'CPU_Second '; put ProgramDeneme.resusd; put /;");
            sw.WriteLine("put /; put 'Assignments_T(h,d,j,p,g,s)'; put /;");
            sw.WriteLine("loop((h,d,j,p,g,s) $ (T.l(h,d,j,p,g,s)>0),");
            sw.WriteLine("      put h.tl;");
            sw.WriteLine("      put d.tl;");
            sw.WriteLine("      put j.tl;");
            sw.WriteLine("      put p.tl;");
            sw.WriteLine("      put g.tl;");
            sw.WriteLine("      put s.tl;");
            sw.WriteLine("      put T.l(h,d,j,p,g,s);");
            sw.WriteLine("      put /;");
            sw.WriteLine("); ");
            sw.Flush();
            sw.Close();

            Process p = new Process();
            p.StartInfo.FileName = FilePath2;
            p.StartInfo.WorkingDirectory = metroTextBox1.Text.ToString();
            p.StartInfo.Arguments = "\" gams \"";
            p.Start();
            p.WaitForExit();

            MessageBox.Show("GAMS Sonuçları oluşturuldu");

            string[] lines = File.ReadAllLines(FilePath4).Skip(7).ToArray();
            File.WriteAllLines(FilePath4, lines);


            string text = File.ReadAllText(FilePath4);
            text = text.Replace("                   1.00", "");
            text = text.Replace("        ", ",");
            text = text.Replace(" ", "");

            File.WriteAllText(FilePath5, text);
            ///////////////////////



            ///////////////////////
            //System.Diagnostics.Process.Start(FilePath5);

        }

        

        private void metroButton1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Klasor = new FolderBrowserDialog();
            Klasor.ShowDialog();
            metroTextBox1.Text = Klasor.SelectedPath + "\\gams.exe";

            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "UPDATE gams SET yol='" + metroTextBox1.Text + "' where nu=1";
            komut.ExecuteNonQuery();
            ds.Clear();
         

        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            Form4 Yeni = new Form4();

            Yeni.Show();
        }
    }
}
