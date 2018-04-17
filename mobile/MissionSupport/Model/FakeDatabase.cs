using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionSupport.Model
{
    class FakeDatabase : IDatabase
    {
        private Dictionary<int, User> usersByID;
        private Dictionary<string, User> usersByEmail;
        private Dictionary<string, User> usersByUsername;

        // not stored in user class since we should be hashing password
        // and sending hash to the database for authentication
        private Dictionary<int, string> passwordsByID;

        private Dictionary<int, Site> sitesByID;
        private Dictionary<string, Site> sitesByName;

        public FakeDatabase()
        {
            usersByID = new Dictionary<int, User>();
            usersByUsername = new Dictionary<string, User>();
            usersByEmail = new Dictionary<string, User>();
            passwordsByID = new Dictionary<int, string>();
            sitesByID = new Dictionary<int, Site>();
            sitesByName = new Dictionary<string, Site>();

            Task.Run(async () => await prepopulate());
        }

        private async Task prepopulate()
        {
            await addUser(new User("test", "test", "test", "test"), "test");

            await addSite(new Site("Tech Tower", "Tech Tower, Atlanta, GA 30313", "This is an old tower."));
            await addSite(new Site("CDC", "1600 Clifton Rd, Atlanta, GA 30333", "The Centers for Disease Control and Prevention."));
            await addSite(new Site("Emory", "1648 Pierce Dr NE, Atlanta, GA 30307", "The Emory University School of Medicine."));
        }

        public async Task<User> getUserByID(int id)
        {
            if (usersByID.ContainsKey(id)) {
                return usersByID[id];
            }
            return null;
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

        public async Task<Site> getSiteByID(int id)
        {
            if (sitesByID.ContainsKey(id)) {
                return sitesByID[id];
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
            if (!usersByEmail.ContainsKey(email)) {
                return false;
            }

            int id = usersByEmail[email].ID;
            return passwordsByID[id] == password;
        }

        public async Task<bool> addUser(User user, string password)
        {
            if (usersByID.ContainsKey(user.ID) || usersByEmail.ContainsKey(user.Email) || usersByUsername.ContainsKey(user.UserName)) {
                return false;
            }

            usersByID.Add(user.ID, user);
            usersByEmail.Add(user.Email, user);
            usersByUsername.Add(user.UserName, user);
            passwordsByID.Add(user.ID, password);

            return true;
        }

        public async Task<bool> addSite(Site site)
        {
            if (sitesByID.ContainsKey(site.ID) || sitesByName.ContainsKey(site.Name) || !await Site.validAddress(site.Address)) {
                return false;
            }

            sitesByID.Add(site.ID, site);
            sitesByName.Add(site.Name, site);
            return true;
        }
    }
}
