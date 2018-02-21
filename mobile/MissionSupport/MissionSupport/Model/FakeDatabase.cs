using System.Collections.Generic;

namespace MissionSupport.Model
{
    class FakeDatabase : IDatabase
    {
        private Dictionary<string, User> users;

        // not stored in user class since we should be hashing password
        // and sending hash to the database for authentication
        private Dictionary<string, string> passwords;

        public FakeDatabase()
        {
            users = new Dictionary<string, User>();
            passwords = new Dictionary<string, string>();
        }

        public bool userExists(string email)
        {
            return users.ContainsKey(email);
        }

        public bool login(string email, string password)
        {
            return passwords.ContainsKey(email) && passwords[email] == password;
        }

        public bool createUser(User user, string password)
        {
            if (users.ContainsKey(user.Email)) {
                return false;
            }

            users.Add(user.Email, user);
            passwords.Add(user.Email, password);

            return true;
        }
    }
}
