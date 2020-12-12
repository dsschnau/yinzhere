using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YinzHere.Models
{
    public class ReadyCheck
    {
        public List<string> UsersReady { get; set; }
        public event Action OnChange;

        public void AddUser(string name)
        {
            UsersReady.Add(name);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
