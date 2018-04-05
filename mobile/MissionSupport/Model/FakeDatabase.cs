using System.Collections.Generic;

namespace MissionSupport.Model
{
    class FakeDatabase : IDatabase
    {
        private Dictionary<string, User> usersByEmail;
        private Dictionary<string, User> usersByUsername;

        // not stored in user class since we should be hashing password
        // and sending hash to the database for authentication
        private Dictionary<string, string> passwordsByEmail;

        private Dictionary<string, Site> sitesByName;

        public FakeDatabase()
        {
            usersByUsername = new Dictionary<string, User>();
            usersByEmail = new Dictionary<string, User>();
            passwordsByEmail = new Dictionary<string, string>();
            sitesByName = new Dictionary<string, Site>();
        }

        public User getUserByUsername(string username)
        {
            if (usersByUsername.ContainsKey(username)) {
                return usersByUsername[username];
            }
            return null;
        }

        public User getUserByEmail(string email)
        {
            if (usersByEmail.ContainsKey(email)) {
                return usersByEmail[email];
            }
            return null;
        }

        public Site getSiteByName(string name)
        {
            if (sitesByName.ContainsKey(name)) {
                return sitesByName[name];
            }
            return null;
        }

        public bool login(string email, string password)
        {
            return passwordsByEmail.ContainsKey(email) && passwordsByEmail[email] == password;
        }

        public bool addUser(User user, string password)
        {
            if (usersByEmail.ContainsKey(user.Email) || usersByUsername.ContainsKey(user.UserName)) {
                return false;
            }

            usersByEmail.Add(user.Email, user);
            usersByUsername.Add(user.UserName, user);
            passwordsByEmail.Add(user.Email, password);

            return true;
        }

        public bool addSite(Site site)
        {
            if (sitesByName.ContainsKey(site.Name)) {
                return false;
            }

            sitesByName.Add(site.Name, site);

            return true;
        }
    }
}
