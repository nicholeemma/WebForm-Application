using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using WebApplication1.Models;
using System.Text;

namespace WebApplication1.Service
{
    public class ApiHelper
    {
        public static String apiUrl = System.Configuration.ConfigurationManager.AppSettings["apiStr"];

        public static void createUser(string name, string address, string gender, int age)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new System.IO.StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"UserId\":" + 0 + "," +
                "\"UserName\":" + "\"" + name + "\"" + "," +
                       "\"Age\":" + age + "," +
               "\"Address\":" + "\"" + address + "\"" + "," +
               "\"Gender\":" + "\"" + gender + "\"" + "}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public static List<Employee> getUser()
        {
            WebClient client = new WebClient();
           
            // + employee
            Stream objStream = client.OpenRead(apiUrl);
            StreamReader _read = new StreamReader(objStream, Encoding.UTF8);    //新建一个读取流，用指定的编码读取，此处是utf-8
            String str = _read.ReadToEnd();
            var model = JsonConvert.DeserializeObject<List<Employee>>(str);
            objStream.Close();
            //convert to employee
            _read.Close();
            return model;
        }
    }
}