using MyMvc4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyMvc4.Controllers
{
    public class FileController : ApiController
    {
        // GET api/file
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/file/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/file
        public string Post()
        {
            string ret = "POST1";
            //string user = rf.user;
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string szImei = "00";//HttpContext.Current.Request["szImei"].ToString();
            foreach (string key in files.AllKeys)
            {
                HttpPostedFile file1 = files[key];
                if (string.IsNullOrEmpty(file1.FileName) == false)
                    file1.SaveAs(HttpContext.Current.Server.MapPath("~/App_Data/") + file1.FileName);
                    //file1.SaveAs(HttpContext.Current.Server.MapPath("~/upload/") + szImei + " a.db");
            }   
            ret = "POST2";
            return ret + szImei;
        }

        // PUT api/file/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/file/5
        public void Delete(int id)
        {
        }
    }
}
