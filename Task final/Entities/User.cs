namespace Entities
{
    public class User
    {
        public int Id { get; set; }

        public int IdRole { get; }

        public string Name { get; }

        public string Password { get; }

        public User(string name, Role role, string password)
        {
            Name = name;
            IdRole = role.Id;
            Password = password;
        }
    }
}