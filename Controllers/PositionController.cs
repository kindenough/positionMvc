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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/position/5
        public string Get(int id)
        {
            //Mssql ms = new Mssql();
            //return ms.connSql(new Pos()).ToString();
            return id.ToString();
        }

        // POST api/position
        public string Post([FromBody]string value)
        {
            return value;
        }

        [HttpPost]
        public string postStartEnd(DateTimeStartEnd se)
        {
            Mssql ms = new Mssql();
            return ms.getPtsByStartEndTime(se.starttime, se.endtime);
        }

        [HttpPost]
        public string postLastPosition(DateTimeStartEnd se)
        {
            Mssql ms = new Mssql();
            return ms.getLastPosition();
        }

        [HttpPost]
        public string PostPosition(Pos p)
        {
            Mssql ms = new Mssql();
            return ms.connSql(p).ToString();
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
