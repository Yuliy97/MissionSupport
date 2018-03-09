namespace MissionSupport.Model
{
    public class User
    {
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public User(string email, string username, string firstName, string lastName)
        {
            Email = email;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
