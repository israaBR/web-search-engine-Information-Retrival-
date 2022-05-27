using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3: Form
    {
        private static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\faculty\web-search-engine-Information-Retrival-\WindowsFormsApp1\searchEngineDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        private static String fileName = String.Empty;
        private List<String> done_terms = new List<string>();
        public Form3()
        {
            InitializeComponent();
        }

        private void calculatebtn_Click(object sender, EventArgs e)
        {
            String URL = URLtxt.Text;
            
            //extract text
            String RString = get_URL_content(URL);

            //extract terms
            String text = extract_text(RString);
            List<String> list_of_terms = tokenize(text);
            list_of_terms = Remove_Punctuation(list_of_terms);
            list_of_terms = remove_stopWords(list_of_terms);
            Dictionary<string, double> term_tfidf = new Dictionary<string, double>();
            for(int y= 0; y<list_of_terms.Count;y++)
            {
                if(!term_tfidf.ContainsKey(list_of_terms[y]))
                    term_tfidf.Add(list_of_terms[y], 0);
            }
            numberOfTermstxt.Clear();
            numberOfTermstxt.AppendText(term_tfidf.Count.ToString());
            numberOfCalTermstxt.AppendText("0");

            //calculate TF-IDF
            foreach (KeyValuePair<String, double> term in term_tfidf.ToList())
            {
                //term frequency
                String record = get_term_record(term.Key);
                int URLID = get_URL_ID(URL);
                int termFreq = frequency_of_term(record, URLID);
                if (termFreq == -1)
                    continue;
                double TF = calculate_term_frequency(Convert.ToDouble(termFreq), Convert.ToDouble(URLID));
                //Inverse Document Frequency:
                int total_docs = total_number_of_documents();
                int term_docs = term_number_of_documents(record);
                double IDF = calculate_inverse_document_frequency(Convert.ToDouble(total_docs), Convert.ToDouble(term_docs));
                //TF-IDF
                double TF_IDF = TF * IDF;
                term_tfidf[term.Key] = TF_IDF;
                done_terms.Add(term.Key);
                int n = int.Parse(numberOfCalTermstxt.Text);
                n++;
                numberOfCalTermstxt.Clear();
                numberOfCalTermstxt.AppendText(n.ToString());
                calculatedTermstxt.AppendText(term.Key + "\r\n");
            }

            //arrange the dictionary descendingly
            var t_t = from document in term_tfidf orderby document.Value descending select document;
            //store in the text file
            //create text file
            fileName = fileNametxt.Text;
            var myFile = File.Create(fileName);
            myFile.Close();
            // term:tfidf,
            foreach (var term in t_t)
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(term.Key + ":" + term.Value.ToString());
                    writer.Close();
                }
            }
            calculatedTermstxt.AppendText("Storing Done!" + "\r\n");


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
        private String extract_text(String rString)
        {
            String text = String.Empty;
            IHTMLDocument2 myDoc = new HTMLDocumentClass();
            myDoc.write(rString);
            text = myDoc.body.innerText;

            return text;
        }
        private List<string> tokenize(string page_text)
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
                        text.RemoveAt(index);
                }

            }
            return text;
        }
        private String get_term_record(String word)
        {
            String record = String.Empty;
            con.Open();
            string cmd = "SELECT document_id From dictionary WHERE term = @input";
            using (SqlCommand comm = new SqlCommand(cmd, con))
            {
                comm.Parameters.AddWithValue("@input", word);
                using (SqlDataReader oReader = comm.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        record = oReader["document_id"].ToString();
                    }
                }
            }
            con.Close();
            return record;
        }
        private int get_URL_ID(String URL)
        {
            int id= -1;
            con.Open();
            string cmd = "SELECT Id From URLData WHERE _url = @input";
            using (SqlCommand comm = new SqlCommand(cmd, con))
            {
                comm.Parameters.AddWithValue("@input", URL);
                using (SqlDataReader oReader = comm.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        id = int.Parse(oReader["Id"].ToString());
                    }
                }
            }
            con.Close();
            return id;
        }
        private int term_number_of_documents(String record)
        {
            //docID:freq:pos1,pos2,pos3;
            String[] doc_freq_pos = record.Split(';');
            return doc_freq_pos.Count();
        }
        private int frequency_of_term(String record, int doc_id)
        {
            //docID:freq:pos1,pos2,pos3;
            String[] doc_freq_pos = record.Split(';');
            for(int y =0; y<doc_freq_pos.Length;y++)
            {
                if (doc_freq_pos[y] != "")
                {
                    //docID:freq:pos1,pos2,pos3;
                    String[] doc = doc_freq_pos[y].Split(':');
                    int id = int.Parse(doc[0]);
                    int freq = int.Parse(doc[1]);
                    if(id == doc_id)
                        return freq;
                }
            }
            return -1;
        }
        private int total_number_of_documents()
        {
            //get number of records in url_data table in database
          /*  con.Open();
            SqlCommand cmd = con.CreateCommand();
            Int32 count;
            //           cmd.CommandText = "select * from url_data";
            cmd.CommandText = "select * from URLData";

            if (cmd.ExecuteScalar() != null)
                count = (Int32)cmd.ExecuteScalar();
            else
                count = 0;
            con.Close();*/
            return 3001;
        }
        private double calculate_term_frequency(double termFreq, double numOfTerms)
        {
            //freq. term in document / number of terms in document
            return termFreq / numOfTerms;
        }
        private double calculate_inverse_document_frequency(double total_docs, double term_docs)
        {
            return Math.Log(total_docs / term_docs);
        }
        
    }
}
