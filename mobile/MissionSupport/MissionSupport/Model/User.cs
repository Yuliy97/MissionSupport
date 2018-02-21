using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionSupport.Model
{
    class User
    {
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public User(string email, string firstName, string lastName)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
