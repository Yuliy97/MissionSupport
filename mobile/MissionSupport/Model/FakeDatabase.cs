using System.Collections.Generic;
using System.Threading.Tasks;

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

            Task.Run(async () => await prepopulate());
        }

        private async Task prepopulate()
        {
            await addUser(new User("test", "test", "test", "test"), "test");

            await addSite(new Site("Tech Tower", "Tech Tower, Atlanta, GA 30313"));
            await addSite(new Site("CDC", "1600 Clifton Rd, Atlanta, GA 30333"));
            await addSite(new Site("Emory", "1648 Pierce Dr NE, Atlanta, GA 30307"));
        }

        public async Task<User> getUserByUsername(string username)
        {
            if (usersByUsername.ContainsKey(username)) {
                return usersByUsername[username];
            }
            return null;
        }

        public async Task<User> getUserByEmail(string email)
        {
            if (usersByEmail.ContainsKey(email)) {
                return usersByEmail[email];
            }
            return null;
        }

        public async Task<Site> getSiteByName(string name)
        {
            if (sitesByName.ContainsKey(name)) {
                return sitesByName[name];
            }
            return null;
        }

        public IEnumerable<Site> getSites()
        {
            foreach (Site site in sitesByName.Values) {
                yield return site;
            }
        }

        public async Task<bool> login(string email, string password)
        {
            return passwordsByEmail.ContainsKey(email) && passwordsByEmail[email] == password;
        }

        public async Task<bool> addUser(User user, string password)
        {
            if (usersByEmail.ContainsKey(user.Email) || usersByUsername.ContainsKey(user.UserName)) {
                return false;
            }

            usersByEmail.Add(user.Email, user);
            usersByUsername.Add(user.UserName, user);
            passwordsByEmail.Add(user.Email, password);

            return true;
        }

        public async Task<bool> addSite(Site site)
        {
            if (sitesByName.ContainsKey(site.Name) || !await Site.validAddress(site.Address)) {
                return false;
            }

            sitesByName.Add(site.Name, site);
            return true;
        }
    }
}
