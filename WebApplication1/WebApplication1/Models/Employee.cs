using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

using System.Web.Mvc;
using WebApplication1.Models;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;


namespace WebApplication1.Models
{
    // 清理 只放 字段
    public class Employee
    {
        // production环境联数据库， App_Data 可以放Access，SQL Server类似的一个，XML
        public static string UserFile = HttpContext.Current.Server.MapPath("~/App_Data/Users.json");

        public int UserId { get; set; }
        //[Display(Name = "Name")]
        public string UserName { get; set; }
        [Range(5,50)]
        public int Age { get; set; }
        //nvarchar 校验长度不一样 中文/英文
        public String Address { get; set; }
        public String Gender { get; set; }

        public static List<Employee> GetUsers()
        {
            List<Employee> Users = new List<Employee>();


            string queryString =
            "SELECT UserName, Age, Address, Gender FROM dbo.user_table;";
            SqlConnection sqlconn = new SqlConnection();
            var connectionString = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
            // string connectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection =
                       new SqlConnection(connectionString))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                if (reader.HasRows)
                {
                    String content = "";
                    
                    while (reader.Read())
                    {
                        content =
                            "{\"UserId\":0" + "," + 
                            "\"UserName\":"+ "\"" + String.Format("{0}",reader[0]) + "\"" + "," +
                                    "\"Age\":" + String.Format("{0}", reader[1]) + "," +
                            "\"Address\":" + "\"" + String.Format("{0}", reader[2]) + "\"" + "," +
                            "\"Gender\":" + "\"" + String.Format("{0}", reader[3]) + "\"" + "}";
                        var json = JsonConvert.SerializeObject(content);
                        System.Diagnostics.Debug.WriteLine(content);
                        
                        Users.Add(JsonConvert.DeserializeObject<Employee>(content));
                        
                        
                    }
                   // Users = JsonConvert.DeserializeObject<List<Employee>>(content);
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            

            /**
            if (File.Exists(UserFile))
            {
                // File exists..
                string content = File.ReadAllText(UserFile);
                // Deserialize the objects 
                Users = JsonConvert.DeserializeObject<List<Employee>>(content);

                // Returns the clients, either empty list or containing the Client(s).
                return Users;
            }
            else
            {
                // Create the file 
                File.Create(UserFile).Close();
                // Write data to it; [] means an array, 
                // List<Client> would throw error if [] is not wrapping text
                File.WriteAllText(UserFile, "[]");

                // Re run the function
                GetUsers();
            }
            **/
            return Users;
        }
    }
}