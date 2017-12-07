using MyMvc4.Models;
//黄世兴
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyMvc4.Db
{
    public class Mssql
    {
        //string constr = "server=.;database=myschool;integrated security=SSPI";
        static string constr = "server=bds25678386.my3w.com;database=bds25678386_db;uid=bds25678386;pwd=huangshixing";
        //string constr = "data source=.;initial catalog=myschool;user id=sa;pwd=sa";
        static SqlConnection con = new SqlConnection(constr);
        // con.ConnectionString = constr;

        public bool connSql(Pos p)
        {
            try
            {
                con.Open();
                string sql =string.Format("insert into position(TEL,X,Y,SPEED,TIME,ACCURACY,DEVICEID) values('{0}',{1},{2},{3},'{4}','{5}','{6}')",
                    p.tel,p.x,p.y,p.speed,p.time,p.accuracy,p.deviceid);

                string sql_2 = string.Format("Merge Into LASTPOSITION p using (select {0} TEL,{1} X,{2} Y,{3} SPEED,{4} TIME,{5} ACCURACY,{6} DEVICEID,{7} ANGLE,{8} TEAMID) s on p.TEAMID=s.TEAMID When Matched Then Update set p.x=s.x,p.y=s.y When Not Matched Then Insert (tel,x,y) values (tel,x,y)"); 
                
                SqlCommand com = new SqlCommand(sql, con);
                int ret = com.ExecuteNonQuery();
                //MessageBox.Show("成功连接数据库");
                //int x = (int)com.ExecuteScalar();
                //MessageBox.Show(string.Format("成功读取{0},条记录", x));
            }
            catch (Exception)
            {
                return false ;
                //throw;
            }
            finally
            {
                con.Close();
                //MessageBox.Show("成功关闭数据库连接", "提示信息", MessageBoxButtons.YesNoCancel);
            }

            return true;
        }

        public string getLastPosition()
        {
            string ret = "";
            try
            {
                con.Open();
                string sql = "select top 1 * from position  order by time desc";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    //ret += "[" + sdr[2].ToString() + "," + sdr[3].ToString() + "],";
                    string x =  sdr[2].ToString();
                    string y = sdr[3].ToString();
                    string t = sdr[5].ToString();
                    string wc = sdr[6].ToString();
                    ret = x + "," + y + "," + t + "," + wc;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                //throw;
            }
            finally
            {
                con.Close();
                //MessageBox.Show("成功关闭数据库连接", "提示信息", MessageBoxButtons.YesNoCancel);
            }

            return ret;
        }

        public string getPtsByStartEndTime(string s,string e)
        {
            string ret = "[";
            try
            {
                con.Open();
                string sql = "select * from position where time between '" + s + "' and '" + e + "' and speed>0.1 order by time";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read()) 
                {
                    ret += "[" + sdr[2].ToString() + "," + sdr[3].ToString() + "],";
                }
                ret = ret.Remove(ret.Length - 1);
                ret += ret==""?"": "]";
            }
            catch (Exception ex)
            {
                return ex.Message;
                //throw;
            }
            finally
            {
                con.Close();
                //MessageBox.Show("成功关闭数据库连接", "提示信息", MessageBoxButtons.YesNoCancel);
            }

            return ret;
        }
    }
}