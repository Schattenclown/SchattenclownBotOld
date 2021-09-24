using MySql.Data.MySqlClient;
using SchattenclownBot.Model.Objects;
using System;
using System.Threading;

namespace SchattenclownBot.Model.Persistence.Connection
{
    class DB_Connection
    {
        private static string token = "";
        private static int virgin = 0;
        public static void SetDB()
        {
            Connections connections = Connections.GetConnections();
            token = connections.MySqlConStr;
#if DEBUG
            token = connections.MySqlConStrDebug;
#endif
        }
        public static MySqlConnection OpenDB()
        {
            if (virgin == 0)
                SetDB(); 
            virgin = 69;

            MySqlConnection connection = new MySqlConnection(token);
            do
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: SetDB MESSAGE: {ex.Message}");
                    Thread.Sleep(10);
                }
            } while (connection == null);

            return connection;
        }
        public static void CloseDB(MySqlConnection connection)
        {
            connection.Close();
        }
        public static void ExecuteNonQuery(string sql)
        {
            MySqlConnection connection = OpenDB();
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            int ret = sqlCommand.ExecuteNonQuery();
            if (ret != -1)
                Console.WriteLine("DEBUG: DB -1");
            CloseDB(connection);
        }
        public static MySqlDataReader ExecuteReader(String sql, MySqlConnection connection)
        {
            MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
            try
            {
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                return sqlDataReader;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: ExecuteReader MESSAGE: {ex.Message}");
                return null;
            }
        }
    }
}
