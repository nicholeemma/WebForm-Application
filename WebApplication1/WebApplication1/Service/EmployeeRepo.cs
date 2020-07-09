using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using CommonLibrary;

namespace WebApplication1.Service
{
    public class EmployeeRepo
    {
        public static List<Employee> GetUsers()
        {
            List<Employee> Users = new List<Employee>();
            DbHelper.selectUser(Users);
            return Users;
        }
    }
}