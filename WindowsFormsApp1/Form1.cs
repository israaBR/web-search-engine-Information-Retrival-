﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using mshtml;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\faculty\web-search-engine-Information-Retrival-\WindowsFormsApp1\database.mdf;Integrated Security=True");
        private List<String> visitedURLs, toBeVisitedURLs, currentlyVisitingURLs;
        private static List<String> _BlockedUrls = new List<String>();

        public Form1()
        {
            //initialize URL lists
            visitedURLs = new List<String>();
            toBeVisitedURLs = new List<String>();
            currentlyVisitingURLs = new List<String>();

            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e){ }
        private void label1_Click(object sender, EventArgs e){ }
        private void label3_Click(object sender, EventArgs e){ }


        private void crawel_button_Click(object sender, EventArgs e)
        {
            con.Open();

            // crawling processing steps
            // 1.get seed URL(when crawling starts first time)
            String seedURL = url_text.Text;

            
            // 2.fetch the document at the URL
            String Rstring = get_URL_content(url_text.Text);
            ISet<string> Links = GetContent(Rstring);//gets the links only

            // 3.Parse the URL – HTML parser

            // 4. Extract links from it to other docs (URLs)





            con.Close();
        }
        private void pause_button_Click(object sender, EventArgs e)
        {

        }

        private String get_URL_content(String URL)
        {
            String Rstring;
            WebRequest myWebRequest;
            WebResponse myWebResponse;

            myWebRequest = WebRequest.Create(URL);
            myWebResponse = myWebRequest.GetResponse();//Returns a response from an Internet resource

            Stream streamResponse = myWebResponse.GetResponseStream();//return the data stream from the internet and save it in the stream

            StreamReader sreader = new StreamReader(streamResponse);//reads the data stream
            Rstring = sreader.ReadToEnd();//reads it to the end

            streamResponse.Close();
            sreader.Close();
            myWebResponse.Close();
            return Rstring;
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
        
        private void parse_robots_file(String URL)
        {
            Uri CurrentURL = new Uri(URL);
            string RobotsTxtFile = "http://" + CurrentURL.Authority + "/robots.txt";
            string FileContents = get_URL_content(RobotsTxtFile);
            if (!String.IsNullOrEmpty(FileContents))
            {
                string[] fileLines = FileContents.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                bool Applied = false;
                foreach (string line in fileLines)
                {
                    RobotCommand CommandLine = new RobotCommand(line);
                    switch (CommandLine.Command)
                    {
                        case "user-agent":   // User-Agent
                            if ((CommandLine.UserAgent.IndexOf("*") >= 0))
                            {
                                // these rules are applied to our useragent so on next line if its a DISALLOW we will add the URL
                                // to our array of URls we cannot access
                                Applied = true;
                            }
                            else
                            {
                                Applied = false;
                            }
                            break;
                        case "disallow":   // Disallow
                            if (Applied)
                            {
                                // Only add to blocked if the URL is not blank
                                if (CommandLine.Url.Length > 0)
                                {
                                    _BlockedUrls.Add(CommandLine.Url.ToLower());
                                }
                            }
                            break;
                        default:
                            // comment/allow/sitemap/empty/unknown/error
                            break;
                    }
                }
            }
        }
        private bool URL_is_allowed(String URL)
        {
            // If no URLS stored in blocked URLs list then all URLs are allowed
            if (_BlockedUrls.Count == 0) return true;

            // Convert string into an Uri object to easily access the relative path excluding the host and domain etc.
            Uri checkURL = new Uri(URL);
            URL = checkURL.AbsolutePath.ToLower();

            if (URL == "/robots.txt")
            {
                return false;
            }
            else
            {
                // loop our array checking for substring matches
                foreach (string blockedURL in _BlockedUrls)
                {
                    if (URL.Length >= blockedURL.Length)
                    {
                        if (URL.Substring(0, blockedURL.Length) == blockedURL)
                        {
                            // found a DISALLOW command
                            return false;
                        }
                    }
                }
            }
            //the URL is not disallowed
            return true;
        }
        private String URL_normalization(String URL)
        {
            // 1.put URL in lower case
            String finalURL = URL.ToLower();

            // 2.remove '/' from the end of the URL
            if (finalURL[finalURL.Length - 1].Equals('/'))
                finalURL.Remove(finalURL.Length - 1);

            // 3.removing 'www.' part if exists
            if (finalURL.Contains("www."))
                finalURL.Replace("www.", "");

            // 4.remove fragment part'#' if exists
            int fragmentPosition = finalURL.IndexOf('#');
            if(fragmentPosition > 0) //fragment exists
                finalURL = finalURL.Substring(0, fragmentPosition);

            // 5.remove default port(80 for http/ 443 for https)
            if (finalURL.Contains(":80"))
                finalURL.Replace(":80", "");
            if (finalURL.Contains(":443"))
                finalURL.Replace(":443", "");

            // 6.remove 's' from 'https'
            if (finalURL.Contains("https"))
                finalURL.Replace("https", "http");

            return finalURL;
        }
        private bool URL_is_exist(String URL)
        {
            //check if URL has been crawled already and stored in database
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from url_data where url = '" + URL + "'";
            Int32 count = (Int32)cmd.ExecuteScalar();
            con.Close(); 

            if (count > 0)
                return true;
            return false;
        }
    }
}
