namespace Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; }

        public double Price { get; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }
}
