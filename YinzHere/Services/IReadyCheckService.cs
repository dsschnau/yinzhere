using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YinzHere.Models;

namespace YinzHere.Services
{
    public interface IReadyCheckService
    {
        public string NewReadyCheck();
        public ReadyCheck GetReadyCheck(string key);
    }
}
