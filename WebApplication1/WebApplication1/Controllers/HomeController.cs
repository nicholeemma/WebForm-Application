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

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {   //*** 联数据库
          //  SqlConnection sqlconn = new SqlConnection();
            // 一定是从配置文件读的 connectionstring
            // Data source: server name （localhost）数据库服务器的名字
            // Initial catalog： 数据库名字
            // SSPi 凡是能登录机器 就可以连数据库
            // 添加新的账户 如何添加sa账户
           // sqlconn.ConnectionString = "Data Source=DESKTOP-VL3NCF2;Initial Catalog=user;Integrated Security=SSPI;";
            // sqlconn.Open();
            /** if (sqlconn.State == System.Data.ConnectionState.Open)
             {
                 // 写到日志里 nlog 一个框架   读写日志的代码也是共用的方法
                 Response.Write("<Script>alert('connected')</Script>");
             } **/
            //mvc  code first 不给用 要用code改表结构不方便  一般来说用脚本
            // 如何读写数据库 select * ... 
            // 公司的做法：*** stored procedure (SP), sql写到C#代码（不推荐）
            // sql command

            

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
                String connectionString = "Data Source=DESKTOP-VL3NCF2;Initial Catalog=user;Integrated Security=SSPI;";
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
                    }
                }

                /**

                try//异常处理
                {
                    connection.Open();
                    MyCommand.ExecuteNonQuery();
                    sqlconn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} Exception caught.", ex);
                }
                **/
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