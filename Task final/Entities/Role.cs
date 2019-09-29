namespace Entities
{
    public class Role
    {
        public static Role Guest { get; }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool ProductRead { get; set; }

        public bool ProductWrite { get; set; }

        public bool OrderRead { get; set; }

        public bool OrderWrite { get; set; }

        public bool RoleRead { get; set; }

        public bool RoleWrite { get; set; }

        public bool UserRead { get; set; }

        public bool UserWrite { get; set; }

        static Role() => Guest = new Role("Guest");

        public Role(string name)
        {
            Name = name;
        }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
