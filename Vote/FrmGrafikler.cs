using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Vote
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source = ACELIK; Initial Catalog = Vote; Integrated Security = True");
        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            // ilçe adlarını comboboxa çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ILAD from TBL_OY",baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBoxILADI.Items.Add(dr[0]);
            }
            baglanti.Close();

            // grafiğe sonuçları getirme
            baglanti.Open();
            SqlCommand komutChart = new SqlCommand("Select Sum(APARTI),Sum(BPARTI),Sum(CPARTI),Sum(DPARTI),Sum(EPARTI) From TBL_OY",baglanti);
            SqlDataReader drChart = komutChart.ExecuteReader();
            while (drChart.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A Parti",drChart[0]);
                chart1.Series["Partiler"].Points.AddXY("B Parti",drChart[1]);
                chart1.Series["Partiler"].Points.AddXY("C Parti",drChart[2]);
                chart1.Series["Partiler"].Points.AddXY("D Parti",drChart[3]);
                chart1.Series["Partiler"].Points.AddXY("E Parti",drChart[4]);
            }
            baglanti.Close();
        }

        private void comboBoxILADI_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutCombo = new SqlCommand("Select * from TBL_OY where ILAD=@P1", baglanti);
            komutCombo.Parameters.AddWithValue("@P1", comboBoxILADI.Text);
            SqlDataReader drCombo = komutCombo.ExecuteReader();
            while (drCombo.Read())
            {
                progressBar1.Value = int.Parse(drCombo[2].ToString());
                progressBar2.Value = int.Parse(drCombo[3].ToString());
                progressBar3.Value = int.Parse(drCombo[4].ToString());
                progressBar4.Value = int.Parse(drCombo[5].ToString());
                progressBar5.Value = int.Parse(drCombo[6].ToString());

                lblA.Text = drCombo[2].ToString();
                lblB.Text = drCombo[3].ToString();
                lblC.Text = drCombo[4].ToString();
                lblD.Text = drCombo[5].ToString();
                lblE.Text = drCombo[6].ToString();
            }
            baglanti.Close();
        }

        private void BtnGeri_Click(object sender, EventArgs e)
        {
            FrmOyGiris fr = new FrmOyGiris();
            fr.Show();
        }
    }
}
