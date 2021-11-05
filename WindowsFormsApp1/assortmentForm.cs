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
    public partial class assortmentForm : Form
    {
        public assortmentForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void assortmentForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            a_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            e_cb1.Enabled = false;
            e_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_prod as 'Номер товара',prod_name as 'Название товара', " +
                    "cost_1 as 'Цена' from assortment", con);
                MySqlCommand com1 = new MySqlCommand("select * from assortment", con);
                MySqlCommand com2 = new MySqlCommand("select id_prod from prod_stor", con);
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
                a_cb1.Items.Clear();
                e_cb1.Items.Clear();
                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_prod"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_prod"]));
                }
                rd.Close();
                rd = com2.ExecuteReader();
                while (rd.Read())
                {
                    a_cb1.Items.Add(Convert.ToInt32(rd["id_prod"]));
                    e_cb1.Items.Add(Convert.ToInt32(rd["id_prod"]));
                }
                rd.Close();
            }
        }

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_prod as 'Номер товара',prod_name as 'Название товара', " +
                    "cost_1 as 'Цена' from assortment", con);
                MySqlCommand com1 = new MySqlCommand("select * from assortment", con);
                MySqlCommand com2 = new MySqlCommand("select id_prod from prod_stor", con);
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
                a_cb1.Items.Clear();
                e_cb1.Items.Clear();
                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_prod"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_prod"]));
                }
                rd.Close();
                rd = com2.ExecuteReader();
                while (rd.Read())
                {
                    a_cb1.Items.Add(Convert.ToInt32(rd["id_prod"]));
                    e_cb1.Items.Add(Convert.ToInt32(rd["id_prod"]));
                }
                rd.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a, b, c,prov = "" ; int j = 0;  int num; bool isNum1 = int.TryParse(a_box3.Text.Trim(), out num);
            a = a_box1.Text;
            b = Convert.ToString(a_cb1.SelectedItem);
            c = a_box3.Text;

            MySqlCommand com = new MySqlCommand("insert into assortment values (@a, @b, @c)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_prod) from assortment where id_prod = @b", MC);
            com1.Parameters.AddWithValue("b", b);
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
            prov = com1.ExecuteScalar().ToString();
            if (prov == "1")
            {
                DialogResult ans = MessageBox.Show(
                        "Товар с таким номером уже существует",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum1 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Цена' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            int c1 = int.Parse(c);
            if (c1<=0 || c1>100000) //////// PROVERKA NA KOL-VO
            {
                DialogResult ans = MessageBox.Show(
                "Недопустимое значение цены. \nПоле 'Цена' не может иметь значение больше 100000 или меньше либо равно 0",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Товар в ассортименте','Добавление записей',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string a, b, c, g, name = "", id = "", cost = ""; int i = 0, j = 0, num; ; bool isNum1 = int.TryParse(e_box4.Text.Trim(), out num);
            a = e_box2.Text;
            b = Convert.ToString(e_cb1.SelectedItem);
            c = e_box4.Text;
            g = Convert.ToString(comboBox1.SelectedItem);

            MySqlCommand com = new MySqlCommand("update assortment set id_prod = @b, prod_name = @a, cost_1 = @c " +
               "where id_prod = @g", MC);
            MySqlCommand com1 = new MySqlCommand("select prod_name from assortment where id_prod = @g", MC);
            MySqlCommand com2 = new MySqlCommand("select id_prod from assortment where id_prod = @g", MC);
            MySqlCommand com3 = new MySqlCommand("select cost_1 from assortment where id_prod = @g", MC);
            com1.Parameters.AddWithValue("g", g);
            com2.Parameters.AddWithValue("g", g);
            com3.Parameters.AddWithValue("g", g);
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
            name = com1.ExecuteScalar().ToString();
            id = com2.ExecuteScalar().ToString();
            cost = com3.ExecuteScalar().ToString();
            if (a.Length == 0)
            {
                a = name;
                i++;
            }

            if (b.Length == 0)
            {
                b = id;
                i++;
            }
            if (c.Length == 0)
            {
                c = cost;
                i++;
            }
            else
            {
                MySqlCommand com0 = new MySqlCommand("select count(id_prod) from waybill_sp where id_prod = @b", MC);
                com0.Parameters.AddWithValue("b", b);
                cost = com0.ExecuteScalar().ToString();
                int c1 = int.Parse(cost);
                if(c1>0)
                {
                    DialogResult ans = MessageBox.Show(
                            "Вы не можете изменить цену пока не отправлены все накладные с содержащимся товаром\nОбратитесь к администратору или попробуйте позже",
                            "Внимание",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning,
                            MessageBoxDefaultButton.Button1);
                    return;
                }
                if (isNum1 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Цена' не должно содержать буквы и символы! \nПопробуйте еще раз",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
                c1 = int.Parse(c);
                if (c1 <= 0 || c1 > 100000) //////// PROVERKA NA KOL-VO
                {
                    DialogResult ans = MessageBox.Show(
                    "Недопустимое значение цены. \nПоле 'Цена' не может иметь значение больше 100000 или меньше либо равно 0",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (i == 3)
            {
                DialogResult ans = MessageBox.Show(
                "Поля не могут быть пустыми! Необходимо заполнить хотя бы 1-о поле",
                "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Товар в ассортименте','Редактирование',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("g", g);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a,c;
            a = Convert.ToString(comboBox2.SelectedItem);

            MySqlCommand com = new MySqlCommand("delete from assortment where id_prod = @a", MC);
            MySqlCommand com1 = new MySqlCommand("delete from prod_costs where id_prod = @a", MC);
            MySqlCommand com2 = new MySqlCommand("select count(id_prod) from prod_req where id_prod = @a", MC);
            com.Parameters.AddWithValue("a", a); com1.Parameters.AddWithValue("a", a); com2.Parameters.AddWithValue("a", a);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            c = com2.ExecuteScalar().ToString();
            int cc = int.Parse(c);
            if(cc>0)
            {
                DialogResult ans = MessageBox.Show(
                "Этот товар еще в процессе производства. \nДля удаления необходимо снять его с производства и повторить попытку",
                "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Товар в ассортименте','Удаление',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);
            com1.ExecuteNonQuery(); com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }
    }
}
