using MyMvc4.Db;
using MyMvc4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyMvc4.Controllers
{
    public class Position2Controller : ApiController
    {
        // GET api/positionm
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/positionm/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public string PostPosition(Pos p)
        {
            return Mssql2.sqlInsert(p).ToString();
        }

        // POST api/positionm
        public void Post([FromBody]string value)
        {
        }

        // PUT api/positionm/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/positionm/5
        public void Delete(int id)
        {
        }
    }
}
