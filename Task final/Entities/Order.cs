﻿using System;
using System.Collections.Generic;

namespace Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int IdCustomer { get; }

        public int IdManager { get; set; }

        public DateTime Date { get; }

        public string Adress { get; }

        public List<int> ListIdProduct { get; set; }

        public decimal Sum { get; }

        public Status CurrentStatus { get; set; }

        public enum Status
        {
            None = 0,
            Opened = 1,
            Processed = 2,
            Completed = 3,
            Canceled = 4,
            Returned = 5,
            Closed = 6
        }

        public Order(int idCustomer, DateTime date, string adress, List<int> listIdProduct, decimal sum)
        {
            IdCustomer = idCustomer;
            Date = date;
            Adress = adress;
            ListIdProduct = listIdProduct;
            Sum = sum;
            CurrentStatus = Status.Opened;
        }

        public Order(int idCustomer, DateTime date, string adress, List<int> listIdProduct, decimal sum, Status status)
        {
            IdCustomer = idCustomer;
            Date = date;
            Adress = adress;
            ListIdProduct = listIdProduct;
            Sum = sum;
            CurrentStatus = status;
        }
    }
}