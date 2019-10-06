namespace Entities
{
    public class OrderProduct
    {
        public int IdOrder { get; }

        public int IdProduct { get; }

        public OrderProduct(int idOrder, int idProduct)
        {
            IdOrder = idOrder;
            IdProduct = idProduct;
        }
    }
}