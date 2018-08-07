using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace MyMvc4.Db
{
    public class Sqlite3
    {
        //数据库连接
        SQLiteConnection m_dbConnection;
        public static string dbname = "record.db";
        public Sqlite3(string filepath,bool create=false)
        {
            createNewDatabase(filepath);
            connectToDatabase(filepath);
        }

        ~Sqlite3()
        {
            m_dbConnection.Close();
        }

        public void Program(string filepath)
        {
            createNewDatabase(filepath);
            connectToDatabase(filepath);
            createTable();
            fillTable();
            printHighscores();
        }
        //创建一个空的数据库
        void createNewDatabase(string filepath)
        {
            if( !File.Exists(filepath) )
                SQLiteConnection.CreateFile(filepath);
        }
        //创建一个连接到指定数据库
        void connectToDatabase(string filepath)
        {
            m_dbConnection = new SQLiteConnection("Data Source="+filepath+";Version=3;");
            m_dbConnection.Open();
        }
        //在指定数据库中创建一个table
        void createTable()
        {
            string sql = "create table highscores (name varchar(20), score int)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        //插入一些数据
        void fillTable()
        {
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        //使用sql查询语句，并显示结果
        void printHighscores()
        {
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            Console.ReadLine();
        }

        public string GetData(string starttime, string endtime)
        {
            string ret = "[";
            string sql = string.Format("select * from position where dtime>'{0}' and dtime<'{1}' order by dtime", starttime, endtime);

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                string x = reader["x"].ToString();
                string y = reader["y"].ToString();
                ret += string.Format("[{0},{1}],",x,y);
            }

            if (ret != "[")
            {
                ret = ret.Remove(ret.Length - 1);
            }
            ret += "]";
            return ret;
        }
    }
}