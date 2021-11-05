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
    public partial class prod_storForm : Form
    {
        public prod_storForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void prod_storForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            o_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            p_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            a_box3.Enabled = false;
            e_box3.Enabled = false;
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();
            
            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
              //  MySqlCommand com = new MySqlCommand("select assortment.prod_name as 'Название товара', prod_stor.stor_count as 'Кол-во на складе', prod_stor.date_man as 'Дата производства' from prod_stor,assortment" +
              //      " where assortment.id_prod = prod_stor.id_prod", con);
                MySqlCommand com = new MySqlCommand("select id_prod as 'Номер товара',prod_name as 'Название товара',stor_count as 'Кол-во на складе',date_man as 'Дата производства' from assortment left join prod_stor using (id_prod)", con);
                MySqlCommand com1 = new MySqlCommand("select * from prod_stor", con);
                MySqlCommand com2 = new MySqlCommand("select id_prod from prod_stor", con);
                MySqlCommand com3 = new MySqlCommand("select count(*) from sklad_p where dat = @dat", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);
                DateTime dat = DateTime.Today;string c;
                com3.Parameters.AddWithValue("dat", dat);
                c = com3.ExecuteScalar().ToString();
                if (c != "0")
                    label8.Visible = true;
                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }

                rd = com.ExecuteReader();

                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(rd["Номер товара"]));
                    comboBox2.Items.Add(Convert.ToString(rd["Номер товара"]));
                    o_cb1.Items.Add(Convert.ToString(rd["Номер товара"]));
                    p_cb1.Items.Add(Convert.ToString(rd["Номер товара"]));
                }
            }
        }

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                //  MySqlCommand com = new MySqlCommand("select assortment.prod_name as 'Название товара', prod_stor.stor_count as 'Кол-во на складе', prod_stor.date_man as 'Дата производства' from prod_stor,assortment" +
                //      " where assortment.id_prod = prod_stor.id_prod", con);
                MySqlCommand com = new MySqlCommand("select id_prod as 'Номер товара',prod_name as 'Название товара',stor_count as 'Кол-во на складе',date_man as 'Дата производства' from assortment left join prod_stor using (id_prod)", con);
                MySqlCommand com1 = new MySqlCommand("select * from prod_stor", con);
                MySqlCommand com2 = new MySqlCommand("select id_prod from prod_stor", con);
                MySqlCommand com3 = new MySqlCommand("select count(*) from sklad_p where dat = @dat", con);
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

                rd = com.ExecuteReader();

                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(rd["Номер товара"]));
                    comboBox2.Items.Add(Convert.ToString(rd["Номер товара"]));
                    o_cb1.Items.Add(Convert.ToString(rd["Номер товара"]));
                    p_cb1.Items.Add(Convert.ToString(rd["Номер товара"]));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name,cost,a, b, c,prov = ""; int j = 0; int num; bool isNum2 = int.TryParse(a_box2.Text.Trim(), out num);
            a = a_box1.Text; bool isNum1 = int.TryParse(a_box1.Text.Trim(), out num);
            b = a_box2.Text;
            c = a_box3.Text;

            MySqlCommand com = new MySqlCommand("insert into prod_stor values (@a, @b, @c)", MC);
            MySqlCommand coma = new MySqlCommand("insert into assortment values (@name, @a, @cost)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_prod) from assortment where id_prod = @a", MC);
           // MySqlCommand com2 = new MySqlCommand("select id_prod from assortment where prod_name = @a", MC);
            
            //com2.Parameters.AddWithValue("a", a);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            //a = com2.ExecuteScalar().ToString();
            com1.Parameters.AddWithValue("a", a);
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
                "Полe 'Номер товара' не должно содержать буквы и символы! \nПопробуйте еще раз",
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
                        "Товар с таким номером уже существует",
                        "Ошибка ввода",
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
            int c1 = int.Parse(b);
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
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Товар на складе','Добавление записей',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);
            name = "Без имени " + "(id-"+a+")";
            cost = "0";
            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            coma.Parameters.AddWithValue("a", a);
            coma.Parameters.AddWithValue("name", name);
            coma.Parameters.AddWithValue("cost", cost);
            com.ExecuteNonQuery(); coma.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a,name,cost, b, c, g, id = "", kol = "", dt = ""; int i = 0, j = 0, num; ; bool isNum1 = int.TryParse(e_box1.Text.Trim(), out num); bool isNum2 = int.TryParse(e_box2.Text.Trim(), out num);
            a = e_box1.Text;
            b = e_box2.Text;
            c = e_box3.Text;
            g = Convert.ToString(comboBox1.SelectedItem);

            MySqlCommand com = new MySqlCommand("update prod_stor set id_prod = @a, stor_count = @b, date_man = @c " +
               "where id_prod = @g", MC);
            MySqlCommand com1 = new MySqlCommand("select id_prod from prod_stor where id_prod = @g", MC);
            MySqlCommand com2 = new MySqlCommand("select stor_count from prod_stor where id_prod = @g", MC);
            MySqlCommand com3 = new MySqlCommand("select date_man from prod_stor where id_prod = @g", MC);
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
            kol = com2.ExecuteScalar().ToString();
            dt = com3.ExecuteScalar().ToString();
            if (a.Length == 0)
            {
                a = id;
                i++;
            }
            else
            {
                MySqlCommand com1_1 = new MySqlCommand("select count(id_prod) from prod_stor where id_prod = @a", MC);
                com1_1.Parameters.AddWithValue("a", a);
                if (isNum1 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Номер товара' не должно содержать буквы и символы! \nПопробуйте еще раз",
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
                            "Товар с таким номером уже существует",
                            "Ошибка ввода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (b.Length == 0)
            {
                b = kol;
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
                int c1 = int.Parse(b);
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
            if (c.Length == 0)
            {
                c = dt;
                i++;
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
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Товар на складе','Редактирование',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);
            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("g", g);
            com.Parameters.AddWithValue("a", a);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string a;
            a = Convert.ToString(comboBox2.SelectedItem);

            MySqlCommand com = new MySqlCommand("delete from prod_stor where id_prod = @a", MC);
            MySqlCommand com11 = new MySqlCommand("select count(id_prod) from assortment where id_prod = @a", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_prod) from prod_costs where id_prod = @a", MC);
            MySqlCommand com2 = new MySqlCommand("select count(id_prod) from prod_req where id_prod = @a", MC);
            MySqlCommand com0 = new MySqlCommand("select count(id_prod) from waybill_sp where id_prod = @a", MC);
            com0.Parameters.AddWithValue("a", a);
            com11.Parameters.AddWithValue("a", a); com1.Parameters.AddWithValue("a", a); com2.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("a", a);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            string c = com11.ExecuteScalar().ToString(), c1 = com1.ExecuteScalar().ToString(), c2 = com2.ExecuteScalar().ToString(), c3 = com0.ExecuteScalar().ToString();
            int asort = int.Parse(c), prodc = int.Parse(c1), prodr = int.Parse(c2), wbs = int.Parse(c3);
            if(asort >0 || prodc>0 ||prodr>0 ||wbs>0)
            {
                DialogResult ans = MessageBox.Show(
               "Этот товар содержиться в других таблицах \nДля удаления необходимо убрать запись о этом товаре из всех таблиц и повторить попытку",
               "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com112 = new MySqlCommand("insert into log values (@log,'Товар на складе','Удаление',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com112.Parameters.AddWithValue("time", time);
            com112.Parameters.AddWithValue("log", log);
            com.ExecuteNonQuery(); com112.ExecuteNonQuery();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (monthCalendar1.Enabled == true || monthCalendar1.Visible == true)
            {
                monthCalendar1.Enabled = false;
                monthCalendar1.Visible = false;
            }
            else
            {
                monthCalendar1.Enabled = true;
                monthCalendar1.Visible = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (monthCalendar1.Enabled == true || monthCalendar1.Visible == true)
            {
                monthCalendar1.Enabled = false;
                monthCalendar1.Visible = false;
            }
            else
            {
                monthCalendar1.Enabled = true;
                monthCalendar1.Visible = true;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            a_box3.Text = "";
            e_box3.Text = "";
            monthCalendar1.Enabled = false;
            monthCalendar1.Visible = false;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            string date, y, m, day, w; int i = 0; DateTime dat = new DateTime();
            string[] m_numb = new string[12] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            string[] m_name = new string[12] { "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };
            string[] m_name1 = new string[12] { "нваря", "евраля", "арта", "преля", "ая", "юня", "юля", "вгуста", "ентября", "ктября", "оября", "екабря" };
            date = monthCalendar1.SelectionStart.ToLongDateString();
            dat = monthCalendar1.SelectionStart;
            if (dat < DateTime.Today)
            {
                DialogResult ans = MessageBox.Show(
                 "Нельзя указать дату меньше чем сегодняшняя дата!\nПовторите попытку",
                  "Внимание",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning,
                  MessageBoxDefaultButton.Button1);

                return;
            }
            y = date.Substring(date.Length - 7);
            y = y.Substring(0, y.Length - 5);
            m = date.Substring(3);
            m = m.Substring(0, m.Length - 8);
            for (i = 0; i < m_name.Length; i++)
            {
                if (m == m_name[i] || m == m_name1[i])
                    m = m_numb[i].ToString();
            }
            int j = date.Length + 2;
            day = date.Substring(0, j - date.Length);
            if (day == "1 ")
                day = m_numb[0];
            if (day == "2 ")
                day = m_numb[1];
            if (day == "3 ")
                day = m_numb[2];
            if (day == "4 ")
                day = m_numb[3];
            if (day == "5 ")
                day = m_numb[4];
            if (day == "6 ")
                day = m_numb[5];
            if (day == "7 ")
                day = m_numb[6];
            if (day == "8 ")
                day = m_numb[7];
            if (day == "9 ")
                day = m_numb[8];

            w = y + "." + m + "." + day;

            a_box3.Text = w;
            e_box3.Text = w;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string a, b,c; int num; bool isNum1 = int.TryParse(p_box1.Text.Trim(), out num);
            b = p_box1.Text;
            a = Convert.ToString(p_cb1.SelectedItem);
            MySqlCommand com0 = new MySqlCommand("update prod_stor set stor_count = stor_count + @b where id_prod = @a", MC);
            MySqlCommand com = new MySqlCommand("select stor_count from prod_stor where id_prod = @a", MC);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if(a.Length ==0)
            {
                DialogResult ans = MessageBox.Show(
               "Выберите номера изменяемых записей в выпадающих списках!",
               "Внимание",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);
                return;
            }
            if(b.Length == 0)
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
              "Недостаточно места на складе. \nКол-во товара на складе не может превышать 9999",
              "Ошибка",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Товар на складе','Поставка',@time)", MC);
            MySqlCommand comc = new MySqlCommand("select count(*) from sklad_p where id_prod = @a", MC);
            MySqlCommand comd = new MySqlCommand("delete from sklad_p where id_prod = @a", MC);
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
            string a, b,c = ""; int num; bool isNum1 = int.TryParse(o_box2.Text.Trim(), out num);
            b = o_box2.Text;
            a = Convert.ToString(o_cb1.SelectedItem);
            MySqlCommand com0 = new MySqlCommand("update prod_stor set stor_count = stor_count - @b where id_prod = @a", MC);
            MySqlCommand com = new MySqlCommand("select stor_count from prod_stor where id_prod = @a", MC);
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
            if(c2<0)
            {
                DialogResult ans = MessageBox.Show(
              "Недопустимое значение поля 'Кол-во'. \nНельзя списать товар в большем количестве чем имеется на складе",
              "Ошибка ввода",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Товар на складе','Отпуск',@time)", MC);
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
                MySqlCommand com = new MySqlCommand("select id_prod as 'Номер товара',count as 'Количество' from sklad_p where dat = @dat", con);
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
