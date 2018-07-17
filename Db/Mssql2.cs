using MyMvc4.Models;
//黄世兴
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyMvc4.Db
{
    public class Mssql2
    {
        static string constr = "server=bds25678386.my3w.com;database=bds25678386_db;uid=bds25678386;pwd=huangshixing";
        static SqlConnection con = new SqlConnection(constr);
        public static bool sqlInsert(Pos p)
        {
            try
            {
                con.Open();
                string sql = string.Format("insert into position2(TEL,X,Y,SPEED,TIME,ACCURACY,DEVICEID,ANGLE) values('{0}',{1},{2},{3},'{4}','{5}','{6}','{7}')",
                    p.tel, p.x, p.y, p.speed, p.time, p.accuracy, p.deviceid, p.angle);
                SqlCommand com = new SqlCommand(sql, con);
                int ret = com.ExecuteNonQuery();

                string sql2 = string.Format("Merge Into LASTPOSITION2 p using (select '{0}' TEL,{1} X,{2} Y,{3} SPEED,'{4}' TIME,'{5}' ACCURACY,'{6}' DEVICEID,'{7}' TEAMID,'{8}' ANGLE) s on p.DEVICEID=s.DEVICEID When Matched Then Update set p.TEL=s.TEL,p.X=s.X,p.Y=s.Y,p.SPEED=s.SPEED,p.TIME=s.TIME,p.ACCURACY=s.ACCURACY,p.DEVICEID=s.DEVICEID,p.TEAMID=s.TEAMID,p.ANGLE=s.ANGLE When Not Matched Then Insert (TEL,X,Y,SPEED,TIME,ACCURACY,DEVICEID,TEAMID,ANGLE) values ('{0}',{1},{2},{3},'{4}','{5}','{6}','{7}','{8}');",
                    p.tel, p.x, p.y, p.speed, p.time, p.accuracy, p.deviceid, p.teamid, p.angle);

                SqlCommand com2 = new SqlCommand(sql2, con);
                int ret2 = com2.ExecuteNonQuery();

                //MessageBox.Show("成功连接数据库");
                //int x = (int)com.ExecuteScalar();
                //MessageBox.Show(string.Format("成功读取{0},条记录", x));
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
            finally
            {
                con.Close();
                //MessageBox.Show("成功关闭数据库连接", "提示信息", MessageBoxButtons.YesNoCancel);
            }

            return true;
        }
    }
}