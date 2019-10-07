using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace WebPL.Models
{
    public class Order
    {
        public static NameValueCollection Forms { get; set; }

        public static string Message { get; private set; }

        static Order() => Message = string.Empty;

        public static bool Run(NameValueCollection forms)
        {
            Forms = forms;

            return AddOrder() || CancelOrder() || RestoreOrder() || InWorkOrder() || CompleteOrder();
        }

        public static IEnumerable<Entities.Order> GetOrders()
        {
            var userType = Forms["userType"];
            var id = Forms["id"];

            if (string.IsNullOrEmpty(userType) || string.IsNullOrEmpty(id))
            {
                return Enumerable.Empty<Entities.Order>();
            }

            if (!int.TryParse(id, out int idParsed))
            {
                Message = $"Ошибка. Некорректный id - '{id}'!";

                return Enumerable.Empty<Entities.Order>();
            }

            if (userType == "customer")
            {
                Message = string.Empty;

                return Dependencies.OrderLogic.GetByIdCustomer(idParsed);
            }

            if (userType == "manager")
            {
                Message = string.Empty;

                return Dependencies.OrderLogic.GetByIdManager(idParsed);
            }

            return Enumerable.Empty<Entities.Order>();
        }

        private static bool AddOrder()
        {
            var idCustomer = Forms["customerId"];
            var idProduct  = Forms["chosenProductId"];
            var quantity   = Forms["chosenProductQuantity"];
            var adress     = Forms["orderAdress"];

            if (string.IsNullOrEmpty(idCustomer) || string.IsNullOrEmpty(idProduct) || 
                string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(adress))
            {
                return false;
            }

            return OrderAdd(idCustomer, idProduct, quantity, adress);
        }

        private static bool CancelOrder()
        {
            var id = GetFormsOrderId("orderCancelId");

            if (id <= 0)
            {
                return false;
            }

            if (Dependencies.OrderLogic.CancelOrder(id))
            {
                Message = "Заказ отменен";
            }
            else
            {
                Message = $"Ошибка отмены заказа, id - '{id}'!";
            }

            return true;
        }

        private static bool RestoreOrder()
        {
            var id = GetFormsOrderId("orderRestoreId");

            if (id <= 0)
            {
                return false;
            }

            if (Dependencies.OrderLogic.RestoreOrder(id))
            {
                Message = "Заказ восстановлен";
            }
            else
            {
                Message = $"Ошибка восстановления заказа, id - '{id}'!";
            }

            return true;
        }

        private static bool InWorkOrder()
        {
            var orderId = GetFormsOrderId("inWorkOrderId");

            if (orderId <= 0)
            {
                return false;
            }

            var manager = Dependencies.ManagerLogic.GetByUserId(Index.CurrentUser.Id);

            if (Dependencies.OrderLogic.InWorkOrder(orderId, manager.Id))
            {
                Message = "Заказ взят в работу";
            }
            else
            {
                Message = $"Ошибка взятия заказа в работу, id заказа - '{orderId}, id менеджера - '{manager.Id}'!";
            }

            return true;
        }

        private static bool CompleteOrder()
        {
            var id = GetFormsOrderId("orderCompleteId");

            if (id <= 0)
            {
                return false;
            }

            if (Dependencies.OrderLogic.CompleteOrder(id))
            {
                Message = "Заказ доставлен";
            }
            else
            {
                Message = $"Ошибка доставки заказа, id - '{id}'!";
            }

            return true;
        }

        private static int GetFormsOrderId(string elementName)
        {
            var id = Forms[elementName];

            if (string.IsNullOrEmpty(id))
            {
                return 0;
            }

            if (!int.TryParse(id, out int idParsed))
            {
                Message = $"Ошибка. Некорректный id - {id}!";

                return 0;
            }

            return idParsed;
        }

        private static bool OrderAdd(string idCustomer, string idProduct, string quantity, string adress)
        {
            if (!int.TryParse(idCustomer, out int idCustomerParsed))
            {
                Message = $"Ошибка. Некорректный id покупателя - '{idCustomer}'!";

                return true;
            }

            if (!int.TryParse(idProduct, out int idProductParsed))
            {
                Message = $"Ошибка. Некорректный id товара - '{idProduct}'!";

                return true;
            }

            if (!int.TryParse(quantity, out int quantityParsed))
            {
                Message = $"Ошибка. Некорректное количество товара - '{quantity}'!";

                return true;
            }

            return OrderAddParsed(idCustomerParsed, idProductParsed, quantityParsed, adress);
        }

        private static bool OrderAddParsed(int idCustomer, int idProduct, int quantity, string adress)
        {
            var listIdProduct = new List<int>
            {
                idProduct
            };

            var product = Dependencies.ProductLogic.GetById(idProduct);

            decimal sum = product.Price * quantity;

            var order = new Entities.Order(idCustomer, DateTime.Now, adress, listIdProduct, sum);

            if (Dependencies.OrderLogic.Add(ref order))
            {
                AddProductsToOrder(order);

                Message = "Заказ создан";
            }
            else
            {
                Message = $"Ошибка создания заказа, id покупателя - '{idCustomer}'!";
            }

            return true;
        }

        private static void AddProductsToOrder(Entities.Order order)
        {
            foreach (var productId in order.ListIdProduct)
            {
                if (!Dependencies.OrderProductLogic.Add(new OrderProduct(order.Id, productId)))
                {
                    Message = $"Ошибка. Товар id '{productId}' не был добавлен в заказ id '{order.Id}'";

                    return;
                }
            }
        }
    }
}