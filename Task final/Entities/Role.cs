namespace Entities
{
    public class Role
    {
        public static Role Guest { get; }

        public static Role Customer { get; }

        public static Role Manager { get; }

        public static Role Admin { get; }

        public static Role SuperAdmin { get; }

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

        static Role()
        {
            Guest = new Role("Guest")
            {
                ProductRead = true
            };


            Customer = new Role("Customer")
            {
                OrderRead = true,
                OrderWrite = true,
                ProductRead = true
            };

            Manager = new Role("Manager")
            {
                OrderRead = true,
                OrderWrite = true,
                ProductRead = true,
                ProductWrite = true
            };

            Admin = new Role("Admin")
            {
                RoleRead = true,
                RoleWrite = true,
                UserRead = true,
                UserWrite = true
            };

            SuperAdmin = new Role("SuperAdmin")
            {
                OrderRead = true,
                OrderWrite = true,
                ProductRead = true,
                ProductWrite = true,
                RoleRead = true,
                RoleWrite = true,
                UserRead = true,
                UserWrite = true
            };
        }

        public Role(string name) => Name = name;

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
