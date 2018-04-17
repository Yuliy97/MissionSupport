using System.Collections.Generic;
using System.Threading.Tasks;

namespace MissionSupport.Model
{
    public interface IDatabase
    {
        Task<User> getUserByID(int id);

        Task<User> getUserByUsername(string username);

        Task<User> getUserByEmail(string email);

        Task<Site> getSiteByID(int id);

        Task<Site> getSiteByName(string name);

        IEnumerable<Site> getSites();

        // return login success
        Task<bool> login(string email, string password);

        // return user creation success
        Task<bool> addUser(User user, string password);

        // return site creation success
        Task<bool> addSite(Site site);
    }
}
