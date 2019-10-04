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

        public static void Run(NameValueCollection forms)
        {
            Forms = forms;

            AddProduct();
        }

        private static void AddProduct()
        {
            var name = Forms["newProductName"];
            var price = Forms["newProductPrice"];

            if (!decimal.TryParse(price, out decimal priceParsed))
            {
                return;
            }

            if (Dependencies.ProductLogic.Add(name, priceParsed))
            {
                Message = "Товар добавлен!";
            }
            else
            {
                Message = "Ошибка добавления товара!";
            }
        }
    }
}