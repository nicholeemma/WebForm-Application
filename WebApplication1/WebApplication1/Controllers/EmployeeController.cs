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



namespace WebApplication1.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: User
        IList<Employee> UserList = new List<Employee>{
                            new Employee() { UserId = 1, UserName = "John", Age = 18, Address ="jiangsu", Gender = "20-30" } ,
                            new Employee() { UserId = 2, UserName = "Steve",  Age = 21, Address ="jiangsu", Gender = "20-30"  } ,
                            new Employee() { UserId = 3, UserName = "Bill",  Age = 25, Address ="jiangsu", Gender = "20-30" } ,
                            new Employee() { UserId = 4, UserName = "Ram" , Age = 20, Address ="jiangsu", Gender = "20-30"  } ,
                            new Employee() { UserId = 5, UserName = "Ron" , Age = 31, Address ="jiangsu", Gender = "20-30"  } ,
                            new Employee() { UserId = 4, UserName = "Chris" , Age = 17, Address ="jiangsu", Gender = "20-30" } ,
                            new Employee() { UserId = 4, UserName = "Rob" , Age = 19, Address ="jiangsu", Gender = "20-30"  }
                        };
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
                //var id = Request.Form["id"];
                var name = Request.Form["UserName"];
                var address = Request.Form["Address"];
                var gender = Request.Form["Gender"];
                var age = Request.Form["Age"];
                var id = 1;

                // Assumes connectionString is a valid connection string.
                Console.WriteLine(ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString);
                var connectionString = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
                // string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // Do work here.  
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        // 写到日志里 nlog 一个框架   读写日志的代码也是共用的方法
                        Response.Write("<Script>alert('connected')</Script>");
                        string MyInsert = "insert into dbo.user_table(UserId, UserName, Age, Address, Gender)values('" + id + "','" + name + "','" + age + "','" + address + "','" + gender + "')";
                        SqlCommand MyCommand = new SqlCommand(MyInsert, connection);
                        MyCommand.ExecuteNonQuery();
                    }
                }

                Employee client = new Employee()
                {
                    //UserId = Convert.ToInt16(id),
                    UserName = name,
                    Address = address,
                    Age = 20,
                    Gender = gender,
                };

                var userLists = Employee.GetUsers();
                userLists.Add(client);
                created = true;
                /**
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
                var gender = Request.Form["gender"];


                // Create a new Client for these details.
                Employee client = new Employee()
                {
                    //UserId = Convert.ToInt16(id),
                    UserName = name,
                    Address = address,
                    Age = 20,
                    Gender = gender,
                };

                // Save the client in the ClientList
                var ClientFile = Employee.UserFile;
                // 用try catch final
                // 工具函数 放到一个共用的地方
                // 建一个utility 目录，和controllers平级
                var ClientData = System.IO.File.ReadAllText(ClientFile);
                List<Employee> ClientList = new List<Employee>();
                ClientList = JsonConvert.DeserializeObject<List<Employee>>(ClientData);

                if (ClientList == null)
                {
                    ClientList = new List<Employee>();
                }
                ClientList.Add(client);

                // Now save the list on the disk
                System.IO.File.WriteAllText(ClientFile, JsonConvert.SerializeObject(ClientList));
**/
                // Denote that the client was created
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

            /**
            if (HttpContext.Request.RequestType == "POST")
            {
                var name = Request.Form["UserName"];
                var address = Request.Form["address"];
                var age = Request.Form["age"];
                var gender = Request.Form["gender"];


                // Get all of the clients
                var clints = Employee.GetUsers();

            foreach (Employee client in clints)
            {
                // Find the client
                if (client.UserId== Id)
                {
                    // Client found, now update his properties and save it.
                    client.UserName = name;
                    client.Address = address;
                    client.Age = 20;
                    client.Gender = gender;
                    // Break through the loop
                    break;
                }
            }

            // Update the clients in the disk
            System.IO.File.WriteAllText(Employee.UserFile, JsonConvert.SerializeObject(clints));

            // Add the details to the View

            // mvc routing  斜杠的方式// 
         
           // Response.Redirect("~/Employee/Index?Message=User_Updated");
          
        }
  **/
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
            id = id + 1;
            string queryString =
            "DELETE FROM dbo.user_table WHERE UserId=" + id + ";";
            SqlConnection sqlconn = new SqlConnection();

            

            var connectionString = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
            // string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["db"].ConnectionString;

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
                return View();
        }
    }
}