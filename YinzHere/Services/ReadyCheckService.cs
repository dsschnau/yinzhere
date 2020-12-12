using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YinzHere.Models;

namespace YinzHere.Services
{
    public class ReadyCheckService : IReadyCheckService
    {
        private Dictionary<string, ReadyCheck> _readyChecks { get; set; }

        public ReadyCheckService()
        {
            _readyChecks = new Dictionary<string, ReadyCheck>();
        }

        public string NewReadyCheck()
        {
            var key = Guid.NewGuid().ToString();
            var value = new ReadyCheck()
            {
                UsersReady = new List<string>()
            };
            _readyChecks.Add(key, value);

            return key;
        }

        public ReadyCheck GetReadyCheck(string key)
        {
            if (!string.IsNullOrWhiteSpace(key) && _readyChecks.TryGetValue(key, out var readyCheck))
            {
                return readyCheck;
            }
            return null;
        }
    }
}
