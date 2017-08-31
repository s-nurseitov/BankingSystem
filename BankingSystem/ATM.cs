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
        public event Handler EventHandler;
        public ATM(Handler handler)
        {
            this.EventHandler = handler;
        }

        public void HandlerCommand()
        {
            PrintImage(EventHandler(0));
            for (;;)
            {   
                int pos= 0;
                if (Int32.TryParse(Console.ReadLine(), out pos)){
                    PrintImage(EventHandler(pos));
                }
            }
        }

        public void PrintImage(List<string> list)
        {
            Console.Clear();
            foreach (var str in list)
            {
                Console.WriteLine(str);
            }
        }
    }
}
