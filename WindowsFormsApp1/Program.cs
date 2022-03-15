using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using mshtml;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            string link;
            string Paragraph;
            string title;
            string head;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());



             List<string> HtmlTask(string htmllink)
            {
                HTMLDocument hh = new HTMLDocument();
                hh.write(htmllink);
                IHTMLElementCollection ele = hh.links;
                
                foreach (IHTMLElement e in ele)
                {
                    link = (string)e.getAttribute("href", 0);
                    Paragraph = (string)e.getAttribute("p", 0);
                    title = (string)e.getAttribute("title", 0);
                    head = (string)e.getAttribute("head", 0);

                }
                List<string> arr = new List<string>();
                arr.Add(head);
                arr.Add(title);
                arr.Add(Paragraph);
                arr.Add(link);

                return arr;
            }


        }



    }


}

