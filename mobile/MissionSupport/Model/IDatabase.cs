namespace MissionSupport.Model
{
    public interface IDatabase
    {
        User getUserByUsername(string username);

        User getUserByEmail(string email);

        Site getSiteByName(string name);

        // return login success
        bool login(string email, string password);

        // return user creation success
        bool addUser(User user, string password);

        // return site creation success
        bool addSite(Site site);
    }
}
