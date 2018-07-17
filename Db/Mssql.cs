using MyMvc4.Models;
//黄世兴
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

        public bool sqlInsert(Pos p)
        {
            try
            {
                con.Open();
                string sql = string.Format("insert into position(TEL,X,Y,SPEED,TIME,ACCURACY,DEVICEID,ANGLE) values('{0}',{1},{2},{3},'{4}','{5}','{6}','{7}')",
                    p.tel, p.x, p.y, p.speed, p.time, p.accuracy, p.deviceid, p.angle);
                SqlCommand com = new SqlCommand(sql, con);
                int ret = com.ExecuteNonQuery();

                string sql2 = string.Format("Merge Into LASTPOSITION p using (select '{0}' TEL,{1} X,{2} Y,{3} SPEED,'{4}' TIME,'{5}' ACCURACY,'{6}' DEVICEID,'{7}' TEAMID,'{8}' ANGLE) s on p.DEVICEID=s.DEVICEID When Matched Then Update set p.TEL=s.TEL,p.X=s.X,p.Y=s.Y,p.SPEED=s.SPEED,p.TIME=s.TIME,p.ACCURACY=s.ACCURACY,p.DEVICEID=s.DEVICEID,p.TEAMID=s.TEAMID,p.ANGLE=s.ANGLE When Not Matched Then Insert (TEL,X,Y,SPEED,TIME,ACCURACY,DEVICEID,TEAMID,ANGLE) values ('{0}',{1},{2},{3},'{4}','{5}','{6}','{7}','{8}');",
                    p.tel, p.x, p.y, p.speed, p.time, p.accuracy, p.deviceid, p.teamid, p.angle);

                SqlCommand com2 = new SqlCommand(sql2, con);
                int ret2 = com2.ExecuteNonQuery();

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

        public string getLastPosition(int teamid)
        {
            string ret = "";
            try
            {
                con.Open();
                string sql = "select * from lastposition where teamid='" + teamid.ToString() + "'";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    //ret += "[" + sdr[2].ToString() + "," + sdr[3].ToString() + "],";
                    string tel = sdr[1].ToString();
                    string x = sdr[2].ToString();
                    string y = sdr[3].ToString();
                    string time = sdr[5].ToString();
                    string accuracy = sdr[6].ToString();
                    string deviceid = sdr[7].ToString();
                    string angle = sdr[8].ToString();
                    //string teamid = sdr[9].ToString();
                    ret += tel + ",";
                    ret += x + ",";
                    ret += y + ",";
                    ret += time + ",";
                    ret += accuracy + ",";
                    ret += deviceid + ",";
                    ret += angle + ",";
                    ret += teamid + ";";
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

        public string getLastPosition(string devid)
        {
            string ret = "";
            try
            {
                con.Open();
                string sql = "select * from lastposition where deviceid='"+devid+"' order by time desc";
                SqlCommand com = new SqlCommand(sql, con);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    //ret += "[" + sdr[2].ToString() + "," + sdr[3].ToString() + "],";
                    string tel = sdr[1].ToString();
                    string x = sdr[2].ToString();
                    string y = sdr[3].ToString();
                    string speed = sdr[4].ToString();
                    string time = sdr[5].ToString();
                    string accuracy = sdr[6].ToString();
                    string deviceid = sdr[7].ToString();
                    string angle = sdr[8].ToString();
                    //string teamid = sdr[9].ToString();
                    //ret += tel + ",";
                    ret += x + ",";
                    ret += y + ",";
                    ret += time + ",";
                    ret += accuracy + ",";
                    ret += deviceid + ",";
                    ret += speed;
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