namespace MissionSupport.Model
{
    interface IDatabase
    {
        bool userExists(string email);

        // return login success
        bool login(string email, string password);

        // return user creation was success
        bool createUser(User user, string password);
    }
}
