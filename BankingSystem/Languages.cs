using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class Languages
    {
        List<List<string>> languageslist;
        public Languages()
        {
            languageslist = new List<List<string>>();
        }
        public List<string> GetActiveLanguage(int pos)
        {
            return languageslist[pos];
        }
    }
}
