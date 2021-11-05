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
    public partial class mat_storForm : Form
    {
        public mat_storForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void mat_storForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            o_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            p_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
                                                                                  // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_mat as 'Номер материала',mat_name as 'Название материала', " +
                    "stor_count as 'Количество на складе' from mat_stor", con);
                MySqlCommand com1 = new MySqlCommand("select * from mat_stor", con);
                MySqlCommand com2 = new MySqlCommand("select id_mat from mat_stor", con);
                MySqlCommand com3 = new MySqlCommand("select count(*) from sklad_m where dat = @dat", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);
               
                DateTime dat = DateTime.Today; string c;
                com3.Parameters.AddWithValue("dat", dat);
                c = com3.ExecuteScalar().ToString();
                if (c != "0")
                    label8.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }
                rd = com1.ExecuteReader();

                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_mat"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_mat"]));
                    o_cb1.Items.Add(Convert.ToInt32(rd["id_mat"]));
                    p_cb1.Items.Add(Convert.ToInt32(rd["id_mat"]));
                }
            }
        }

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_mat as 'Номер материала',mat_name as 'Название материала', " +
                    "stor_count as 'Количество на складе' from mat_stor", con);
                MySqlCommand com1 = new MySqlCommand("select * from mat_stor", con);
                MySqlCommand com2 = new MySqlCommand("select id_mat from mat_stor", con);
                MySqlCommand com3 = new MySqlCommand("select count(*) from sklad_m where dat = @dat", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);
                DateTime dat = DateTime.Today; string c;
                com3.Parameters.AddWithValue("dat", dat);
                c = com3.ExecuteScalar().ToString();
                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }

                comboBox1.Items.Clear();
                o_cb1.Items.Clear();
                p_cb1.Items.Clear();
                comboBox2.Items.Clear();
                rd = com1.ExecuteReader();

                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_mat"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_mat"]));
                    o_cb1.Items.Add(Convert.ToInt32(rd["id_mat"]));
                    p_cb1.Items.Add(Convert.ToInt32(rd["id_mat"]));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a, b, c,prov = ""; int j = 0; int num; bool isNum1 = int.TryParse(a_box1.Text.Trim(), out num); bool isNum2 = int.TryParse(a_box3.Text.Trim(), out num);
            a = a_box1.Text;
            b = a_box2.Text;
            c = a_box3.Text;

            MySqlCommand com = new MySqlCommand("insert into mat_stor values (@a, @b, @c)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_mat) from mat_stor where id_mat = @a", MC);
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
                "Полe 'Номер материала' не должно содержать буквы и символы! \nПопробуйте еще раз",
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
                        "Материал с таким номером уже существует",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            string ra = @"^[А-Я]";
            Regex reg = new Regex(ra); 
            if (reg.IsMatch(b)==false)
            {
                DialogResult ans = MessageBox.Show(
              "Недопустимое значение поля 'Название материала'!\nНазвание материала должно начинатся с большой буквы",
              "Ошибка",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum2 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Кол-во' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            int c1 = int.Parse(c);
            if (c1 <= 0 || c1 > 1000) //////// PROVERKA NA KOL-VO
            {
                DialogResult ans = MessageBox.Show(
                "Недопустимое значение поля 'Кол-во'. \nПоле 'Кол-во' не может иметь значение больше 1000 или меньше либо равно 0",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Материал на складе','Добавление записей',@time)", MC);
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
            string a, b, c, g, id = "", kol = "", name = ""; int i = 0, j = 0, num; ; bool isNum1 = int.TryParse(e_box1.Text.Trim(), out num); bool isNum2 = int.TryParse(e_box3.Text.Trim(), out num);
            a = e_box1.Text;
            b = e_box2.Text;
            c = e_box3.Text;
            g = Convert.ToString(comboBox1.SelectedItem);

            MySqlCommand com = new MySqlCommand("update mat_stor set id_mat = @a, mat_name = @b, stor_count = @c " +
               "where id_mat = @g", MC);
            MySqlCommand com1 = new MySqlCommand("select id_mat from mat_stor where id_mat = @g", MC);
            MySqlCommand com2 = new MySqlCommand("select mat_name from mat_stor where id_mat = @g", MC);
            MySqlCommand com3 = new MySqlCommand("select stor_count from mat_stor where id_mat = @g", MC);
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
            id = com1.ExecuteScalar().ToString();
            name = com2.ExecuteScalar().ToString();
            kol = com3.ExecuteScalar().ToString();
            if (a.Length == 0)
            {
                a = id;
                i++;
            }
            else
            {
                MySqlCommand com1_1 = new MySqlCommand("select count(id_mat) from mat_stor where id_mat = @a", MC);
                com1_1.Parameters.AddWithValue("a", a);
                if (isNum1 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Номер материала' не должно содержать буквы и символы! \nПопробуйте еще раз",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
                id = com1_1.ExecuteScalar().ToString();
                if (id == "1")
                {
                    DialogResult ans = MessageBox.Show(
                            "Материал с таким номером уже существует",
                            "Ошибка ввода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (b.Length == 0)
            {
                b = name;
                i++;
            }
            else
            {
                string ra = @"^[А-Я]";
                Regex reg = new Regex(ra);
                if (reg.IsMatch(b) == false)
                {
                    DialogResult ans = MessageBox.Show(
                  "Недопустимое значение поля 'Название материала'!\nНазвание материала должно начинатся с большой буквы",
                  "Ошибка",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (c.Length == 0)
            {
                c = kol;
                i++;
            }
            else
            {
                if (isNum2 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Кол-во' не должно содержать буквы и символы! \nПопробуйте еще раз",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
                int c1 = int.Parse(c);
                if (c1 <= 0 || c1 > 1000) //////// PROVERKA NA KOL-VO
                {
                    DialogResult ans = MessageBox.Show(
                    "Недопустимое значение поля 'Кол-во'. \nПоле 'Кол-во' не может иметь значение больше 1000 или меньше либо равно 0",
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
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Материал на складе','Редактирование',@time)", MC);
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
            string a;
            a = Convert.ToString(comboBox2.SelectedItem);

            MySqlCommand com = new MySqlCommand("delete from mat_stor where id_mat = @a", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_mat) from prod_costs where id_mat = @a", MC);
            MySqlCommand com2 = new MySqlCommand("select count(id_mat) from req_costs where id_mat = @a", MC);
            MySqlCommand com0 = new MySqlCommand("select count(id_mat) from waybill_pm where id_mat = @a", MC);
            com0.Parameters.AddWithValue("a", a);
            com1.Parameters.AddWithValue("a", a); com2.Parameters.AddWithValue("a", a);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            string  c1 = com1.ExecuteScalar().ToString(), c2 = com2.ExecuteScalar().ToString(), c3 = com0.ExecuteScalar().ToString();
            int prodc = int.Parse(c1), prodr = int.Parse(c2), wbs = int.Parse(c3);
            if (prodc > 0 || prodr > 0 || wbs > 0)
            {
                DialogResult ans = MessageBox.Show(
               "Этот материал содержиться в других таблицах \nДля удаления необходимо убрать запись о этом материале из всех таблиц и повторить попытку",
               "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Материал на складе','Удаление',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.Parameters.AddWithValue("a", a);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string a, b, c; int num; bool isNum1 = int.TryParse(p_box1.Text.Trim(), out num);
            b = p_box1.Text;
            a = Convert.ToString(p_cb1.SelectedItem);
            MySqlCommand com0 = new MySqlCommand("update mat_stor set stor_count = stor_count + @b where id_mat = @a", MC);
            MySqlCommand com = new MySqlCommand("select stor_count from mat_stor where id_mat = @a", MC);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (a.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
               "Выберите номера изменяемых записей в выпадающих списках!",
               "Внимание",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);
                return;
            }
            if (b.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
                "Поля не могут быть пустыми\nПовторите попытку",
                 "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum1 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Кол-во' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            com.Parameters.AddWithValue("a", a);
            c = com.ExecuteScalar().ToString();
            int c1 = int.Parse(b), c2 = int.Parse(c);
            if (c1 <= 0 || c1 > 1000) //////// PROVERKA NA KOL-VO
            {
                DialogResult ans = MessageBox.Show(
                "Недопустимое значение поля 'Кол-во'. \nПоле 'Кол-во' не может иметь значение больше 1000 или меньше либо равно 0",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            c2 = c2 + c1;
            if (c2 >= 10000)
            {
                DialogResult ans = MessageBox.Show(
              "Недостаточно места на складе. \nКол-во материала на складе не может превышать 9999",
              "Ошибка",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Материал на складе','Поставка',@time)", MC);
            MySqlCommand comc = new MySqlCommand("select count(*) from sklad_m where id_mat = @a", MC);
            MySqlCommand comd = new MySqlCommand("delete from sklad_m where id_mat = @a", MC);
            comc.Parameters.AddWithValue("a", a);
            comd.Parameters.AddWithValue("a", a);
            string cc = comc.ExecuteScalar().ToString();
            if (cc != "0")
                comd.ExecuteNonQuery();
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com0.Parameters.AddWithValue("a", a); com0.Parameters.AddWithValue("b", b);
            com0.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string a, b, c = ""; int num; bool isNum1 = int.TryParse(o_box2.Text.Trim(), out num);
            b = o_box2.Text;
            a = Convert.ToString(o_cb1.SelectedItem);
            MySqlCommand com0 = new MySqlCommand("update mat_stor set stor_count = stor_count - @b where id_mat = @a", MC);
            MySqlCommand com = new MySqlCommand("select stor_count from mat_stor where id_mat = @a", MC);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (a.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
               "Выберите номера изменяемых записей в выпадающих списках!",
               "Внимание",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);
                return;
            }
            if (b.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
                "Поля не могут быть пустыми\nПовторите попытку",
                 "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum1 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Кол-во' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            com.Parameters.AddWithValue("a", a);
            c = com.ExecuteScalar().ToString();
            int c1 = int.Parse(b), c2 = int.Parse(c);
            if (c1 <= 0 || c1 > 1000) //////// PROVERKA NA KOL-VO
            {
                DialogResult ans = MessageBox.Show(
                "Недопустимое значение поля 'Кол-во'. \nПоле 'Кол-во' не может иметь значение больше 1000 или меньше либо равно 0",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            c2 = c2 - c1;
            if (c2 < 0)
            {
                DialogResult ans = MessageBox.Show(
              "Недопустимое значение поля 'Кол-во'. \nНельзя списать материал в большем количестве чем имеется на складе",
              "Ошибка ввода",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Материал на складе','Отпуск',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com0.Parameters.AddWithValue("a", a); com0.Parameters.AddWithValue("b", b);
            com0.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                //  MySqlCommand com = new MySqlCommand("select assortment.prod_name as 'Название товара', prod_stor.stor_count as 'Кол-во на складе', prod_stor.date_man as 'Дата производства' from prod_stor,assortment" +
                //      " where assortment.id_prod = prod_stor.id_prod", con);
                MySqlCommand com = new MySqlCommand("select id_mat as 'Номер товара',count as 'Количество' from sklad_m where dat = @dat", con);
                DateTime dat = new DateTime();
                dat = DateTime.Today;
                com.Parameters.AddWithValue("dat", dat);
                // MySqlCommand com1 = new MySqlCommand("select * from prod_stor", con);
                // MySqlCommand com2 = new MySqlCommand("select id_prod from prod_stor", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt_1.Load(rd);
                if (dt_1.Rows.Count > 0)
                {
                    tabl.DataSource = dt_1;
                }

                // rd = com.ExecuteReader();

                /*while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(rd["Номер товара"]));
                    comboBox2.Items.Add(Convert.ToString(rd["Номер товара"]));
                    o_cb1.Items.Add(Convert.ToString(rd["Номер товара"]));
                    p_cb1.Items.Add(Convert.ToString(rd["Номер товара"]));
                }*/
            }
        }
    }
}
