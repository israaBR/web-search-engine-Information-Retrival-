using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\faculty\web-search-engine-Information-Retrival-\WindowsFormsApp1\searchEngineDatabase.mdf;Integrated Security=True;Connect Timeout=30");
        public Form2()
        {
            InitializeComponent();
        }
        private void srch_btn_Click(object sender, EventArgs e)
        {
            string srch_txt = search_txtbox.Text;
            if (srch_txt[0].Equals('"') && srch_txt[srch_txt.Length - 1].Equals('"'))
            {
                //Exact Search
                srch_txt = srch_txt.Substring(1, srch_txt.Length - 2);  //remove double quotes
                List<string> txt_lst = tokenize(srch_txt);   //remove spaces, newlines and panctuations
                List<String> records = get_DB_records(txt_lst);    //get records of the terms from the dictionary
                Dictionary<String, List<KeyValuePair<int, String>>> terms_docs_pos = get_common_docs(txt_lst, records);  //get common documents between the terms
                Dictionary<int, int> documents_with_freq = rank_with_frequency(terms_docs_pos, txt_lst);  //rank pages with frequency

                //remove documents with ZERO frequency
                foreach (var document in documents_with_freq.ToList())
                {
                    if (document.Value == 0)
                        documents_with_freq.Remove(document.Key);
                }
                //descendingly arrage the document in dictionary
                var ranked_documents = from document in documents_with_freq orderby document.Value descending select document;

                //display URL in List View
                /*foreach(var document in ranked_documents)
                {
                    String URL = get_URL_from_database(document.Key);
                    ListViewItem item = new ListViewItem(URL);
                    listView1.Items.Add(item);
                }*/

            }
            else
            {
                //Multi Keyword Swarch
                List<string> txt_lst = tokenize(srch_txt);   //remove spaces, newlines and panctuations
                List<string> n_txt_lst = remove_stopWords(txt_lst);   //remove stop words
                List<String> records = get_DB_records(n_txt_lst);   //get records of the terms from the dictionary
                Dictionary<String, List<KeyValuePair<int, String>>> terms_docs_pos = get_common_docs(n_txt_lst, records);  //get common documents between the terms
                List<int> documents_with_rank = rank_with_distance(terms_docs_pos);  //rank pages with distance

            }


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
                        list.Add(word.ToLower());
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
            list.Remove("");
            return list;
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
        private List<String> get_DB_records(List<string> word)
        {
            List<String> records = new List<String>();
            DataTable dataTable = new DataTable();
            string cmdText = "SELECT document_id From dictionary WHERE term = @input";

            using (SqlCommand cmd = new SqlCommand(cmdText, con))
            {
                //DataSet ds = null;
                SqlDataAdapter da;
                con.Open();
                for (int i = 0; i < word.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmdText = "SELECT document_id From dictionary WHERE term = @input";
                    cmd.Parameters.AddWithValue("@input", word.ElementAt(i));
                    // ds = new DataSet();
                    da = new SqlDataAdapter(cmd);
                    int rows = da.Fill(dataTable);
                    //da.Fill(ds);
                }
                //dataGridView1.DataSource = ds;
                //dataGridView1.Update();
            }
            con.Close();
            foreach (DataRow row in dataTable.Rows)
                records.Add(row["document_id"].ToString());
            return records;
        }
        private List<int> intersection_of_lists(List<int> list1, List<int> list2)
        {
            List<int> intersection = list1.Intersect(list2).ToList();
            return intersection;
        }
        private Dictionary<String, List<KeyValuePair<int, String>>> get_common_docs(List<String> terms, List<String> records)
        {
            //Dictionary<String, List<KeyValuePair<int, String>>> initial_docs = new Dictionary<String, List<KeyValuePair<int, String>>>();
            List<int> intersection = new List<int>();
            Dictionary<String, List<KeyValuePair<int, String>>> common_docs = new Dictionary<String, List<KeyValuePair<int, String>>>();
            for (int i = 0; i < records.Count; i++)
            {
                //split the record
                //split documents
                String[] documents = records[i].Split(';');

                List<int> doc_IDs = new List<int>();
                List<KeyValuePair<int, String>> documents_positions = new List<KeyValuePair<int, string>>();
                for (int y = 0; y < documents.Length; y++)
                {
                    if (documents[y] != "")
                    {
                        //docID:freq:pos1,pos2,pos3;
                        String[] doc_freq_pos = documents[y].Split(':');
                        int doc_id = int.Parse(doc_freq_pos[0]);
                        //int freq = int.Parse(doc_freq_pos[1]);
                        String pos = doc_freq_pos[2];
                        doc_IDs.Add(doc_id);
                        KeyValuePair<int, String> doc_pos = new KeyValuePair<int, String>(doc_id, pos);
                        documents_positions.Add(doc_pos);
                    }
                    else
                        documents = documents.Where(w => w != documents[y]).ToArray();
                }
                //add term to the initial dictionary
                common_docs.Add(terms[i], documents_positions);

                //get common documents of the first i+1 terms
                if (i == 0)
                    intersection = doc_IDs.ToList();
                else
                    intersection = intersection_of_lists(doc_IDs, intersection);
            }

            //filter non common documents
            foreach (var term in common_docs)
            {
                foreach (KeyValuePair<int, String> doc_pos in term.Value.ToList())
                {
                    if (!intersection.Contains(doc_pos.Key))
                        term.Value.Remove(doc_pos);
                }
            }

            return common_docs;
        }
        private List<int> Convert_string_to_intarr(string positions)
        {
            string num = " ";
            List<int> arr = new List<int>();
            for (int i = 0; i < positions.Length; i++)
            {

                if (positions[i] == ',')
                {
                    arr.Add(int.Parse(num));
                    num = "";
                    continue;
                }
                else
                {
                    num += positions[i];
                }
            }
            arr.Add(int.Parse(num));
            return arr;
        }
        private List<int> rank_with_distance(Dictionary<string, List<KeyValuePair<int, string>>> words)
        {
            int length = 0;

            List<List<KeyValuePair<int, string>>> items = new List<List<KeyValuePair<int, string>>>();
            foreach (KeyValuePair<string, List<KeyValuePair<int, string>>> val in words)
            {
                items.Add(val.Value);
                length++;
            }
            List<int> smallestLength = new List<int>();
            for (int i = 0; i < length; i++)
            {
                List<KeyValuePair<int, string>> doc1 = new List<KeyValuePair<int, string>>();
                List<KeyValuePair<int, string>> doc2 = new List<KeyValuePair<int, string>>();
                if (i + 1 >= length)
                {
                    break;
                }
                else
                {
                    doc1 = items[i];
                    doc2 = items[i + 1];
                    int[] arr1;
                    int[] arr2;
                    foreach (var val in doc1)
                    {
                        foreach (var val2 in doc2)
                        {
                            if (val.Key.Equals(val2.Key))
                            {
                                arr1 = new int[val.Value.Length];
                                arr2 = new int[val2.Value.Length];
                                int j = 0;
                                int jj = 0;
                                foreach (var num in val.Value.Split(','))
                                {
                                    arr1[j] = int.Parse(num);
                                    j++;
                                }

                                foreach (var num in val2.Value.Split(','))
                                {
                                    arr2[jj] = int.Parse(num);
                                    jj++;
                                }
                                int small = 0;
                                int first = 0;
                                for (int k = 0; k < j; k++)
                                {
                                    if (arr1[0] < arr2[k])
                                    {
                                        small = arr2[k] - arr1[0];
                                        first = k;
                                        break;
                                    }
                                }

                                for (int bm = 1; bm < j; bm++)
                                {
                                    for (int bmm = first; bmm < jj; bmm++)
                                    {
                                        if (arr1[bm] < arr2[bmm])
                                        {
                                            if (small > arr2[bmm] - arr1[bm])
                                            {
                                                small = arr2[bmm] - arr1[bm];
                                                break;
                                            }
                                        }
                                    }
                                }
                                smallestLength.Add(small);
                                small = 0;

                            }
                        }
                    }
                }
            }
            int len = smallestLength.Count / (length - 1);

            List<int> finalLength = new List<int>();
            int len1 = len;
            for (int i = 0; i < len; i++)
            {
                finalLength.Add(smallestLength[i] + smallestLength[len1]);
                len1++;
            }
            return finalLength;

        }
        private Dictionary<int, int> rank_with_frequency(Dictionary<string, List<KeyValuePair<int, string>>> dectionary, List<string> words)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int m = 0; m < dectionary[words[0]].Count; m++)
            {
                bool find = false;
                int count = 0;
                List<int> index_found = new List<int>();
                List<int> positions_of_word1 = Convert_string_to_intarr(dectionary[words[0]][m].Value);
                List<int> positions_of_word2 = Convert_string_to_intarr(dectionary[words[1]][m].Value);
                while (count < positions_of_word2.Count - 1)
                {
                    for (int z = 0; z < positions_of_word1.Count; z++)
                    {
                        if (positions_of_word2[count] <= positions_of_word1[z])
                        {
                            continue;
                        }
                        else if (positions_of_word2[count] - positions_of_word1[z] == 1)
                        {
                            index_found.Add(positions_of_word2[count]);
                        }

                    }
                    count++;
                }

                if (dectionary.Count > 2)
                {
                    count = 0;
                    for (int i = 2; i < dectionary.Count; i++)
                    {
                        List<int> positions_of_current_word = Convert_string_to_intarr(dectionary[words[i]][m].Value);
                        for (int j = 0; j < index_found.Count; j++)
                        {
                            while (count < positions_of_current_word.Count - 1)
                            {
                                if (positions_of_current_word[count] <= index_found[j])
                                {
                                    count++;
                                    continue;
                                }
                                else if (positions_of_current_word[count] - index_found[j] == 1)
                                {
                                    find = true;
                                }
                                count++;
                            }
                            if (!find)
                            {
                                index_found.RemoveAt(j);
                            }
                        }
                    }
                }
                dic[dectionary[words[0]][m].Key] = index_found.Count;
            }
            return dic;
        }
        private String get_URL_from_database(int doc_id)
        {
            String URL = String.Empty;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select _url from URLData where Id = '" + doc_id + "'";

            if (cmd.ExecuteScalar() != null)
                URL = (string)cmd.ExecuteScalar();
            con.Close();
            return URL;

        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void search_txtbox_TextChanged(object sender, EventArgs e)
        {

        }
        private void results_list_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
