//黄世兴
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyMvc4.Models
{
    public class Pos
    {
        //public int id { get; set; }
        public string tel { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string speed { get; set; }
        public string time {get;set;}
        public string accuracy{ get; set; }
        public string deviceid { get; set; }
    }

    public class DateTimeStartEnd
    {
        public string starttime;
        public string endtime;
    }
}