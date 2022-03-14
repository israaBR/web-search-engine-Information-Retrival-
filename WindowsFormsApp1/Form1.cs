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
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Esraa\source\repos\WindowsFormsApp1\WindowsFormsApp1\database.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
   
        private void label3_Click(object sender, EventArgs e)
        {
          

        }

        private void crawel_button_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "insert into url_data values ('" + url_text + "','" + url2_text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("added successfully");
        }
    }
}
