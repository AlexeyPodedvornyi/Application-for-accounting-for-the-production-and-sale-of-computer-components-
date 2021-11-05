using MySql.Data.MySqlClient;
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
    public partial class adm_Form : Form
    {
        public adm_Form()
        {
            InitializeComponent();
        }
        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);
        private void adm_Form_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            tabl2.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl2.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl2.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            l3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            lvl1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            lvl2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            ed1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            int[] mas = new int[5];
            mas[0] = 0; mas[1] = 1; mas[2] = 2;mas[3] = 3; mas[4] = 4;
            lvl1.Items.Add(0); lvl2.Items.Add(0);
            lvl1.Items.Add(1); lvl2.Items.Add(1);
            lvl1.Items.Add(2); lvl2.Items.Add(2);
            lvl1.Items.Add(3); lvl2.Items.Add(3);
            lvl1.Items.Add(4); lvl2.Items.Add(4);
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select login as 'Логин', pass as 'Пароль', lvl as 'Уровень доступа' " +
                    "from tmp_user", con);
                MySqlCommand comc = new MySqlCommand("select user_log as 'Логин', table_name as 'Название таблицы', action as 'Действие', query_time as 'Время выполнения действия' " +
                    "from log", con);
                MySqlCommand com1 = new MySqlCommand("select * from tmp_user", con);
                MySqlCommand com2 = new MySqlCommand("select * from log", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }
                rd = com1.ExecuteReader();
                while (rd.Read())
                {
                    l3.Items.Add(Convert.ToString(rd["login"]));
                    ed1.Items.Add(Convert.ToString(rd["login"]));
                }
                rd.Close();
                rd = comc.ExecuteReader();
                dt1.Load(rd);
                if (dt1.Rows.Count > 0)
                {
                    tabl2.DataSource = dt1;
                }
                rd.Close();
            }
        }

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select login as 'Логин', pass as 'Пароль', lvl as 'Уровень доступа' " +
                    "from tmp_user", con);
                MySqlCommand comc = new MySqlCommand("select user_log as 'Логин', table_name as 'Название таблицы', action as 'Действие', query_time as 'Время выполнения действия' " +
                    "from log", con);
                MySqlCommand com1 = new MySqlCommand("select * from tmp_user", con);
                MySqlCommand com2 = new MySqlCommand("select * from log", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }
                rd = com1.ExecuteReader();
                l3.Items.Clear();ed1.Items.Clear();
                while (rd.Read())
                {
                    l3.Items.Add(Convert.ToString(rd["login"]));
                    ed1.Items.Add(Convert.ToString(rd["login"]));
                }
                rd.Close();
                rd = comc.ExecuteReader();
                dt1.Load(rd);
                if (dt1.Rows.Count > 0)
                {
                    tabl2.DataSource = dt1;
                }
                rd.Close();
            }
        }

        private void ad_but_Click(object sender, EventArgs e)
        {
            string a, b, c, wbp = "";
            a = l1.Text;
            b = p1.Text;
            c = Convert.ToString(lvl1.SelectedItem);
            MySqlCommand com = new MySqlCommand("insert into tmp_user values (@a, @b, @c)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(login) from tmp_user where login = @a", MC);
            com1.Parameters.AddWithValue("a", a);

            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (a.Length == 0 || b.Length == 0 || c.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
               "Поля не могут быть пустыми! Необходимо заполнить все поля",
               "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            wbp = com1.ExecuteScalar().ToString();
            if (wbp == "1")
            {
                DialogResult ans = MessageBox.Show(
                        "Пользователь с таким логином уже существует",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.ExecuteNonQuery();
        }

        private void red_but_Click(object sender, EventArgs e)
        {
            string a, b, c,d,tmp, l = "",p="",lv =""; int i=0;
            a = l2.Text;
            b = p2.Text;
            c = Convert.ToString(lvl2.SelectedItem);
            d = Convert.ToString(ed1.SelectedItem);
            MySqlCommand com = new MySqlCommand("update tmp_user set login = @a, pass = @b, lvl = @c where login = @d", MC);
            MySqlCommand com1 = new MySqlCommand("select count(login) from tmp_user where login = @a", MC);
            MySqlCommand com2 = new MySqlCommand("select login from tmp_user where login = @d", MC);
            MySqlCommand com3 = new MySqlCommand("select pass from tmp_user where login = @d", MC);
            MySqlCommand com4 = new MySqlCommand("select lvl from tmp_user where login = @d", MC); 
            com2.Parameters.AddWithValue("d", d);
            com3.Parameters.AddWithValue("d", d);
            com4.Parameters.AddWithValue("d", d);

            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (d.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
                "Выберите номера изменяемых записей в выпадающих списках!",
                "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            l = com2.ExecuteScalar().ToString();
            p = com3.ExecuteScalar().ToString();
            lv = com4.ExecuteScalar().ToString();i = 0;
            if (a.Length == 0)
            {
                a = l;
                i++;
            }
            else
            {
                com1.Parameters.AddWithValue("a", a);
                l = com1.ExecuteScalar().ToString();
                if (l == "1")
                {
                    DialogResult ans = MessageBox.Show(
                            "Пользователь с таким логином уже существует",
                            "Ошибка ввода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if(b.Length==0)
            {
                b = p;
                i++;
            }
            if(c.Length==0)
            {
                c = lv;
                i++;
            }
            if(i==3)
            {
                DialogResult ans = MessageBox.Show(
               "Поля не могут быть пустыми! Необходимо заполнить хотя бы 1-о поле",
               "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("d", d);
            com.ExecuteNonQuery();
        }

        private void del_but_Click(object sender, EventArgs e)
        {
            string a,tmp;
            a = Convert.ToString(l3.SelectedItem);

            MySqlCommand com = new MySqlCommand("delete from tmp_user where login = @a", MC);
            MySqlCommand com5 = new MySqlCommand("select @tmp_log", MC);
            com.Parameters.AddWithValue("a", a);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            tmp = com5.ExecuteScalar().ToString();
            if(a == tmp)
            {
                DialogResult ans = MessageBox.Show(
             "Вы не можете удалить запись о себе находясь в системе",
             "Ошибка",
              MessageBoxButtons.OK,
              MessageBoxIcon.Error,
              MessageBoxDefaultButton.Button1);
                return;
            }
            com.ExecuteNonQuery();
        }
    }
}
