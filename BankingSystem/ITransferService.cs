﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public interface ITransferService
    {
        void Transfer(Account account, Account transferaccount);
    }
}
