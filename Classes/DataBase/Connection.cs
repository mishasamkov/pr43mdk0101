using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace WpfApp1.Classes.DataBase
{
    public class Connection
    {
        private static readonly string config = "server=HOME-PC\\MYSERVER;" +
            "Trusted_Connection=No;" +
            "DataBase=BooksAndAuthors;" +
            "User=sa;" +
            "PWD=sa;";
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(config);
            connection.Open();
            return connection;
        }
        public static SqlDataReader Query(string sql, out SqlConnection connection)
        {
            connection = OpenConnection();
            return new SqlCommand(sql, connection).ExecuteReader();
        }
        public static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
            SqlConnection.ClearPool(connection);
        }
    }
}
