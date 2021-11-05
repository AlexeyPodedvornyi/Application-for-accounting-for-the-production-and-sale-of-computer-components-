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
    public partial class providerForm : Form
    {
        public providerForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);



        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_prov as 'Номер поставщика',prov_name as 'Название фирмы',"
                    + "address as 'Адрес', dir_FIO as 'ФИО директора',p_numb as 'Номер телефона' from provider", con);
                MySqlCommand com1 = new MySqlCommand("select * from provider", con);
                MySqlCommand com2 = new MySqlCommand("select id_prov from provider", con);
                con.Open();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }

                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                rd = com1.ExecuteReader();

                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_prov"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_prov"]));
                }

            }
        }

        private void providerForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_prov as 'Номер поставщика',prov_name as 'Название фирмы',"
                    + "address as 'Адрес', dir_FIO as 'ФИО директора',p_numb as 'Номер телефона' from provider", con);
                MySqlCommand com1 = new MySqlCommand("select * from provider", con);
                MySqlCommand com2 = new MySqlCommand("select id_prov from provider", con);
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
                    comboBox1.Items.Add(Convert.ToInt32(rd["id_prov"]));
                    comboBox2.Items.Add(Convert.ToInt32(rd["id_prov"]));
                }
            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            string a, b, c, d, f, prov = ""; int num; bool isNum1 = int.TryParse(a_box1.Text.Trim(), out num);
            a = a_box1.Text;
            b = a_box2.Text;
            c = a_box3.Text;
            d = a_box4.Text;
            f = a_box5.Text;

            MySqlCommand com = new MySqlCommand("insert into provider values (@a, @b, @c, @d, @f)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_prov) from provider where id_prov = @a", MC);
            com.Parameters.AddWithValue("a", a);
            com1.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("d", d);
            com.Parameters.AddWithValue("f", f);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (a.Length == 0 || b.Length == 0 || c.Length == 0 || d.Length == 0 || f.Length == 0)
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
                "Полe 'Номер поставщика' не должно содержать буквы и символы! \nПопробуйте еще раз",
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
                        "Поставщик с таким номером уже существует",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            string raa = @"^[А-Я][а-я]+\s[А-Я][а-я]+$", raa1 = @"^[А-Я][а-я]+$",
                ra_s = @"\s";
            Regex regex0 = new Regex(raa);
            Regex regex01 = new Regex(raa1);
            Regex regex_s = new Regex(ra_s);
            if (regex_s.IsMatch(b)) //Если есть пробелы, название состоит из 2-ух слов
            {
                if (regex0.IsMatch(b) == false)
                {
                    DialogResult result = MessageBox.Show(
                    "Недопустимое значение поля 'Название фирмы'!\nНазвание должно начинатся с большой буквы, даже если состоит из 2-ух слов и может содержать только символы кириллицы",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            else  //Если неь пробелов, название состоит из 1 слова
            {
                if (regex01.IsMatch(b) == false)
                {
                    DialogResult result = MessageBox.Show(
                    "Недопустимое значение поля 'Название фирмы'!\nНазвание должно начинатся с большой буквы, даже если состоит из 2-ух слов и может содержать только символы кириллицы",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            string ra_nom = @"^(\+380)[0-9]{9}$",
                ra_ad = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}$", ra_fi = @"^[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+$",
                ra_ad1 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}\w$",
                ra_ad2 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}(/)[0-9]{1,3}$",
                ra_ad21 = @"\s(0)", ra_ad22 = @"\s[1-9]{1,3}(/)(0)$", ra_ad31 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}\w$",
                ra_ad32 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}(/)[0-9]{1,3}$", ra_ad3 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}$",
                ra = @"(/)", ra1 = @"[0-9][а-яё]$", s1, s2; s1 = c.Substring(c.Length - 2); s2 = c.Substring(0, c.Length - (c.Length - 3));
            Regex regex = new Regex(ra1);
            Regex regex1 = new Regex(ra_ad21);
            Regex regex2 = new Regex(ra_ad22);
            if (s2 != "ул." && s2 != "пр.") // esli na4alo stroki ne podhodit po formatu
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Адрес'!\nПеред точкой допустимы только значения 'ул'-улица или 'пр' - проспект",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            if (regex.IsMatch(s1)) //// Esli v konce adresa bukva
            {
                regex = new Regex(ra_ad31);
                if (regex.IsMatch(c))
                {
                    if (regex.IsMatch(c) == false)
                    {
                        DialogResult result = MessageBox.Show(
                       "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad31'",
                       "Ошибка",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                else
                {
                    regex = new Regex(ra_ad1);
                    if (regex.IsMatch(c) == false) // proverka na format ad1 (s bukvoy)
                    {
                        DialogResult result = MessageBox.Show(
                        "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad1'",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
            }
            else //esli net bukvi
            {
                regex = new Regex(ra);
                if (regex.IsMatch(c)) //esli v adrese est drob` (/)
                {
                    if (regex1.IsMatch(c) || regex2.IsMatch(c)) //NUli v drobi ili posle
                    {
                        DialogResult result = MessageBox.Show(
                        "Недопустимое значение поля 'Адрес'!\nНесоответсвие формату 'ad21' или 'ad22'!\nВ поле 'Адрес' недопустимы нулевые значения",
                        "Ошибка",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                        return;
                    }
                    regex = new Regex(ra_ad32);
                    if (regex.IsMatch(c))
                    {
                        if (regex.IsMatch(c) == false)
                        {
                            DialogResult result = MessageBox.Show(
                           "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad32'",
                           "Ошибка",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                    else
                    {
                        regex = new Regex(ra_ad2);
                        if (regex.IsMatch(c) == false) //proverka na format ad2
                        {
                            DialogResult result = MessageBox.Show(
                            "Недопустимое значение поля 'Адрес'!\nНесоответсвие формату 'ad2'",
                            "Ошибка",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                }
                else
                {
                    regex = new Regex(ra_ad3);
                    if (regex.IsMatch(c))
                    {
                        if (regex.IsMatch(c) == false)
                        {
                            DialogResult result = MessageBox.Show(
                           "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad3'",
                           "Ошибка",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                    else
                    {
                        regex = new Regex(ra_ad);
                        if (regex.IsMatch(c) == false) //proverka na format ad
                        {
                            DialogResult result = MessageBox.Show(
                             "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad'",
                             "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1);
                            return;
                        }
                    }
                }
            }
            regex = new Regex(ra_fi);
            if (regex.IsMatch(d) == false)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'ФИ'!\nФамилия и имя не соответсвуют формату 'fi'",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            regex = new Regex(ra_nom);
            s1 = f.Substring(0, f.Length - 9); //+380
            if (f.Length != 13)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона должен иметь длину 13 символов включая код страны",
                "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            if (s1 != "+380")
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона должен начинатся так '+380', если нужен код другой страны - обратитесь к администратору",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            if (regex.IsMatch(f) == false)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона не соответсвует формату 'nom'",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Поставщики','Добавление записей',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            string a, b, c, d, f, g, id = "", firm = "", adr = "", fi = "", tel = ""; int i = 0, j = 0, num; ;
            a = e_box1.Text;
            b = e_box2.Text;
            c = e_box3.Text;
            d = e_box4.Text;
            f = e_box5.Text;
            g = Convert.ToString(comboBox1.SelectedItem);
            bool isNum1 = int.TryParse(e_box1.Text.Trim(), out num);
            MySqlCommand com = new MySqlCommand("update provider set id_prov = @a, prov_name = @b, address = @c, dir_FIO = @d, p_numb = @f " +
               "where id_prov = @g", MC);
            MySqlCommand com1 = new MySqlCommand("select id_prov from provider where id_prov = @g", MC);
            MySqlCommand com2 = new MySqlCommand("select prov_name from provider where id_prov = @g", MC);
            MySqlCommand com3 = new MySqlCommand("select address from provider where id_prov = @g", MC);
            MySqlCommand com4 = new MySqlCommand("select dir_FIO from provider where id_prov = @g", MC);
            MySqlCommand com5 = new MySqlCommand("select p_numb from provider where id_prov = @g", MC);
            com1.Parameters.AddWithValue("g", g);
            com2.Parameters.AddWithValue("g", g);
            com3.Parameters.AddWithValue("g", g);
            com4.Parameters.AddWithValue("g", g);
            com5.Parameters.AddWithValue("g", g);
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
            firm = com2.ExecuteScalar().ToString();
            adr = com3.ExecuteScalar().ToString();
            fi = com4.ExecuteScalar().ToString();
            tel = com5.ExecuteScalar().ToString();
            if (e_box1.TextLength == 0)
            {
                a = id;
                i++;
            }
            else
            {
                MySqlCommand com0 = new MySqlCommand("select count(id_prov) from provider where id_prov = @a", MC);
                com0.Parameters.AddWithValue("a", a);
                if (isNum1 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Номер поставщика' не должно содержать буквы и символы! \nПопробуйте еще раз",
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
                            "Поставщик с таким номером уже существует",
                            "Ошибка ввода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (b.Length == 0)         // 
            {
                b = firm;
                i++;
            }
            else
            {
                Regex_firm(e_box2, ref j);
                if (j == 1)
                    return;
            }
            if (c.Length == 0)         // 
            {
                c = firm;
                i++;
            }
            else
            {
                Regex_adr(e_box3, ref j);
                if (j == 1)
                    return;
            }
            if (d.Length == 0)         // 
            {
                d = firm;
                i++;
            }
            else
            {
                Regex_fi(e_box4, ref j);
                if (j == 1)
                    return;
            }
            if (f.Length == 0)         // 
            {
                f = firm;
                i++;
            }
            else
            {
                Regex_tel(e_box5, ref j);
                if (j == 1)
                    return;
            }
            if (i == 5)
            {
                DialogResult ans = MessageBox.Show(
                "Поля не могут быть пустыми! Необходимо заполнить хотя бы 1-о поле",
                "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Поставщики','Редактирование',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("d", d);
            com.Parameters.AddWithValue("f", f);
            com.Parameters.AddWithValue("g", g);
           
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }
        private void butDel_Click(object sender, EventArgs e)
        {
            string a;
            a = Convert.ToString(comboBox2.SelectedItem);

            MySqlCommand com = new MySqlCommand("delete from provider where id_prov = @a", MC);
            com.Parameters.AddWithValue("a", a);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();

            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Поставщики','Удаление',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }
        public int Regex_firm(TextBox tb, ref int j)    //проверка на фирму
        {
            string raa = @"^[А-Я][а-я]+\s[А-Я][а-я]+$", raa1 = @"^[А-Я][а-я]+$",
                ra_s = @"\s"; j = 0;
            Regex regex0 = new Regex(raa);
            Regex regex01 = new Regex(raa1);
            Regex regex_s = new Regex(ra_s);
            if (regex_s.IsMatch(tb.Text)) //Если есть пробелы, название состоит из 2-ух слов
            {
                if (regex0.IsMatch(tb.Text) == false)
                {
                    DialogResult result = MessageBox.Show(
                    "Недопустимое значение поля 'Название фирмы'!\nНазвание фирмы не соответсвует формату 'raa'",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1); j = 1;
                    return j;
                }
            }
            else  //Если неь пробелов, название состоит из 1 слова
            {
                if (regex01.IsMatch(tb.Text) == false)
                {
                    DialogResult result = MessageBox.Show(
                    "Недопустимое значение поля 'Название фирмы'!\nНазвание фирмы не соответсвует формату 'raa1'",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1); j = 1;
                    return j;
                }
            }
            return j;
        }
        public int Regex_adr(TextBox tb, ref int j) //проверка на адрес
        {
            string
               ra_ad = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}$", ra_ad0 = @"\s[0]",
               ra_ad1 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}\w$",
               ra_ad2 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}(/)[0-9]{1,3}$",
               ra_ad21 = @"\s(0)", ra_ad22 = @"\s[1-9]{1,3}(/)(0)$", ra_ad31 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}\w$",
               ra_ad32 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}(/)[0-9]{1,3}$", ra_ad3 = @"^[у,п,л,р]{2}(.)\s[А-ЯЁ][а-яё]+\s[А-ЯЁ][а-яё]+\s[0-9]{1,3}$",
               ra = @"(/)", ra1 = @"[0-9][а-яё]$", s1, s2; s1 = tb.Text.Substring(tb.Text.Length - 2); s2 = tb.Text.Substring(0, tb.Text.Length - (tb.Text.Length - 3));
            Regex regex = new Regex(ra1);
            Regex regex1 = new Regex(ra_ad21);
            Regex regex2 = new Regex(ra_ad22);
            if (s2 != "ул." && s2 != "пр.") // esli na4alo stroki ne podhodit po formatu
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Адрес'!\nПеред точкой допустимы только значения 'ул'-улица или 'пр' - проспект",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1); j = 1;
                return j;
            }
            if (regex.IsMatch(s1)) //// Esli v konce adresa bukva
            {
                regex = new Regex(ra_ad31);
                if (regex.IsMatch(tb.Text))
                {
                    if (regex.IsMatch(tb.Text) == false)
                    {
                        DialogResult result = MessageBox.Show(
                       "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad31'",
                       "Ошибка",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1); j = 1;
                        return j;
                    }
                }
                else
                {
                    regex = new Regex(ra_ad1);
                    if (regex.IsMatch(tb.Text) == false) // proverka na format ad1 (s bukvoy)
                    {
                        DialogResult result = MessageBox.Show(
                        "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad1'",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1); j = 1;
                        return j;
                    }
                }
            }
            else //esli net bukvi
            {
                regex = new Regex(ra);
                if (regex.IsMatch(tb.Text)) //esli v adrese est drob` (/)
                {
                    if (regex1.IsMatch(tb.Text) || regex2.IsMatch(tb.Text)) //NUli v drobi ili posle
                    {
                        DialogResult result = MessageBox.Show(
                        "Недопустимое значение поля 'Адрес'!\nНесоответсвие формату 'ad21' или 'ad22'!\nВ поле 'Адрес' недопустимы нулевые значения",
                        "Ошибка",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Error,
                         MessageBoxDefaultButton.Button1);
                        j = 1; return j;
                    }
                    regex = new Regex(ra_ad32);
                    if (regex.IsMatch(tb.Text))
                    {
                        if (regex.IsMatch(tb.Text) == false)
                        {
                            DialogResult result = MessageBox.Show(
                           "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad32'",
                           "Ошибка",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1);
                            j = 1; return j;
                        }
                    }
                    else
                    {
                        regex = new Regex(ra_ad2);
                        if (regex.IsMatch(tb.Text) == false) //proverka na format ad2
                        {
                            DialogResult result = MessageBox.Show(
                            "Недопустимое значение поля 'Адрес'!\nНесоответсвие формату 'ad2'",
                            "Ошибка",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error,
                             MessageBoxDefaultButton.Button1);
                            j = 1; return j;
                        }
                    }
                }
                else
                {
                    regex = new Regex(ra_ad3);
                    if (regex.IsMatch(tb.Text))
                    {
                        if (regex.IsMatch(tb.Text) == false)
                        {
                            DialogResult result = MessageBox.Show(
                           "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad3'",
                           "Ошибка",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error,
                           MessageBoxDefaultButton.Button1); j = 1;
                            return j;
                        }
                    }
                    else
                    {
                        regex = new Regex(ra_ad);
                        if (regex.IsMatch(tb.Text) == false) //proverka na format ad
                        {
                            DialogResult result = MessageBox.Show(
                             "Недопустимое значение поля 'Адрес'!\nАдрес не соответсвует формату 'ad'",
                             "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1); j = 1;
                            return j;
                        }
                        regex = new Regex(ra_ad0);
                        if (regex.IsMatch(tb.Text)) //proverka na format ad
                        {
                            DialogResult result = MessageBox.Show(
                             "Недопустимое значение поля 'Адрес'!\nНомер улицы не может начинатся с 0",
                             "Ошибка",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error,
                              MessageBoxDefaultButton.Button1); j = 1;
                            return j;
                        }
                    }
                }
            }
            return j;
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
            string ra_nom = @"^(\+380)[0-9]{9}$",s1;
            Regex regex = new Regex(ra_nom);
            s1 = tb.Text.Substring(0, tb.Text.Length - 9); //+380
            if (tb.Text.Length != 13)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона должен иметь длину 13 символов включая код страны",
                "Ошибка",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1); j = 1; return j;
            }
            if (s1 != "+380")
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона должен начинатся так '+380', если нужен код другой страны - обратитесь к администратору",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1); j = 1; return j;
            }
            if (regex.IsMatch(tb.Text) == false)
            {
                DialogResult result = MessageBox.Show(
                "Недопустимое значение поля 'Номер телефона'!\nНомер телефона не соответсвует формату 'nom'",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1); j = 1; return j;
            }
            return j;
        }
    }
}
