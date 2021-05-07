using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace YinzHere.Models
{
    public class ReadyCheck
    {
        public ReadyCheck()
        {
            UsersReady = new List<User>();
        }
        public List<User> UsersReady { get; set; }
        public event Action OnChange;

        public void AddUser(string name)
        {
            UsersReady.Add(new User(name));
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }

    public class User
    {
        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
        public Guid Id { get; }
        public string Name { get; set; }
    }
}
