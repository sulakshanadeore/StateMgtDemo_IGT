using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateMgtDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string name = "Jack";
            HttpCookie cookie = new HttpCookie("userdata");
            cookie.Value = name;
            cookie.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Add(cookie);
            //Created the cookie and redirected  to Contact Page


            return RedirectToAction("Contact");
        }

        public ActionResult About()
        {
            //Pass query string using route parameters
            ViewBag.Message = "Your application description page.";
            string name = "Jim";
            int age = 10;

            return RedirectToAction("ShowData", new { username =name,userage=age });
        }




        public ActionResult ShowData(string username,int userage)
        {
            //Access query string route parameters
            string uname = username;
            int uage = userage;


            return Content(string.Concat(uname, uage));
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            HttpCookie co = Request.Cookies["userdata"];
            string CookieData = co.Value;

            return Content(CookieData);
        }
    }
}