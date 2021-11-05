using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class DB
    {
        MySqlConnection connect = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=lab");

        public void OpenConnection()   ///Соединение с БД
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        public void CloseConnection() /// Закрываем сессию с БД
        {
            if (connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }

        public MySqlConnection GetConnection() /// Возвращаем соединение
        {
            return connect;
        }
    }
}
