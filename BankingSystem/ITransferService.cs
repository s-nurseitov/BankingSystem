using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public interface ITransferService
    {
        bool Transfer(Account account, Account transferaccount, double Money);
    }
}
