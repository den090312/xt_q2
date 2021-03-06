﻿using Common;
using System.Collections.Specialized;

namespace WebPL.Models
{
    public class Product
    {
        public static NameValueCollection Forms { get; set; }

        public static string Message { get; private set; }

        static Product() => Message = string.Empty;

        public static bool Run(NameValueCollection forms)
        {
            Forms = forms;

            return TryAddProduct() || TryRemoveProduct();
        }

        private static bool TryAddProduct()
        {
            var name  = Forms["newProductName"];
            var price = Forms["newProductPrice"];

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price))
            {
                return false;
            }

            if (!decimal.TryParse(price, out decimal priceParsed))
            {
                Message = $"Ошибка. Некорректная цена - '{price}'!";

                return true;
            }

            if (Dependencies.ProductLogic.Add(name, priceParsed))
            {
                Message = "Товар добавлен";
            }
            else
            {
                Message = $"Ошибка добавления товара - '{name}', цена - '{priceParsed}'!";
            }

            return true;
        }

        private static bool TryRemoveProduct()
        {
            var productId = Forms["chosenProductIdDelete"];

            if (string.IsNullOrEmpty(productId))
            {
                Message = string.Empty;

                return false;
            }

            if (!int.TryParse(productId, out int productIdParsed))
            {
                Message = $"Ошибка. Некорректный id товара - '{productId}'!";

                return true;
            }

            if (Dependencies.ProductLogic.Remove(productIdParsed))
            {
                Message = "Товар удален";
            }
            else
            {
                Message = $"Ошибка удаления товара, id - '{productIdParsed}'!";
            }

            return true;
        }
    }
}