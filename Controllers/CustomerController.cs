using StateMgtDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace StateMgtDemo.Controllers
{
    public class CustomerController : Controller
    {
        static List<User> users = new List<User>() 
        {
        new User{UserName="Akshay",Password="Akshay@123" },
        new User{UserName="Shrini",Password="Shrini@123" },
        new User{UserName="Prakash",Password="Prakash@123" },
        };

        static List<CustomerModel> customers = new List<CustomerModel>() {
        new CustomerModel{Custid=1,CustName="Akshay Patel",City="Pune",CustomerProfile=users[0] },
        new CustomerModel{Custid=2,CustName="Shrinivas Patil",City="Pune",CustomerProfile=users[1] },
        new CustomerModel{Custid=3,CustName="Prakash Rao",City="Mumbai",CustomerProfile=users[2] },
        };


        static List<OrderDetails> orders = new List<OrderDetails>();

        static int orderid = 0;

        public ActionResult CustomerProfileData()
        {
            if (TempData["custid"] != null)
            {
                int id = Convert.ToInt32(TempData["custid"]);
                CustomerModel c1=customers.Find(c=>c.Custid==id);
                ViewBag.custid = c1.Custid;
                ViewBag.custname = c1.CustName;
                ViewBag.custcity = c1.City;
                return View();
            }
            else
            {
                HttpCookie cookie=Request.Cookies["UserDemo"];
                if (cookie != null)
                {
                    string usernameInCookie=cookie.Value;


                    string username = TempData["myValidateduser"].ToString();
                    CustomerModel model = customers.Find(c => c.CustomerProfile.UserName == username);
                    if (model == null)
                    {
                        return View("Login");
                    }
                    else
                    {
                        ViewBag.custid = model.Custid;
                        ViewBag.custname = model.CustName;
                        ViewBag.custcity = model.City;


                    }
                }
                return View();
            }
        }

        public ActionResult EditCustomerProfileData(int id)
        {
            CustomerModel model = customers.Find(c => c.Custid == id);
            return View(model);
        
        }


        [HttpPost]
        public ActionResult EditCustomerProfileData(int id,CustomerModel data)
        {
            CustomerModel c1 = null;
            if (ModelState.IsValid)
            {

                c1=customers.Find(c=>c.Custid==data.Custid);
                c1.CustName=data.CustName;
                c1.City=data.City;
            }
            TempData["custid"] = c1.Custid;
            return RedirectToAction("CustomerProfileData");
        }

        public ActionResult ShowListUsers()
        {

            ViewBag.ShowListUsers = users;

            ViewData["userslist"] = users;

            TempData["tempusers"]=users;
            return View();
        }


        // GET: Customer
        public ActionResult Index()
        {
            TempData.Peek("myValidateduser");//use the tempdata without removing it, ie it will not be marked for deletion
            string s1="";

            string uname = TempData["myValidateduser"].ToString();
            s1 = uname.ToUpper();
            TempData["myValidateduser"] = s1;
            string s = string.Concat("You are welcome ", uname);

            return RedirectToAction("ShowMyDetails");
        }


        public ActionResult ShowMyDetails()
        {
            string s=TempData["myValidateduser"].ToString();

            ViewBag.MyData = s;

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User data)
        {
            if (ModelState.IsValid)
            {
                User validatedUser=users.Find(u=>u.UserName==data.UserName && u.Password==data.Password);
                if (validatedUser != null) 
                {
                    //pass data from login action to cart action
                    TempData["myValidateduser"]=validatedUser.UserName;
                    HttpCookie cookie = new HttpCookie("UserDemo");
                    cookie.Value = validatedUser.UserName;
                    cookie.Expires = DateTime.Now.AddMinutes(10);
                    Response.Cookies.Add(cookie);


                    return RedirectToAction("CustomerProfileData");
                
                }
                else
                {
                    return View();
                }
            
                
            }

            return View(data);
        }


        public ActionResult MyOrders(int id)
        {

            Session["custid"] = id;
            return RedirectToAction("ListOfProducts", "Products");
        }

        public ActionResult Cart(int id)
        {
            ViewBag.Productid = id;
            ProductsModel productData = ProductsController.products.Find(p => p.Prodid == id);
            orderid = orderid + 1;
            if (productData != null)
            {
                orders.Add(new OrderDetails { Custid = Convert.ToInt32(Session["custid"]), Prodid = id, Orderid = orderid });
                

            }
            ViewBag.Orderid = orderid;
            return View();
        }

        [HttpPost]
        public ActionResult Cart(int id,int qty)
        {
            //custid,prodid,qty,orderid
           //TempData.Keep("myValidateUser");//keep the data for the next request
           //int custid=Convert.ToInt32(Session["custid"]);
           //string uname = TempData["myValidateduser"].ToString();
            //string s=string.Concat("You are welcome ", uname);
      OrderDetails details   =orders.Find(o => o.Prodid == id);
            details.Qty = qty;
            List<OrderDetails> orderDetails = orders.FindAll(o => o.Custid == Convert.ToInt32(Session["custid"]));
            ProductsModel modelData=ProductsController.products.Find(p => p.Prodid == id);
            int amt=modelData.Price* qty;
            double totalAmt = 0;
            foreach (var item in orderDetails)
            {
                int q=item.Qty;
                var pdata = ProductsController.products.Find(p => p.Prodid == item.Prodid);
             totalAmt=(totalAmt) + (pdata.Price* q);

            }

            ViewBag.TotalPrice = totalAmt;
            ViewBag.Productid = id;
            ViewBag.Orderid = orderid;
            return View(); 
        }


        public ActionResult ShowAll(int id)
        {
            List<OrderDetails> allOrders=orders.FindAll(c=>c.Custid==id);    
            return View(allOrders);
        }
    }
}