namespace Entities
{
    public class User
    {
        public static User Guest { get; }

        public int Id { get; set; }

        public int IdRole { get; }

        public string Name { get; }

        public string PasswordHash { get; set; }

        static User() => Guest = new User(Role.Guest);

        public User(Role role) => Name = role.Name;

        public User(int roleId, string name, string passwordHash)
        {
            IdRole = roleId;
            Name = name;
            PasswordHash = passwordHash;
        }

        public User(int id, int roleId, string name, string passwordHash)
        {
            Id = id;
            IdRole = roleId;
            Name = name;
            PasswordHash = passwordHash;
        }
    }
}