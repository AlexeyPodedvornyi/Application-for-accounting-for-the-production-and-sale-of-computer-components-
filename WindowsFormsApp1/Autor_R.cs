using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Autor_R : Form
    {
        public static string current_login;
        public Autor_R()
        {
            InitializeComponent();
            pfield.AutoSize = false;
            pfield.Size = new Size(pfield.Size.Width, 41);
            lfield.ForeColor = Color.Gray;
            pfield.ForeColor = Color.Gray;
            lfield.Text = "Введите логин";
            pfield.Text = "Введите пароль";
        }
        //public static string connect_1 = "server=localhost;port=3306;username=root;password=root;database=lab;Allow User Variables=True;convert zero datetime=true";
       public static string connect_1 = "server=localhost;port=3306;username=root;password=root;database=lab;Allow User Variables=True;convert zero datetime=true";
        public static string log;
        MySqlConnection MC = new MySqlConnection(connect_1);
        DB db = new DB();
        private void button1_Click(object sender, EventArgs e) ///CONECT K BD NA KNOPKU
        {
            string login_u = lfield.Text;
            string pass_u = pfield.Text;

           // DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            //MessageBox.Show(login_u, pass_u);

            const string CmdText = "select * from tmp_user WHERE login = @l AND pass = @p";
            MySqlCommand com0 = new MySqlCommand("select lvl from tmp_user where login = @l", MC) ;
            MySqlCommand com1 = new MySqlCommand("select login from tmp_user where login = @l", MC);
            MySqlCommand command = new MySqlCommand(CmdText, db.GetConnection());//Соединяемся с БД и выполняем команду
            string login = "";
                
            command.Parameters.Add("@l", MySqlDbType.VarChar).Value = login_u;//Присваиваем заглушке введенный логин
            command.Parameters.Add("@p", MySqlDbType.VarChar).Value = pass_u;//Присваиваем заглушке введенный пароль
            com0.Parameters.Add("@l", MySqlDbType.VarChar).Value = login_u;
            com1.Parameters.Add("@l", MySqlDbType.VarChar).Value = login_u;
            adapter.SelectCommand = command;//Выбираем команду для выполнения адаптером
            try
            { adapter.Fill(table); }//Заполняем таблицу table получеными данными 
            catch(MySqlException)
            {
                MessageBox.Show("Отсутсвует соединение с сервером MySQL\nОбратитесь к администратору", "\nОшибка ",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1
                      );
                return;
            }
            try
            {
                if (MC.State == ConnectionState.Closed)
                    MC.Open();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Невозможно установить соединение с удаленным сервером MySQL\nОбратитесь к администратору", "\nОшибка ",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1
                      );
                return;
            }
            if (table.Rows.Count > 0)
            {
                log = com0.ExecuteScalar().ToString();//
                login = com1.ExecuteScalar().ToString();
                current_login = login;
                MessageBox.Show("Вы успешно вошли в систему.", "\nДобро пожаловать! ",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Information,
                     MessageBoxDefaultButton.Button1
                      );
                MC.Close();
                this.Hide();
                
                Menu main = new Menu(ref log,ref login);
                main.Show();

            }
            else
                MessageBox.Show("Ошибка авторизации. \nПроверьте правильность ввода данных!");


        }

        private void lfield_Enter(object sender, EventArgs e)
        {
            if (lfield.Text == "Введите логин")
            {
                lfield.ForeColor = Color.White;
                lfield.Text = "";
            }
            
        }

        private void lfield_Leave(object sender, EventArgs e)
        {
            if (lfield.Text == "")
            {
                lfield.ForeColor = Color.Gray;
                lfield.Text = "Введите логин";
            }
        }

        private void pfield_Enter(object sender, EventArgs e)
        {
            if(pfield.Text == "Введите пароль")
            {
                pfield.ForeColor = Color.White;
                pfield.Text = "";
            }
        }

        private void pfield_Leave(object sender, EventArgs e)
        {
            if (pfield.Text == "")
            {
                pfield.ForeColor = Color.Gray;
                pfield.Text = "Введите пароль";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            db.CloseConnection();
            Application.Exit();
        }
    }
}
