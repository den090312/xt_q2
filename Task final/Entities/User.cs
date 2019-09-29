namespace Entities
{
    public class User
    {
        public static User Guest { get; }

        public static User Customer { get; }

        public static User Manager { get; }

        public static User Admin { get; }

        public static User SuperAdmin { get; }

        public int Id { get; set; }

        public int IdRole { get; set; }

        public string Name { get; }

        public string PasswordHash { get; set; }

        static User()
        {
            Guest      = new User(Role.Guest);
            Customer   = new User(Role.Customer);
            Manager    = new User(Role.Manager);
            Admin      = new User(Role.Admin);
            SuperAdmin = new User(Role.SuperAdmin);
        }

        public User(Role role)
        {
            IdRole = role.Id;
            Name = role.Name;
        }

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