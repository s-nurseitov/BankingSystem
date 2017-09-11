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
        public delegate List<string> Handler(string command);
        public event Handler EventHandler;
        public ATM(Handler handler)
        {
            this.EventHandler = handler;
        }

        public void HandlerCommand()
        {
            List<string> menu = EventHandler(null);
            PrintImage(menu);
            for (;;)
            {
                long pos = 0;
                string command = Console.ReadLine();
                if (Int64.TryParse(command, out pos))
                {
                    if (pos > 0 && pos <= menu.Count)
                    {
                        menu = EventHandler(menu[(int)pos - 1]);
                        PrintImage(menu);
                    }
                    else
                    {
                        menu = EventHandler(command);
                        PrintImage(menu);
                    }
                }
                if (menu == null)
                {
                    break;
                }
            }
        }

        public void PrintImage(List<string> list)
        {
            Console.Clear();
            if (list != null)
                foreach (var str in list)
                {
                    Console.WriteLine(str);
                }
        }
    }
}
