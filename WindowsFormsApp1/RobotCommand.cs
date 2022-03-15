using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class RobotCommand
    {
        private string _Command;
        private string _Url = string.Empty;
        private string _Useragent = string.Empty;

        public RobotCommand(string commandline)
        {
            int commentPosition = commandline.IndexOf('#');
            if (commentPosition == 0)
            {
                // whole line is a comment
                _Command = "COMMENT";
            }
            else
            {
                // there is a comment on the line so remove it
                if (commentPosition >= 0)
                {
                    commandline = commandline.Substring(0, commentPosition);
                }
                // now if we have an instruction
                if (commandline.Length > 0)
                {
                    string[] lineArray = commandline.Split(':');
                    _Command = lineArray[0].Trim().ToLower();
                    if (lineArray.Length > 1)
                    {
                        // set appropriate property depending on command type
                        if (_Command == "user-agent")
                        {
                            _Useragent = lineArray[1].Trim();
                        }
                        else
                        {
                            _Url = lineArray[1].Trim();
                            // if the URL is a full URL e.g sitemaps then it will contain
                            // a : so add to URL
                            if (lineArray.Length > 2)
                            {
                                _Url += ":" + lineArray[2].Trim();
                            }
                        }
                    }
                }
            }
        }

        public string Command
        {
            get { return _Command; }
        }
        public string Url
        {
            get { return _Url; }
        }
        public string UserAgent
        {
            get { return _Useragent; }
        }

    }
}
