using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductDao productDao;

        public ProductLogic(IProductDao iProductDao)
        {
            NullCheck(iProductDao);

            productDao = iProductDao;
        }

        public bool Add(string name, decimal price)
        {
            NullCheck(name);
            EmptyStringCheck(name);
            PriceCheck(price);

            var product = new Product(name, price);

            return productDao.Add(ref product);
        }

        public bool Add(ref Product product)
        {
            NullCheck(product);
            EmptyStringCheck(product.Name);
            PriceCheck(product.Price);

            return productDao.Add(ref product);
        }

        public bool NoProducts() => productDao.NoProducts();

        private void PriceCheck(decimal price)
        {
            if (price <= 0)
            {
                throw new ArgumentException($"{nameof(price)} must be greater than zero!");
            }
        }

        private void EmptyStringCheck(string inputString)
        {
            if (inputString == string.Empty)
            {
                throw new ArgumentException($"{nameof(inputString)} is empty!");
            }
        }

        private void NullCheck<T>(T classObject) where T : class
        {
            if (classObject is null)
            {
                throw new ArgumentNullException($"{nameof(classObject)} is null!");
            }
        }
    }
}
