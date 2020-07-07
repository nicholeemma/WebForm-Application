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
            var created = false;
            // Create the Client
            if (HttpContext.Request.RequestType == "POST")
            {
                ViewBag.Submitted = true;
                // If the request is POST, get the values from the form
                //var id = Request.Form["id"];
                var name = Request.Form["UserName"];
                var address = Request.Form["Address"];
                var gender = Request.Form["Gender"];
                var age = Int16.Parse(Request.Form["Age"]);
                
                
                // Assumes connectionString is a valid connection string.
                Console.WriteLine(ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString);
                var connectionString = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
                string MyInsert = "insert into dbo.user_table(UserName, Age, Address, Gender)values('" +  name + "','" + age + "','" + address + "','" + gender + "');" + "SELECT @@IDENTITY;";

                var id = SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, MyInsert);
                System.Diagnostics.Debug.WriteLine(id);

                Employee client = new Employee(id, name, address, age, gender);
                /**
                {
                    
                    UserName = name,
                    Address = address,
                    Age = Int16.Parse(age),
                    Gender = gender,
                };**/

                var userLists = Employee.GetUsers();
               //  userLists.Add(client);
                
                created = true;
                Response.Redirect("/employee");

            }
            /**
            if (created)
            {
                ViewBag.Message = "Client was created successfully.";
            }
            else
            {
                ViewBag.Message = "There was an error while creating the client.";
            }**/

            
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