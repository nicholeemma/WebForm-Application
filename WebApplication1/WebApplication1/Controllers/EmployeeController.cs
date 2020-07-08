using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using Microsoft.ApplicationBlocks.Data;



namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: User
        IList<Employee> UserList = new List<Employee>();
        public ActionResult Index()
        {
            // GET: User
            //不能使用user 会和其他的包起冲突
            // Get the students from the database in the real application
            var userLists = Employee.GetUsers();

            return View(userLists);


        }
        private static void ReadSingleRow(IDataRecord record)
        {
            Console.WriteLine(String.Format("{0}", record));
        }
        public ActionResult Create()
        {

            ViewBag.Submitted = false;
            var created = false;
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
                
            }

            if (created)
            {
                ViewBag.Message = "Client was created successfully.";
            }
            else
            {
                ViewBag.Message = "There was an error while creating the client.";
            }
            return View();
        }
        
        public ActionResult Edit(int Id)
        {
            
            var std = UserList.Where(s => s.UserId == Id).FirstOrDefault();

            if (HttpContext.Request.RequestType == "POST")
            {
                ViewBag.Submitted = true;
                // If the request is POST, get the values from the form
                var name = Request.Form["UserName"];
                var address = Request.Form["Address"];
                var gender = Request.Form["Gender"];
                var age = Int16.Parse(Request.Form["Age"]);

                DbHelper.editUser(name, address, gender, age, Id);

            }
                return View(std);
        }

        public ActionResult Delete(int id)
        {
            DbHelper.deleteUser(id);
            return View();
        }
    }
}