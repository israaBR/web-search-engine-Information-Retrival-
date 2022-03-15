using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

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

            String Rstring;

            WebRequest myWebRequest;
            WebResponse myWebResponse;
            String URL = url_text.Text;

            myWebRequest = WebRequest.Create(URL);
            myWebResponse = myWebRequest.GetResponse();//Returns a response from an Internet resource

            Stream streamResponse = myWebResponse.GetResponseStream();//return the data stream from the internet
                                                                      //and save it in the stream

            StreamReader sreader = new StreamReader(streamResponse);//reads the data stream
            Rstring = sreader.ReadToEnd();//reads it to the end
            ISet<string> Links = GetContent(Rstring);//gets the links only


            streamResponse.Close();
            sreader.Close();
            myWebResponse.Close();







            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("added successfully");
        }


        private ISet<string> GetContent(String Rstring)
        {
            Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            ISet<string> newLinks = new HashSet<string>();
            foreach (var match in regexLink.Matches(Rstring))
            {
                if (!newLinks.Contains(match.ToString()))
                    newLinks.Add(match.ToString());
            }

            return newLinks;

            
        }
    }
}
