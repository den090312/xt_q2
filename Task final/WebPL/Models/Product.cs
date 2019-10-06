using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace WebPL.Models
{
    public class Product
    {
        public static NameValueCollection Forms { get; set; }

        public static string Message { get; private set; }

        static Product() => Message = string.Empty;

        public static void Run(NameValueCollection forms)
        {
            Forms = forms;

            if (AddProduct() || RemoveProduct())
            {
                return;
            } 
        }

        private static bool AddProduct()
        {
            var name  = Forms["newProductName"];
            var price = Forms["newProductPrice"];

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price))
            {
                return false;
            }

            if (!decimal.TryParse(price, out decimal priceParsed))
            {
                return false;
            }

            if (Dependencies.ProductLogic.Add(name, priceParsed))
            {
                Message = "Товар добавлен";

                return true;
            }
            else
            {
                Message = "Ошибка добавления товара!";

                return false;
            }
        }

        private static bool RemoveProduct()
        {
            var productId = Forms["chosenProductIdDelete"];

            if (string.IsNullOrEmpty(productId))
            {
                Message = string.Empty;

                return false;
            }

            if (!int.TryParse(productId, out int productIdParsed))
            {
                Message = "Некорректный id товара!";

                return false;
            }

            if (Dependencies.ProductLogic.Remove(productIdParsed))
            {
                Message = "Товар удален";

                return true;
            }
            else
            {
                Message = "Ошибка удаления товара!";

                return false;
            }
        }
    }
}