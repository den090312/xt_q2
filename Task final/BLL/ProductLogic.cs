using Entities;
using InterfacesBLL;
using InterfacesDAL;
using log4net;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class ProductLogic : IProductLogic, ILoggerLogic
    {
        private readonly IProductDao productDao;

        private readonly ILoggerDao loggerDao;

        public ILog Log => loggerDao.Log;

        public ProductLogic(IProductDao iProductDao, ILoggerDao iLoggerDao)
        {
            NullCheck(iProductDao);
            NullCheck(iLoggerDao);

            productDao = iProductDao;
            loggerDao = iLoggerDao;
        }

        public void InitLogger() => loggerDao.InitLogger();

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

        public IEnumerable<Product> GetAll() => productDao.GetAll();

        public Product GetById(int idProduct)
        {
            IdCheck(idProduct);

            return productDao.GetById(idProduct);
        }

        public bool Remove(int id)
        {
            IdCheck(id);

            return productDao.Remove(id);
        }

        private void IdCheck(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} is incorrect!");
            }
        }

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
