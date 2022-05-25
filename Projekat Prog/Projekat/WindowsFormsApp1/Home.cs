using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {
            Logika logika = new Logika();
            string[] itemcol= new string[100];
            DataSet vremena = new DataSet();
            vremena = logika.Kategorije();

            comboBox1.DataSource = vremena.Tables[0];
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "ime";

           

        }
        
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logika logika = new Logika();
            DataSet prod = new DataSet();
            string kat = comboBox1.Text;
            prod = logika.Prodavnice(kat);
            comboBox2.DataSource = prod.Tables[0];
            comboBox2.ValueMember = "cena";
            comboBox2.DisplayMember = "naziv";

            dataGridView1.DataSource = prod.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Logika logika = new Logika();
            string prod = comboBox2.Text;
            string kor = Korisnik.email;
            string vreme = comboBox3.Text;
            string usluga = comboBox1.Text;
            int idprod = logika.NadjiProd(prod);
            int idkor = logika.NadjiKor(kor);
            DateTime sad = DateTime.Now;
            int ku = DateTime.Compare(dateTimePicker1.Value,sad);
            if (ku>=0)
            {
                int rez = logika.UpisiTermin(idkor, idprod, dateTimePicker1.Value, vreme, usluga);
                if (rez == 1)
                {
                    label4.Text = "Uspesno Ste zakazali dan.";
                }
                else
                {
                    label4.Text = "nesto niste doro uneli";
                }
            }
            else
            {
                label4.Text = "urposlost a?";
            }
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logika logika = new Logika();
            DataSet vremena = new DataSet();
            string prod = comboBox2.Text;
            vremena = logika.OstlVreme(prod,dateTimePicker1.Value);

            comboBox3.DataSource = vremena.Tables[0];
            comboBox3.ValueMember = "vreme";
            comboBox3.DisplayMember = "vreme";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Logika logika = new Logika();
            string term = textBox1.Text;
            dataGridView1.DataSource = logika.Search(term).Tables[0];


        }
    }
}
