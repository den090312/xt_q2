using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace WebPL.Models
{
    public class Order
    {
        public static NameValueCollection Forms { get; set; }

        public static string Message { get; private set; }

        public static void Run(NameValueCollection forms)
        {
            Forms = forms;

            if (AddOrder() || CancelOrder() || RestoreOrder() || InWorkOrder() || CompleteOrder())
            {
                return;
            }
        }

        public static IEnumerable<Entities.Order> GetOrders()
        {
            var userType = Forms["userType"];
            var id = Forms["id"];

            if (userType == null || id == null || userType == string.Empty || id == string.Empty)
            {
                return Enumerable.Empty<Entities.Order>();
            }

            if (!int.TryParse(id, out int idParsed))
            {
                Message = $"Некорректный id - '{id}'!";

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

        public static IEnumerable<Entities.Order> GetNewOrders() => Dependencies.OrderLogic.GetNewOrders();

        private static bool AddOrder()
        {
            var idCustomer = Forms["customerId"];
            var idProduct = Forms["chosenProductId"];
            var quantity = Forms["chosenProductQuantity"];
            var adress = Forms["orderAdress"];

            if (idProduct == null || quantity == null || adress == null || idCustomer == null)
            {
                return false;
            }

            if (idProduct == string.Empty || quantity == string.Empty || adress == string.Empty || idCustomer == string.Empty)
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

                return true;
            }
            else
            {
                Message = "Ошибка отмены заказа!";

                return false;
            }
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

                return true;
            }
            else
            {
                Message = "Ошибка восстановления заказа!";

                return false;
            }
        }

        private static bool InWorkOrder()
        {
            var orderId = GetFormsOrderId("inWorkOrderId");

            if (orderId <= 0)
            {
                return false;
            }

            var idManager = Dependencies.ManagerLogic.GetByUserId(Index.CurrentUser.Id);

            if (Dependencies.OrderLogic.InWorkOrder(orderId, idManager.Id))
            {
                Message = "Заказ взят в работу";

                return true;
            }
            else
            {
                Message = "Ошибка взятия заказа в работу!";

                return false;
            }
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

                return true;
            }
            else
            {
                Message = "Ошибка доставки заказа!";

                return false;
            }
        }

        private static int GetFormsOrderId(string elementName)
        {
            var id = Forms[elementName];

            if (id == null || id == string.Empty)
            {
                return 0;
            }

            if (!int.TryParse(id, out int idParsed))
            {
                Message = $"Некорректный id - {id}!";

                return 0;
            }

            return idParsed;
        }

        private static bool OrderAdd(string idCustomer, string idProduct, string quantity, string adress)
        {
            if (!int.TryParse(idCustomer, out int idCustomerParsed))
            {
                Message = $"Некорректный idCustomer - '{idCustomer}'!";

                return false;
            }

            if (!int.TryParse(idProduct, out int idProductParsed))
            {
                Message = $"Некорректный idProduct - '{idProduct}'!";

                return false;
            }

            if (!int.TryParse(quantity, out int quantityParsed))
            {
                Message = $"Некорректный quantity - '{quantity}'!";

                return false;
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

                return true;
            }
            else
            {
                Message = "Ошибка создания заказа!";

                return false;
            }
        }

        private static void AddProductsToOrder(Entities.Order order)
        {
            foreach (var productId in order.ListIdProduct)
            {
                if (!Dependencies.OrderProductLogic.Add(new OrderProduct(order.Id, productId)))
                {
                    Message = $"Товар id '{productId}' не был добавлен в заказ id '{order.Id}'";

                    return;
                }
            }
        }
    }
}