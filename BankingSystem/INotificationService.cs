﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem
{
    public interface INotificationService
    {
        bool Notify(User user, string subject, string body);
    }
}
