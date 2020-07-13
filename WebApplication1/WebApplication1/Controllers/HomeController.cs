using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Configuration;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   
            ViewBag.Submitted = false;
            // Create the Client
            if (HttpContext.Request.RequestType == "POST")
            {
                ViewBag.Submitted = true;
               
                var name = Request.Form["UserName"];
                var address = Request.Form["Address"];
                var gender = Request.Form["Gender"];
                var age = Int16.Parse(Request.Form["Age"]);

                //TODO 调用web api
                //  DbHelper.createUser(name, address, gender, age);

                ApiHelper.createUser(name, address, gender, age);
               
                Response.Redirect("/employee");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}