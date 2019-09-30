namespace Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; }

        public decimal Price { get; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
