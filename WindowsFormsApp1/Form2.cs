using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void srch_btn_Click(object sender, EventArgs e)

        {
            results_list.Visible = true;
            string srch_txt = search_txtbox.Text;
           
            List <string> txt_lst = tokenize(srch_txt);
            List<string> n_txt_lst = remove_stopWords(txt_lst);

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
    }
}
