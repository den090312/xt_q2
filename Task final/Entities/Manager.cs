using System.Collections.Generic;

namespace Entities
{
    public class Manager
    {
        public int Id { get; set; }

        public int IdUser { get; }

        public string Name { get; }

        public List<int> ListOrderId { get; set; }

        public Rank CurrentRank { get; set; }

        public enum Rank
        {
            None = 0,
            Junior = 1,
            Middle = 2,
            Top = 3,
            General = 4
        }

        public Manager(int idUser, string name, Rank rank)
        {
            IdUser = idUser;
            Name = name;
            CurrentRank = rank;
        }
    }
}
