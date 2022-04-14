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
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using mshtml;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //modify connection string if database is not working
        private static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\faculty\web-search-engine-Information-Retrival-\WindowsFormsApp1\database.mdf;Integrated Security=True");
        private Queue<String> toBeVisitedURLs;
        private List<String> currentlyVisitingURLs, _BlockedUrls;
        int crawled_documents_number;
        String URLsFilePath;
        Queue<KeyValuePair<int, String>> URLs;
        string[] copyOfText;
        public Form1()
        {
            //initialize URL lists
            toBeVisitedURLs = new Queue<String>();
            currentlyVisitingURLs = new List<String>();
            _BlockedUrls = new List<String>();
            crawled_documents_number = get_documents_number();
            URLsFilePath = @"c:\URLs";

            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e){ }
        private void label1_Click(object sender, EventArgs e){ }
        private void label3_Click(object sender, EventArgs e){ }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Indexer Functions
        /// </summary>
        private void indexingButton_Click(object sender, EventArgs e)
        {
            //get URLs from database
             get_URLs();

            
            while(URLs.Count != 0)
            {
                KeyValuePair<int, String> URL = URLs.Dequeue();

                //indexing steps
                //1.parse the text
                String RString = get_URL_content(URL.Value);
                String text = extract_text(RString);

                //2.tokenize it
                List<String> listOfTokens = tokenize(text);

                //3.apply linguistics algorithim

                //remove punctuation character + casefolding

                //stop word removal

                //stemming

                //4.save it in the inverted index

            }
        }

        private void get_URLs()
        {
            //get all URLs from database and return them in alist
            URLs = new Queue<KeyValuePair<int, string>>();
            string sqlQuery = "SELECT * FROM url_data";
            SqlCommand command = new SqlCommand(sqlQuery, con);
            try
            { 
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KeyValuePair<int, String> URL = new KeyValuePair<int, String>((Int32)reader["Id"], ["url"].ToString());
                    URLs.Enqueue(URL);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        private String extract_text(String rString)
        {
            String text = String.Empty;
            IHTMLDocument2 myDoc = new HTMLDocumentClass();
            myDoc.write(rString);
            text = myDoc.body.innerText;
            return text;
        }
        List<string> tokenize(string page_text)
        {
            //store words from page content after tokenize
            List<string> list = new List<string>();
            string word = "";
            int i = 0;
            foreach (char ch in page_text)
            {
                i++;
                if (ch == ' ' || ch == '.' || ch == ',' || ch == ':' || ch == ';' || ch == '/' || ch == '?' || ch == '!')
                {
                    if (!word.Equals(""))
                    {
                        list.Add(word);
                        word = "";
                    }

                }
                else
                {
                    word += ch;
                }
                //if last char in page content
                if (i == page_text.Length)
                {
                    list.Add(word);
                    break;
                }
            }
            return list;
        }
        private string Remove_Punctuation(string text)
        {
            var sb = new StringBuilder();
            foreach (char c in text)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            string str = sb.ToString();

            return str.ToLower();
        }
        private string remove_stopWords(string text)
        {
            var words_to_remove = new HashSet<string> {"this","that","with","myself","a", "an","the","able", "about", "above", "abst", "accordance", "according",  "accordingly",
  "across",  "act", "actually", "added", "adj",  "affected",
  "affecting",  "affects",  "after", "afterwards",
  "again",  "against",  "ah",  "all",  "almost",  "alone",
  "along",  "already",  "also",  "although", "always",  "am",  "among",  "amongst",  "an","and",
  "announce","another","any","anybody","anyhow","anymore",  "anyone",
  "anything",  "anyway","anyways","anywhere","apparently","approximately","are","aren","arent", "arise",
  "around", "as", "aside", "ask","asking",
  "at", "auth", "available", "away", "awfully", "b","back", "be", "became", "because", "become","becomes",
  "becoming","been", "before", "beforehand", "begin", "beginning", "beginnings", "begins",
  "behind", "being","believe",  "below", "beside","besides",  "between",
  "beyond", "biol", "both",  "brief",  "briefly",  "but",  "by",  "c",  "ca",
  "came",  "can",  "cannot",  "can't",  "cause",  "causes","certain",  "certainly",  "com",
  "come",  "comes",  "contain",  "containing",  "contains",  "could",  "couldnt",  "d",  "date",  "did", "didn't",
  "different", "do",  "does",  "doesn't",  "doing",  "done",  "don't",  "down",  "downwards",  "due",  "during",
  "e",  "each",  "ed",  "edu",  "effect", "eg",  "eight", "eighty",  "either",  "else",
 "elsewhere",  "end",  "ending",  "enough",  "especially",  "et",  "et-al", "etc",  "even",
  "ever",  "every",  "everybody",  "everyone",  "everything",  "everywhere",  "ex",  "except",  "f",  "far", "few",  "ff",  "fifth","first",
  "five", "fix", "followed", "following","follows", "for",
  "former",  "formerly", "forth", "found",  "four",  "from",  "further",  "furthermore",  "g",  "gave", "get",  "gets", "getting", "give",  "given", "gives",
  "giving",  "go","goes",  "gone",  "got",  "gotten", "h",  "had",  "happens", "hardly",
  "has", "hasn't",
  "have", "haven't", "having", "he", "hed", "hence", "her","here","hereafter",
  "hereby",  "herein", "heres", "hereupon", "hers","herself",
 "hes","his", "hither", "how", "howbeit", "however","hundred",
 "in",  "inc" ,"indeed", "index", "information",  "instead",
  "into",  "invention", "inward","is",  "it'll",
  "its",  "itself", "i've",
  "j",  "just",  "k",  "keep	keeps",  "kept",  "kg",  "km",
  };

            string output = string.Join(
                " ",
                text
                    .Split(new[] { ' ', '\t', '\n', '\r', '.', '!', '?', ':', '/' })
                    .Where(word => !words_to_remove.Contains(word))
            );

            return output;
        }
        

        private string stemming(string unstemmed)
        {
            copyOfText = unstemmed.Split(' ');
            var stemmer = new EnglishPorter2Stemmer();
            var stemmed = stemmer.Stem(unstemmed).Value;
            return stemmed;
        }
        private void pauseIndexing_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Crawler Functions
        /// </summary>
        private void crawel_button_Click(object sender, EventArgs e)
        {
            // crawling processing steps(Breadth-first)
            //check if URLs file contains URLs from prevous crawling
            if (!URLs_file_exist())
            {
                // 1.get seed URL(when crawling starts first time)
                // add to list of URLs to be visited
                toBeVisitedURLs.Enqueue(url_text.Text);
                // parse URL's Robot.txt file
                _BlockedUrls.Clear();
                parse_robots_file(url_text.Text);
            }

            // 2.fetch the document at the URL
            while (toBeVisitedURLs.Count > 0)
            {
                // get URL from to be visited and add it to currently visiting URLs
                String URL = toBeVisitedURLs.Dequeue();
                currentlyVisitingURLs.Add(URL);


                String Rstring = get_URL_content(url_text.Text);
                bool lang = false;
                foreach (var a in Rstring.Split(' '))
                {
                    if (a.Equals("lang=\"en\""))
                    {
                        lang = true;
                        break;
                    }
                }
                // 3.Parse the URL – HTML parser
                // Extract links from it to other docs(URLs)
                List<String> Links = extract_links(Rstring);

                // 4.check if URL passes filter tests
                //check if extracted URLs are allowed
                for (int i = 0; i < Links.Count; i++)
                {
                    if (URL_is_allowed(Links.First()))
                    {
                        //normalize the URL
                        String newURL = URL_normalization(Links.First());
                        //check if exists in URLs to be visited or in database
                        if (!toBeVisitedURLs.Contains(newURL) && !URL_is_exist(newURL))
                            toBeVisitedURLs.Enqueue(newURL);
                    }
                    Links.Remove(Links.First());////////
                }
                if (lang == true)
                {
                    //extract text
                    //String text = extract_text(Rstring);

                    //store it in database
                    store_URL_in_database(URL_normalization(URL), "");
                    //remove from currently visiting URLs and display URL in crawled URLs
                    currentlyVisitingURLs.Remove(URL);
                    crawledURLs_txt.AppendText(URL + "\r\n");
                    crawled_documents_number++;
                    documentsNumber_txt.Clear();
                    documentsNumber_txt.AppendText(crawled_documents_number.ToString());
                }
                Console.WriteLine(URL);
            }
        }
        private void pause_button_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(URLsFilePath);

            // write URLs in currentlyVisiting list to the file
            sw.WriteLine(currentlyVisitingURLs.Count);
            for (int i = 0; i < currentlyVisitingURLs.Count; i++)
                sw.Write(currentlyVisitingURLs[i]);

            // write URLs in toBeVisited queue to the file
            sw.WriteLine(toBeVisitedURLs.Count);
            for (int i = 0; i < toBeVisitedURLs.Count; i++)
                sw.Write(toBeVisitedURLs.Dequeue());

            sw.Close();
            Application.Exit();
        }
        private String get_URL_content(String URL)
        {
            String Rstring = String.Empty;
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
        private List<String> extract_links(String rString)
        {
            List<String> links = new List<String>();
            IHTMLDocument2 myDoc = new HTMLDocumentClass();
            myDoc.write(rString);
            IHTMLElementCollection elements = myDoc.links;
            foreach (IHTMLElement el in elements)
            {
                links.Add((string)el.getAttribute("href", 0));
            }
            return links;
        }
        private void parse_robots_file(String URL)
        {  
            //string RobotsTxtFile = "http://" + URL + "/robots.txt";
            string RobotsTxtFile = URL + "/robots.txt";

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
            // Uri checkURL = new Uri(URL);
            // URL = checkURL.AbsolutePath.ToLower();
            URL.ToLower();
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
                        if (URL.Equals(blockedURL))
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
                finalURL = finalURL.Remove(finalURL.Length - 1);

            // 3.removing 'www.' part if exists
            if (finalURL.Contains("www."))
                finalURL = finalURL.Replace("www.", "");

            // 4.remove fragment part'#' if exists
            int fragmentPosition = finalURL.IndexOf('#');
            if(fragmentPosition > 0) //fragment exists
                finalURL = finalURL = finalURL.Substring(0, fragmentPosition);

            // 5.remove default port(80 for http/ 443 for https)
            if (finalURL.Contains(":80"))
                finalURL = finalURL.Replace(":80", "");
            else if (finalURL.Contains(":443"))
                finalURL = finalURL.Replace(":443", "");

            //// 6.remove 's' from 'https'
            //if (finalURL.Contains("https"))
            //    finalURL = finalURL.Replace("https", "http");

            // 6.remove 'http/s' from the URL
            if (finalURL.Contains("https:"))
                finalURL = finalURL.Replace("https:", "");
            else if (finalURL.Contains("http:"))
                finalURL = finalURL.Replace("http:", "");

            return finalURL;
        }
        private bool URL_is_exist(String URL)
        {
            //check if URL has been crawled already and stored in database
            con.Open();
            int count = 0;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from url_data where url = '" + URL + "'";
            if (cmd.ExecuteScalar() != null)
                count = (Int32)cmd.ExecuteScalar();
            con.Close(); 

            if (count > 0)
                return true;
            return false;
        }
        private void store_URL_in_database(String URL, String text)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            //cmd.CommandText = "insert into url_data values ('" + URL + "', '"+ text+ "');";
            cmd.CommandText = "insert into url_data (url) values ('" + URL + "');";
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private int get_documents_number()
        {
            //get number of records in url_data table in database
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            Int32 count;
            cmd.CommandText = "select * from url_data";
            if (cmd.ExecuteScalar() != null)
                count = (Int32)cmd.ExecuteScalar();
            else
                count = 0;
            con.Close();
            return count;
        }
        private bool URLs_file_exist()
        {
            // the file doesn't exist
            if (!File.Exists(URLsFilePath))
                return false;

            StreamReader sr = new StreamReader(URLsFilePath);
            // read URLs of currentlyVisiting list from the file
            int currentlyCount = int.Parse(sr.ReadLine());
            for (int i = 0; i < currentlyCount; i++)
                currentlyVisitingURLs.Add(sr.ReadLine());

            // read URLs of toBeVisited queue from the file
            int toBeCount = int.Parse(sr.ReadLine());
            for (int i = 0; i < toBeCount; i++)
                toBeVisitedURLs.Enqueue(sr.ReadLine());

            sr.Close();
            return true;
        }
    }
}