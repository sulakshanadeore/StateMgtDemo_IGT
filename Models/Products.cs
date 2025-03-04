using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateMgtDemo.Models
{
    public class ProductsModel
    {
        public int Prodid { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }


    }


    public class OrderDetails
    {
        public int Custid { get; set; }
        public int Orderid { get; set; }

        public int Prodid { get; set; }
        public int Qty { get; set; }


    }
}