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
using System.Net.Sockets;

namespace WebApplication1
{
    public class DbHelper
    {
        public static String connStr = ConfigurationManager.ConnectionStrings["dbConnStr"].ConnectionString;
        public static void createUser(string name, string address, string gender, int age)
        {
                // Assumes connectionString is a valid connection string.
                //string MyInsert = "insert into dbo.user_table(UserName, Age, Address, Gender)values('" + name + "','" + age + "','" + address + "','" + gender + "');" + "SELECT @@IDENTITY;";

                SqlParameter[] commandParameters = new SqlParameter[4];
                commandParameters[0] = new SqlParameter("@Username", name);
                commandParameters[1] = new SqlParameter("@Age", age);
                commandParameters[2] = new SqlParameter("@Address", address);
                commandParameters[3] = new SqlParameter("@Gender", gender);
                var id = SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "sp_createemployee", commandParameters);
                System.Diagnostics.Debug.WriteLine(id);
            
        }

        public static void editUser(string name, string address, string gender, int age, int id)
        {
            SqlParameter[] commandParameters = new SqlParameter[5];
            commandParameters[0] = new SqlParameter("@Username", name);
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
    }
}