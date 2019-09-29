using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPL.Models
{
    public class Index
    {
        public static User CurrentUser { get; set; }

        static Index() => CurrentUser = User.Guest;
    }
}