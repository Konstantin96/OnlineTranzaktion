using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTranzaktionOperator.LIB.Modul
{
    public class Operator
    {
        public string NameOperator { get; set; }
        public double Percent { get; set; }
        public string Logotip { get; set; }
       public List<Prefix> Prefixes = new List<Prefix>();
    }
    public class Prefix
    {
        public int Pref { get; set; }
    }
}
