namespace MissionSupport.Model
{
    public class User
    {
        private static int currentID;

        static User()
        {
            currentID = 0;
        }

        public int ID { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public User(string email, string username, string firstName, string lastName)
        {
            ID = currentID;
            currentID++;

            Email = email;
            UserName = username;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
