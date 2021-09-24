using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using SchattenclownBot.Model.Objects;
using SchattenclownBot.Model.Persistence.Connection;
using SchattenclownBot.HelpClasses;

namespace SchattenclownBot.Model.Persistence
{
    public class DB_ScTimer
    {
        public static List<ScTimer> ReadAll()
        {
            string sql = "SELECT * FROM Timer";

            List<ScTimer> lstTimer = new List<ScTimer>();
            MySqlConnection connection = DB_Connection.OpenDB();
            MySqlDataReader dataReader = DB_Connection.ExecuteReader(sql, connection);

            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    ScTimer timer = new ScTimer
                    {
                        DBEntryID = dataReader.GetInt32("DBEntryID"),
                        NotificationTime = dataReader.GetDateTime("NotificationTime"),
                        ChannelId = dataReader.GetUInt64("ChannelId"),
                        MemberId = dataReader.GetUInt64("MemberId")
                    };
                    lstTimer.Add(timer);
                }
            }

            return lstTimer;
        }
        public static void Add(ScTimer timer)
        {
            string sql = $"INSERT INTO Timer (NotificationTime, ChannelId, MemberId) " +
                         $"VALUES ('{timer.NotificationTime:yyyy-MM-dd HH:mm:ss}', {timer.ChannelId}, {timer.MemberId})";
            DB_Connection.ExecuteNonQuery(sql);
        }
        public static void Delete(ScTimer scTimer)
        {
            string sql = $"DELETE FROM Timer WHERE `DBEntryID` = '{scTimer.DBEntryID}'";
            DB_Connection.ExecuteNonQuery(sql);
        }
        public static void CreateTable_Timer()
        {
            CSV_Connections cSV_Connections = new CSV_Connections();
            Connections cons = new Connections();
            cons = CSV_Connections.ReadAll();

            string database = WordCutter.RemoveUntilWord(cons.MySqlConStr, "Database=", 9);
#if DEBUG
            database = WordCutter.RemoveUntilWord(cons.MySqlConStrDebug, "Database=", 9);
#endif
            database = WordCutter.RemoveAfterWord(database, "; Uid", 0);

            string sql = $"CREATE DATABASE IF NOT EXISTS `{database}`;" +
                         $"USE `{database}`;" +
                          "CREATE TABLE IF NOT EXISTS `Timer` (" +
                          "`DBEntryID` int(12) NOT NULL AUTO_INCREMENT," +
                          "`NotificationTime` DATETIME NOT NULL," +
                          "`ChannelId` bigint(20) NOT NULL," +
                          "`MemberId` bigint(20) NOT NULL," +
                          "PRIMARY KEY (`DBEntryID`)) " +
                          "ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;";

            DB_Connection.ExecuteNonQuery(sql);
        }
    }
}
