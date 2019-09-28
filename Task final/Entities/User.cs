namespace Entities
{
    public class User
    {
        public int UserId { get; set; }

        public int IdRole { get; }

        public string Name { get; }

        public string Password { get; }

        public User(string name, int idRole, string password)
        {
            Name = name;
            IdRole = idRole;
            Password = password;
        }
    }
}