using MyMvc4.Db;
using MyMvc4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyMvc4.Controllers
{
    public class PositionController : ApiController
    {
        // GET api/position
        public string Get()
        {
            Mssql ms = new Mssql();
            return ms.getLastPosition("867822027117158");
            //return new string[] { "value1", "value2" };
        }

        // GET api/position/5
        public string Get(int id)
        {
            Mssql ms = new Mssql();
            string ret = ms.getLastPosition(id);
            return ret;
        }

        // POST api/position
        public string Post([FromBody]string value)
        {
            return "value:"+value;
        }

        [HttpPost]
        public string postStartEnd(DateTimeStartEnd se)
        {
            Mssql ms = new Mssql();
            return ms.getPtsByStartEndTime(se.starttime, se.endtime);
        }

        [HttpPost]
        public string PostLastPosition(Pos p)
        {
            Mssql ms = new Mssql();
            return ms.getLastPosition("867822027117158");
        }

        [HttpPost]
        public string PostPosition(Pos p)
        {
            Mssql ms = new Mssql();
            return ms.sqlInsert(p).ToString();
        }

        // PUT api/position/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/position/5
        public void Delete(int id)
        {
        }
    }
}
