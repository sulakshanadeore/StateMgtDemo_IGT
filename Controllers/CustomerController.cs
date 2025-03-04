using StateMgtDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

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

                    return RedirectToAction("CustomerProfileData");
                
                }
                else
                {
                    return View();
                }
            
                
            }

            return View(data);
        }
        public ActionResult Cart()
        {
            TempData.Keep("myValidateUser");//keep the data for the next request
            string uname = TempData["myValidateduser"].ToString();

            string s=string.Concat("You are welcome ", uname);

            return RedirectToAction("Index");
        }
    }
}