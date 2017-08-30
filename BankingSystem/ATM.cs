using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public class ATM
    {
        public long Id { get; set; }
        public delegate List<string> Handler(int command);
        public Handler handler;
        public ATM(Handler handler)
        {
            this.handler = handler;
        }

        public void HandlerCommand()
        {
            for (;;)
            {
                int pos= 0;
                if (Int32.TryParse(Console.ReadLine(), out pos)){
                    PrintImage(handler(pos));
                }
            }
        }

        public void PrintImage(List<string> list)
        {
            Console.Clear();
            foreach (string str in list)
            {
                Console.WriteLine(str);
            }
        }
    }
}
