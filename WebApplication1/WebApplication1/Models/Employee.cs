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
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
       
        public string UserName { get; set; }
        //加点别的东西 模型层的校验 有的在service 复杂
        // js校验过模型层嘛？ js和后端都要做校验 js会被绕过
        [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
        public int Age { get; set; }
        //nvarchar 校验长度不一样 中文/英文
        public String Address { get; set; }
        public String Gender { get; set; }
       
        public Employee () { }
        public Employee(int _id, string _UserName, String _Address, int _Age, String _Gender)
        {
            this.Address = _Address;
            this.Gender = _Gender;
            this.UserName = _UserName;
            this.Age = _Age;
            this.UserId = _id;
        }

       
    }
}