using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class clientsForm : Form
    {
        public clientsForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void clientsForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            a_box4.Enabled = false; a_box4.Text = "0";
            e_box4.Enabled = false; 
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_client as 'Номер клиента', dir_FIO as 'ФИО директора', p_numb as 'Номер телефона', " +
                    "acc_am as 'Накопленная сумма' from clients", con);
                MySqlCommand com1 = new MySqlCommand("select * from clients", con);
                MySqlCommand com2 = new MySqlCommand("select id_client from clients", con);
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
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_client"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_client"]));
                }
            }
        }

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_client as 'Номер клиента', dir_FIO as 'ФИО директора', p_numb as 'Номер телефона', " +
                    "acc_am as 'Накопленная сумма' from clients", con);
                MySqlCommand com1 = new MySqlCommand("select * from clients", con);
                MySqlCommand com2 = new MySqlCommand("select id_client from clients", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }

                rd = com1.ExecuteReader();
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();

                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_client"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_client"]));
                }
            }
        }
        public int Regex_fi(TextBox tb, ref int j) //проверка на ФИ
        {
            string ra_fi = @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+$"; Regex regex = new Regex(ra_fi);
            if (regex.IsMatch(tb.Text) == false)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'ФИ'!\nФамилия и имя не соответсвуют формату 'fi'",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1); j = 1;
            }
            return j;
        }
        public int Regex_tel(TextBox tb, ref int j) //проверка на тел
        {
            string ra_nom = @"^(\+380)[0-9]{9}$", s1;
            Regex regex = new Regex(ra_nom);
            if (regex.IsMatch(tb.Text) == false)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона не соответсвует формату 'nom'",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1); j = 1; return j;
            }
            if (tb.Text.Length != 13)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона должен иметь длину 13 символов включая код страны",
                "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1); j = 1; return j;
            }
            s1 = tb.Text.Substring(0, tb.Text.Length - 9); //+380
            if (s1 != "+380")
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона должен начинатся так '+380', если нужен код другой страны - обратитесь к администратору",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1); j = 1; return j;
            }
            
            return j;
        }
        private void butAdd_Click(object sender, EventArgs e)
        {
            string a, b, c, d,prov; int j = 0; prov = ""; int num; bool isNum1 = int.TryParse(a_box1.Text.Trim(), out num);
            a = a_box1.Text;
            b = a_box2.Text;
            c = a_box3.Text; 
            d = a_box4.Text;

            MySqlCommand com = new MySqlCommand("insert into clients values (@a, @b, @c, @d)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_client) from clients where id_client = @a", MC);
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
            if (isNum1 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Номер клиента' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            prov = com1.ExecuteScalar().ToString();
            if (prov == "1")
            {
                DialogResult ans = MessageBox.Show(
                        "Клиент с таким номером уже существует",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            Regex_fi(a_box2, ref j);
            if (j == 1)
                return;
            Regex_tel(a_box3, ref j);
            if (j == 1)
                return;

            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Клиенты','Добавление записей',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("d", d);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            string a, b, c, d, g, id = "", fi = "", tel = "", acc = ""; int i = 0, j = 0, num; ; bool isNum1 = int.TryParse(e_box1.Text.Trim(), out num);
            a = e_box1.Text;
            b = e_box2.Text;
            c = e_box3.Text;
            d = e_box4.Text;
            g = Convert.ToString(comboBox1.SelectedItem);

            MySqlCommand com = new MySqlCommand("update clients set id_client = @a, dir_FIO = @b, p_numb = @c, acc_am = @d " +
               "where id_client = @g", MC);
            MySqlCommand com1 = new MySqlCommand("select id_client from clients where id_client = @g", MC);
            MySqlCommand com2 = new MySqlCommand("select dir_FIO from clients where id_client = @g", MC);
            MySqlCommand com3 = new MySqlCommand("select p_numb from clients where id_client = @g", MC);
            MySqlCommand com4 = new MySqlCommand("select acc_am from clients where id_client = @g", MC);
            com1.Parameters.AddWithValue("g", g);
            com2.Parameters.AddWithValue("g", g);
            com3.Parameters.AddWithValue("g", g);
            com4.Parameters.AddWithValue("g", g);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (g.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
                "Выберите номера изменяемых записей в выпадающих списках!",
                "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            id = com1.ExecuteScalar().ToString();
            fi = com2.ExecuteScalar().ToString();
            tel = com3.ExecuteScalar().ToString();
            acc = com4.ExecuteScalar().ToString();
            if (e_box1.TextLength == 0)
            {
                a = id;
                i++;
            }
            else
            {
                MySqlCommand com0 = new MySqlCommand("select count(id_client) from clients where id_client = @a", MC);
                com0.Parameters.AddWithValue("a", a);
                if (isNum1 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Номер клиента' не должно содержать буквы и символы! \nПопробуйте еще раз",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
                id = com0.ExecuteScalar().ToString();
                if (id == "1")
                {
                    DialogResult ans = MessageBox.Show(
                            "Клиент с таким номером уже существует",
                            "Ошибка ввода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (b.Length == 0)         // 
            {
                b = fi;
                i++;
            }
            else
            {
                Regex_fi(e_box2, ref j);
                if (j == 1)
                    return;
            }
            if (c.Length == 0)         // 
            {
                c = tel;
                i++;
            }
            else
            {
                Regex_tel(e_box3, ref j);
                if (j == 1)
                    return;
            }
            if (d.Length == 0)         // 
            {
                d = acc;
                i++;
            }
            if (i == 4)
            {
                DialogResult ans = MessageBox.Show(
                "Поля не могут быть пустыми! Необходимо заполнить хотя бы 1-о поле",
                "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Клиенты','Редактирование',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("d", d);
            com.Parameters.AddWithValue("g", g);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            string a;
            a = Convert.ToString(comboBox2.SelectedItem);

            MySqlCommand com = new MySqlCommand("delete from clients where id_client = @a", MC);
            com.Parameters.AddWithValue("a", a);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Клиенты','Удаление',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }
    }
}
