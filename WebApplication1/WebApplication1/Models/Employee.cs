using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace WebApplication1.Models
{
    // 清理 只放 字段
    public class Employee
    {
        // production环境联数据库， App_Data 可以放Access，SQL Server类似的一个，XML
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        //[Display(Name = "Name")]
        public string UserName { get; set; }
        [Range(5,50)]
        public int Age { get; set; }
        //nvarchar 校验长度不一样 中文/英文
        public String Address { get; set; }
        public String Gender { get; set; }
       // public static int globalID = 1;
       
        public Employee(int _id, string _UserName, String _Address, int _Age, String _Gender)
        {
            this.Address = _Address;
            this.Gender = _Gender;
            this.UserName = _UserName;
            this.Age = _Age;
            this.UserId = _id;
        }

        public static List<Employee> GetUsers()
        {
            List<Employee> Users = new List<Employee>();
            DbHelper.selectUser(Users);
            
            return Users;
        }  
    }
}