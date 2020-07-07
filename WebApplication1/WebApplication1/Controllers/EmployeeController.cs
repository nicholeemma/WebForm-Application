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

                Console.WriteLine(ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString);
                var connectionString = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
                string MyInsert = "insert into dbo.user_table(UserName, Age, Address, Gender)values('" + name + "','" + age + "','" + address + "','" + gender + "');" + "SELECT @@IDENTITY;";

                var id = SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, MyInsert);
                System.Diagnostics.Debug.WriteLine(id);

                Employee client = new Employee(id, name, address, age, gender);

                var userLists = Employee.GetUsers();
                userLists.Add(client);
                created = true;
               
                created = true;
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

        // 引用到的地方都要释放， 释放的代码放到另外的地方
        
        public ActionResult Edit(int Id)
        {
            //Get the student from studentList sample collection for demo purpose.
            //Get the student from the database in the real application
            var std = UserList.Where(s => s.UserId == Id).FirstOrDefault();
            Delete(Id);
            Create();

            
            return View(std);
        }

        public ActionResult Delete(int id)
        {
            /**
            // Get the clients
            var Clients = Employee.GetUsers();
            var deleted = false;
            // Delete the specific one.
            foreach (Employee client in Clients)
            {
                // Found the client
                if (client.UserId == id)
                {
                    // delete this client
                    var index = Clients.IndexOf(client);
                    Clients.RemoveAt(index);

                    // Removed now save the data back.
                    System.IO.File.WriteAllText(Employee.UserFile, JsonConvert.SerializeObject(Clients));
                    deleted = true;
                    break;
                }
            }

            // Add the process details to the ViewBag
            if (deleted)
            {
                ViewBag.Message = "Client was deleted successfully.";
            }
            else
            {
                ViewBag.Message = "There was an error while deleting the client.";
            }
            **/
            // id = id + 1;
            System.Diagnostics.Debug.WriteLine(id);
            string queryString =
            "DELETE FROM dbo.user_table WHERE UserId=" + id + ";";
            //SqlConnection sqlconn = new SqlConnection();

            

           
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
           // string MyInsert = "insert into dbo.user_table(UserName, Age, Address, Gender)values('" + name + "','" + age + "','" + address + "','" + gender + "')";

            SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, queryString);

            /**
            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();
                int r = command.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine(id);
                System.Diagnostics.Debug.WriteLine("成功删除了{0}行数据", r);
                     
                
                //SqlDataReader reader = command.ExecuteNonReader();
            }
            **/
                return View();
        }
    }
}