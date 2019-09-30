using Entities;
using InterfacesBLL;
using InterfacesDAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class Product : IProductLogic
    {
        private readonly IProductDao productDao;

        public Product(IProductDao iProductDao)
        {
            NullCheck(iProductDao);

            productDao = iProductDao;
        }

        public bool NoProducts() => productDao.NoProducts();

        private void PriceCheck(double price)
        {
            if (price <= 0)
            {
                throw new ArgumentException($"{nameof(price)} must be greater than zero!");
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
