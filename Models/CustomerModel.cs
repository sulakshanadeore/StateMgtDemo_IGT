using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StateMgtDemo.Models
{
    public class CustomerModel
    {
        public int Custid { get; set; }
        public string CustName { get; set; }
        public string City { get; set; }

        public User CustomerProfile { get; set; }
    }
}