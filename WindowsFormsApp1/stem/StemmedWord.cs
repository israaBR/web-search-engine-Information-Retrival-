using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public struct StemmedWord
    {
        public readonly string Value;

        public readonly string Unstemmed;

        public StemmedWord(string value, string unstemmed)
        {
            Value = value;
            Unstemmed = unstemmed;
        }
    }
}
