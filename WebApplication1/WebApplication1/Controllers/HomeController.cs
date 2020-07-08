using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Data;
using Microsoft.ApplicationBlocks.Data;

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
                // If the request is POST, get the values from the form
                var name = Request.Form["UserName"];
                var address = Request.Form["Address"];
                var gender = Request.Form["Gender"];
                var age = Int16.Parse(Request.Form["Age"]);
                DbHelper.createUser(name, address, gender, age);
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