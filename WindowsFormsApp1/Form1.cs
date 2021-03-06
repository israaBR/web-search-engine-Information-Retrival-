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
        int crawled_documents_number, indexed_documents_number;
        private List<invertedIndex> ListOfInvertedIndex = new List<invertedIndex>();
        String URLsFilePath;
        Queue<KeyValuePair<int, String>> URLs;
        Dictionary<string, string> copyOfText = new Dictionary<string, string>();
        public Form1()
        {
            //initialize URL lists
            toBeVisitedURLs = new Queue<String>();
            currentlyVisitingURLs = new List<String>();
            _BlockedUrls = new List<String>();
            crawled_documents_number = get_documents_number();
            indexed_documents_number = 0;
            URLsFilePath = @"c:\URLs";

            InitializeComponent();
        }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
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

            while (URLs.Count != 0)
            {
                KeyValuePair<int, String> URL = URLs.Dequeue();

                //indexing steps
                //1.parse the text
                String RString = get_URL_content(URL.Value);
                if (RString.Equals(String.Empty))
                    continue;
                else
                {
                    String text = extract_text(RString);

                    //2.tokenize it
                    List<String> listOfTokens = tokenize(text);

                    //3.apply linguistics algorithim
                    //remove punctuation character + casefolding
                    List<String> listOfTerms = Remove_Punctuation(listOfTokens);
                    //stop word removal
                    listOfTerms = remove_stopWords(listOfTerms);
                    //stemming
                    copy_list_of_terms(listOfTerms, URL.Key);
                    listOfTerms = stemming(listOfTerms);

                    //4.save it in the inverted index
                    StoreInvertedIndex(listOfTerms, URL.Key);

                    //remove from currently visiting URLs and display URL in crawled URLs
                    indexedPages_txt.AppendText(URL + "\r\n");
                    indexed_documents_number++;
                    indPgNumbers_txt.Clear();
                    indPgNumbers_txt.AppendText(indexed_documents_number.ToString());
                    //}
                    Console.WriteLine(URL);
                }

            }

            //store in database
            store_terms_in_database();
            store_invertedIndex_DB();
            indexedPages_txt.AppendText("-----Indexing Done-----");
            Console.WriteLine("-----Indexing Done-----");
        }

        private void get_URLs()
        {
            //get all URLs from database and return them in alist
            URLs = new Queue<KeyValuePair<int, string>>();
            //            string sqlQuery = "SELECT * FROM url_data";
            string sqlQuery = "SELECT * FROM URLData";

            SqlCommand command = new SqlCommand(sqlQuery, con);
            try
            {
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KeyValuePair<int, String> URL = new KeyValuePair<int, String>((Int32)reader["Id"], reader["_url"].ToString());
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

        private void store_terms_in_database()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            foreach (KeyValuePair<String, String> term in copyOfText)
            {
                cmd.CommandText = "insert into term_data values ('" + term.Key + "','" + term.Value + "');";
                cmd.ExecuteNonQuery();

            }
            con.Close();
        }

        private void store_invertedIndex_DB()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            foreach (invertedIndex index in ListOfInvertedIndex)
            {
                string term_index = index.freq.ToString() + ";";
                foreach (int doc in index.doc_pos.Keys)
                {
                    term_index += doc + ":" + index.doc_pos[doc] + ";";
                }

                cmd.CommandText = "insert into dictionary values ('" + index.Term + "','" + term_index + "');";
                cmd.ExecuteNonQuery();
            }
            con.Close();
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
                if (ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r' || ch == '.' || ch == ',' || ch == ':' || ch == ';' || ch == '/' || ch == '?' || ch == '!')
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

        private List<string> Remove_Punctuation(List<string> text)
        {
            List<string> newText = new List<string>();
            foreach (string word in text)
            {
                var sb = new StringBuilder();
                foreach (char c in word)
                {
                    if (!char.IsPunctuation(c))
                        sb.Append(c);
                }
                string str = sb.ToString();
                newText.Add(str.ToLower());
            }

            return newText;
        }

        private List<string> remove_stopWords(List<string> text)
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
  "has", "hasn't","or",
  "have", "haven't", "having", "he", "hed", "hence", "her","here","hereafter",
  "hereby",  "herein", "heres", "hereupon", "hers","herself",
 "hes","his", "hither", "how", "howbeit", "however","hundred",
 "in",  "inc" ,"indeed", "index", "information",  "instead",
  "into",  "invention", "inward","is",  "it'll",
  "its",  "itself", "i've",
  "j",  "just",  "k",  "keep	keeps",  "kept",  "kg",  "km",
  };

            foreach (string word in text.ToList())
            {
                if (words_to_remove.Contains(word))
                {
                    int index = text.FindIndex(s => s == word);

                    if (index != -1)
                        text[index] = String.Empty;
                }

            }

            /*
            string output = string.Join(
                " ",
                text
                    .Split(new[] { ' ', '\t', '\n', '\r', '.', '!', '?', ':', '/' })
                    .Where(word => !words_to_remove.Contains(word))
            );
            */
            return text;
        }

        private void copy_list_of_terms(List<string> listOfTerms, int docID)
        {
            foreach (string term in listOfTerms)
            {
                if (copyOfText.ContainsKey(term))
                {
                    string[] doc = copyOfText[term].Split(',');
                    if (!doc.Contains(docID.ToString()))
                        copyOfText[term] = copyOfText[term] + "," + docID.ToString();
                }
                else
                    copyOfText.Add(term, docID.ToString());
            }
        }

        private List<string> stemming(List<string> unstemmed)
        {
            List<string> stemmed = new List<string>();
            var stemmer = new EnglishPorter2Stemmer();
            foreach (string unstemmedWord in unstemmed)
            {
                var stemmedWord = stemmer.Stem(unstemmedWord).Value;
                stemmed.Add(stemmedWord);
            }
            return stemmed;
        }

        private void StoreInvertedIndex(List<string> ItemList, int documentID)
        {
            //int position = 1;
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i].Equals(String.Empty))
                    continue;
                else
                {

                    bool exist = false;
                    foreach (var list in ListOfInvertedIndex)
                    {
                        if (list.Term.Equals(ItemList[i]) && list.doc_pos.ContainsKey(documentID))//term exists and docID is the same
                        {
                            list.freq++;
                            //list.position.Add(inv.position.ElementAt(0));
                            list.doc_pos[documentID] = list.doc_pos[documentID] + "," + i.ToString();
                            exist = true;
                            break;
                        }
                        else if (list.Term.Equals(ItemList[i]))//term exists and different docID
                        {
                            list.freq++;
                            list.doc_pos.Add(documentID, i.ToString());
                            exist = true;
                            break;
                        }

                    }
                    if (!exist)//term do not exist
                    {
                        invertedIndex inv = new invertedIndex();
                        inv.freq = 1;
                        inv.Term = ItemList[i];
                        inv.doc_pos.Add(documentID, i.ToString());
                        ListOfInvertedIndex.Add(inv);
                        //inv.docId = documentID;
                        //inv.position.Add(i);

                    }
                }
            }
        }

        public class invertedIndex : IComparable<invertedIndex>
        {
            public string Term;
            //public int docId;
            public int freq;
            //public List<int> position = new List<int>();
            public Dictionary<int, String> doc_pos = new Dictionary<int, string>();
            public int CompareTo(invertedIndex obj)
            {
                if (this.Term[0] > obj.Term[0])
                    return 1;
                else if (this.Term[0] < obj.Term[0])
                    return -1;
                else
                    return 0;
            }
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
                //_BlockedUrls.Clear();
                //parse_robots_file(url_text.Text);
            }

            // 2.fetch the document at the URL
            while (toBeVisitedURLs.Count > 0)
            {
                // get URL from to be visited and add it to currently visiting URLs
                String URL = toBeVisitedURLs.Dequeue();
                currentlyVisitingURLs.Add(URL);


                String Rstring = get_URL_content(url_text.Text);
                //bool lang = false;
                //foreach (var a in Rstring.Split(' '))
                // {
                //   if (a.Equals("lang=\"en\""))
                // {
                //   lang = true;
                // break;
                //}
                //}
                // 3.Parse the URL – HTML parser
                // Extract links from it to other docs(URLs)
                List<String> Links = extract_links(Rstring);

                // 4.check if URL passes filter tests
                //check if extracted URLs are allowed
                for (int i = 0; i < Links.Count; i++)
                {
                    //  if (URL_is_allowed(Links.First()))
                    //{
                    //normalize the URL
                    String newURL = URL_normalization(Links.First());
                    if (newURL.Equals(String.Empty))
                    {
                        Links.Remove(Links.First());
                        continue;
                    }
                    //check if exists in URLs to be visited or in database
                    if (!toBeVisitedURLs.Contains(newURL) && !URL_is_exist(newURL))
                        toBeVisitedURLs.Enqueue(newURL);
                    //}
                    Links.Remove(Links.First());
                }
                //if (lang == true)
                //{
                //store it in database
                store_URL_in_database(URL);
                //remove from currently visiting URLs and display URL in crawled URLs
                currentlyVisitingURLs.Remove(URL);
                crawledURLs_txt.AppendText(URL + "\r\n");
                crawled_documents_number++;
                documentsNumber_txt.Clear();
                documentsNumber_txt.AppendText(crawled_documents_number.ToString());
                //}
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

            try
            {
                myWebRequest = WebRequest.Create(URL);
                myWebResponse = myWebRequest.GetResponse();//Returns a response from an Internet resource

                Stream streamResponse = myWebResponse.GetResponseStream();//return the data stream from the internet and save it in the stream
                StreamReader sreader = new StreamReader(streamResponse);//reads the data stream
                Rstring = sreader.ReadToEnd();//reads it to the end

                streamResponse.Close();
                sreader.Close();
                myWebResponse.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!SKIP!!! => " + e.Message);

            }
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
            if (URL.Substring(0, 5).Equals("about"))
            {
                if (URL.Contains("blank"))
                    return String.Empty;
                else
                    URL = URL.Replace(URL.Substring(0, 7), currentlyVisitingURLs[0]);
            }
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
            if (fragmentPosition > 0) //fragment exists
                finalURL = finalURL.Substring(0, fragmentPosition);

            // 5.remove default port(80 for http/ 443 for https)
            if (finalURL.Contains(":80"))
                finalURL = finalURL.Replace(":80", "");
            else if (finalURL.Contains(":443"))
                finalURL = finalURL.Replace(":443", "");

            // 6.remove 's' from 'https'
            if (finalURL.Contains("https"))
                finalURL = finalURL.Replace("https", "http");
            /*
            // 6.remove 'http/s' from the URL
            if (finalURL.Contains("https:"))
                finalURL = finalURL.Replace("https:", "");
            else if (finalURL.Contains("http:"))
                finalURL = finalURL.Replace("http:", "");
            */
            return finalURL;
        }

        private bool URL_is_exist(String URL)
        {
            //check if URL has been crawled already and stored in database
            con.Open();
            int count = 0;
            SqlCommand cmd = con.CreateCommand();
            //            cmd.CommandText = "select * from url_data where url = '" + URL + "'";
            cmd.CommandText = "select * from URLData where _url = '" + URL + "'";

            if (cmd.ExecuteScalar() != null)
                count = (Int32)cmd.ExecuteScalar();
            con.Close();

            if (count > 0)
                return true;
            return false;
        }

        private void store_URL_in_database(String URL)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            //            cmd.CommandText = "insert into url_data (url) values ('" + URL + "');";
            cmd.CommandText = "insert into URLData (_url) values ('" + URL + "');";

            cmd.ExecuteNonQuery();
            con.Close();
        }

        private int get_documents_number()
        {
            //get number of records in url_data table in database
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            Int32 count;
            //           cmd.CommandText = "select * from url_data";
            cmd.CommandText = "select * from URLData";

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