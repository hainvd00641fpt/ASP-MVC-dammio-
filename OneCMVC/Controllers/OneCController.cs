using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneCMVC.Controllers
{
    public class OneCController : Controller
    {
        // GET: OneC
        public ActionResult  Index()
        {
            return View();
        }
        // GET: /OneC/ChaoMung/ 
        public string ChaoMung(string name, int age = 1)
        {
            return HttpUtility.HtmlEncode("Xin chao" + name + ". Tuoi cua ban la" + age);
        }

        public ActionResult Hello(string name, int numTimes = 1)
        {
            ViewBag.Message = "Hello" + name;
            ViewBag.NumTimes = numTimes;

            return View();
        } 
    }
}