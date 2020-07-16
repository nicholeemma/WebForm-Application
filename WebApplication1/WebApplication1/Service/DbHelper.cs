using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebApplication1.Models;
using System.Data.SqlClient;
using ApiEmployee.DataAccess;

using System.IO;
using System.Configuration;
using System.Data;


using System.Net.Sockets;

namespace CommonLibrary
{
    public class DbHelper
    {
        public static String connStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
        public static void createUser(string name, string address, string gender, int age)
        {
            // Assumes connectionString is a valid connection string.
            //string MyInsert = "insert into dbo.user_table(UserName, Age, Address, Gender)values('" + name + "','" + age + "','" + address + "','" + gender + "');" + "SELECT @@IDENTITY;";

            SqlParameter[] commandParameters = new SqlParameter[4];
            commandParameters[0] = new SqlParameter("@UserName", name);
            commandParameters[1] = new SqlParameter("@Age", age);
            commandParameters[2] = new SqlParameter("@Address", address);
            commandParameters[3] = new SqlParameter("@Gender", gender);
            var id = SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_createemployee", commandParameters);
            System.Diagnostics.Debug.WriteLine(id);

        }

        public static void editUser(string name, string address, string gender, int age, int id)
        {
            SqlParameter[] commandParameters = new SqlParameter[5];
            commandParameters[0] = new SqlParameter("@UserName", name);
            commandParameters[1] = new SqlParameter("@Age", age);
            commandParameters[2] = new SqlParameter("@Address", address);
            commandParameters[3] = new SqlParameter("@Gender", gender);
            commandParameters[4] = new SqlParameter("@UserId", id);
            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_editemployee", commandParameters);
        }

        public static void deleteUser(int id)
        {
            SqlParameter commandParameter = new SqlParameter("@UserId", id);
            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_deleteemployee", commandParameter);

        }

        public static void selectUser(List<Employee> Users)
        {
            string queryString = "SELECT UserId, UserName, Age, Address, Gender FROM dbo.user_table;";
            SqlConnection sqlconn = new SqlConnection();
            sqlconn.ConnectionString = connStr;

            SqlDataReader reader = SqlHelper.ExecuteReader(sqlconn, CommandType.Text, queryString);

            if (reader.HasRows)
            {
                String content = "";

                while (reader.Read())
                {
                    content =
                        "{\"UserId\":" + String.Format("{0}", reader[0]) + "," +
                        "\"UserName\":" + "\"" + String.Format("{0}", reader[1]) + "\"" + "," +
                                "\"Age\":" + String.Format("{0}", reader[2]) + "," +
                        "\"Address\":" + "\"" + String.Format("{0}", reader[3]) + "\"" + "," +
                        "\"Gender\":" + "\"" + String.Format("{0}", reader[4]) + "\"" + "}";
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                    System.Diagnostics.Debug.WriteLine(content);

                    Users.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Employee>(content));

                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
        }
    }
}