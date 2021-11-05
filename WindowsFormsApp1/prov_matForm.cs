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
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class prov_matForm : Form
    {
        public prov_matForm()
        {
            InitializeComponent();
        }
        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);
        private void prov_matForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; ////////// ЗАПРЕТ РУЧНОГО ВВОДА В ВЫПАДАЮЩИЙ СПИСОК
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            a_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            a_cb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            e_cb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            e_cb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();


            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com1_1 = new MySqlCommand("select prov_name from provider", con);
                MySqlCommand com1_11 = new MySqlCommand("select provider.prov_name from prov_mat,provider where prov_mat.id_prov = provider.id_prov group by prov_mat.id_prov", con);
                MySqlCommand com1_2 = new MySqlCommand("select mat_name from mat_stor", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table prov_matt(id_prov varchar(40),id_mat varchar(40),dt int(3), cost_1 bigint);", con);
                MySqlCommand com1 = new MySqlCommand("insert into prov_matt SELECT provider.prov_name , mat_stor.mat_name , prov_mat.dt , prov_mat.cost_1 " +
                    " FROM prov_mat, mat_stor,provider WHERE mat_stor.id_mat = prov_mat.id_mat and provider.id_prov = prov_mat.id_prov;", con);
                MySqlCommand com = new MySqlCommand("select id_prov as 'Поставщик', id_mat as 'Название материала', dt as 'Время поставки(дни)',cost_1 as'Цена за шт.' from prov_matt", con);
                MySqlCommand com3 = new MySqlCommand("drop table prov_matt", con);

                con.Open();
                com2.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);
                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }
                else
                    label12.Visible = true;

                rd = com1_11.ExecuteReader();
                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(rd["prov_name"]));
                    comboBox3.Items.Add(Convert.ToString(rd["prov_name"]));
                }
                rd.Close();
                rd = com1_1.ExecuteReader();
                while (rd.Read())
                {
                    a_cb1.Items.Add(Convert.ToString(rd["prov_name"]));
                    e_cb1.Items.Add(Convert.ToString(rd["prov_name"]));
                }
                rd.Close();
                rd = com1_2.ExecuteReader();
                while (rd.Read())
                {
                    a_cb2.Items.Add(Convert.ToString(rd["mat_name"]));
                    e_cb2.Items.Add(Convert.ToString(rd["mat_name"]));
                }
                rd.Close();
                com3.ExecuteNonQuery();

            }
        }
        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com1_1 = new MySqlCommand("select prov_name from provider", con);
                MySqlCommand com1_11 = new MySqlCommand("select provider.prov_name from prov_mat,provider where prov_mat.id_prov = provider.id_prov group by prov_mat.id_prov", con);
                MySqlCommand com1_2 = new MySqlCommand("select mat_name from mat_stor", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table prov_matt(id_prov varchar(40),id_mat varchar(40),dt int(3), cost_1 bigint);", con);
                MySqlCommand com1 = new MySqlCommand("insert into prov_matt SELECT provider.prov_name , mat_stor.mat_name , prov_mat.dt , prov_mat.cost_1 " +
                    " FROM prov_mat, mat_stor,provider WHERE mat_stor.id_mat = prov_mat.id_mat and provider.id_prov = prov_mat.id_prov;", con);
                MySqlCommand com = new MySqlCommand("select id_prov as 'Поставщик', id_mat as 'Название материала', dt as 'Время поставки(дни)',cost_1 as'Цена за шт.' from prov_matt", con);
                MySqlCommand com3 = new MySqlCommand("drop table prov_matt", con);

                con.Open();
                com2.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);
                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }
                else
                    label12.Visible = true;
                
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
                a_cb1.Items.Clear();
                a_cb2.Items.Clear();
                e_cb1.Items.Clear();
                e_cb2.Items.Clear();
                rd = com1_11.ExecuteReader();
                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(rd["prov_name"]));
                    comboBox3.Items.Add(Convert.ToString(rd["prov_name"]));
                }
                rd.Close();
                rd = com1_1.ExecuteReader();
                while (rd.Read())
                {
                    a_cb1.Items.Add(Convert.ToString(rd["prov_name"]));
                    e_cb1.Items.Add(Convert.ToString(rd["prov_name"]));
                }
                rd.Close();
                rd = com1_2.ExecuteReader();
                while (rd.Read())
                {
                    a_cb2.Items.Add(Convert.ToString(rd["mat_name"]));
                    e_cb2.Items.Add(Convert.ToString(rd["mat_name"]));
                }
                rd.Close();
                com3.ExecuteNonQuery();
            }
        }

        private void butAdd_Click(object sender, EventArgs e)
        {
            string a, b, c, f, seb = "", prov; int num; bool isNum3 = int.TryParse(a_box3.Text.Trim(), out num); bool isNum4 = int.TryParse(a_box4.Text.Trim(), out num);
            a = Convert.ToString(a_cb1.SelectedItem);
            b = Convert.ToString(a_cb2.SelectedItem);
            c = a_box3.Text;
            f = a_box4.Text;

            MySqlCommand com = new MySqlCommand("insert into prov_mat values (@a, @b, @c, @f)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_prov) from prov_mat where id_prov = @a and id_mat = @b", MC);
            MySqlCommand com2 = new MySqlCommand("select id_prov from provider where prov_name = @a", MC);
            MySqlCommand com3 = new MySqlCommand("select id_mat from mat_stor where mat_name = @b", MC);
            com2.Parameters.AddWithValue("a", a);
            com3.Parameters.AddWithValue("b", b);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            a = com2.ExecuteScalar().ToString();
            b = com3.ExecuteScalar().ToString();
            //MessageBox.Show(a,b);
            com1.Parameters.AddWithValue("a", a);
            com1.Parameters.AddWithValue("b", b);

            if (a.Length == 0 || b.Length == 0 || c.Length == 0 || f.Length == 0)
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
                        "Запись с таким поставщиком и материалом уже существует",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum3 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Время поставки' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            int c1 = int.Parse(c);
            if (c1 > 100 || c1 <= 0) //////// PROVERKA NA KOL-VO
            {
                DialogResult ans = MessageBox.Show(
                "Недопустимое количество. \nПоле 'Время поставки' не может иметь значение больше 100 или меньше либо равно 0",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum4 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Стоимость/шт.' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            string regseb1 = @"^(0)", s1, s2;
            Regex regex = new Regex(regseb1);
            if (regex.IsMatch(f))
            {
                DialogResult ans = MessageBox.Show(
               "Недопустимое значение поля 'Стоимость/шт.'. \nСтоимость/шт. не может начинаться с 0",
               "Ошибка ввода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }

            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Поставляемые материалы','Добавление записей',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);

            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("f", f);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            string a, a1, b, b1, c, d, f, g, prov = "", mat = "", kolv = "", seb = ""; int i = 0, num; bool isNum3 = int.TryParse(e_box3.Text.Trim(), out num);
            a = Convert.ToString(e_cb1.SelectedItem); bool isNum4 = int.TryParse(e_box4.Text.Trim(), out num);
            b = Convert.ToString(e_cb2.SelectedItem);
            c = e_box3.Text;
            g = e_box4.Text;
            d = Convert.ToString(comboBox1.SelectedItem);
            f = Convert.ToString(comboBox2.SelectedItem);


            MySqlCommand com = new MySqlCommand("update prov_mat set id_prov = @a, id_mat = @b, dt = @c,cost_1 = @g  " +
               "where id_prov = @d and id_mat = @f", MC);
            MySqlCommand com1 = new MySqlCommand("select id_prov from prov_mat where id_prov = @d and id_mat = @f", MC);
            MySqlCommand com2 = new MySqlCommand("select id_mat from prov_mat where id_prov = @d and id_mat = @f", MC);
            MySqlCommand com3 = new MySqlCommand("select dt from prov_mat where id_prov = @d and id_mat = @f", MC);
            MySqlCommand com4 = new MySqlCommand("select cost_1 from prov_mat where id_prov = @d and id_mat = @f", MC);
            MySqlCommand com00 = new MySqlCommand("select id_prov from provider where prov_name = @d", MC);
            MySqlCommand com01 = new MySqlCommand("select id_mat from mat_stor where mat_name = @f", MC);
            MySqlCommand com02 = new MySqlCommand("select id_prov from provider where prov_name = @d", MC);
            MySqlCommand com03 = new MySqlCommand("select id_mat from mat_stor where mat_name = @f", MC);
            com00.Parameters.AddWithValue("d", d);
            com01.Parameters.AddWithValue("f", f);
            com02.Parameters.AddWithValue("d", d);
            com03.Parameters.AddWithValue("f", f);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (d.Length == 0 || f.Length == 0)
            {
                DialogResult ans = MessageBox.Show(
                "Выберите номера изменяемых записей в выпадающих списках!",
                "Внимание",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            a1 = d; b1 = f;
            a = com00.ExecuteScalar().ToString();
            b = com01.ExecuteScalar().ToString();
            d = com02.ExecuteScalar().ToString();
            f = com03.ExecuteScalar().ToString();
            com1.Parameters.AddWithValue("d", d);
            com2.Parameters.AddWithValue("d", d);
            com3.Parameters.AddWithValue("d", d);
            com4.Parameters.AddWithValue("d", d);
            com1.Parameters.AddWithValue("f", f);
            com2.Parameters.AddWithValue("f", f);
            com3.Parameters.AddWithValue("f", f);
            com4.Parameters.AddWithValue("f", f);
            prov = com1.ExecuteScalar().ToString(); //null reference ...
            mat = com2.ExecuteScalar().ToString();
            kolv = com3.ExecuteScalar().ToString();
            seb = com4.ExecuteScalar().ToString();
            if (e_cb1.Text.Length == 0)
            {
                a = prov;
                i++;
            }
            else
            {
                if (b.Length == 0)
                    b = mat;
                MySqlCommand com0 = new MySqlCommand("select count(id_prov) from prov_mat where id_prov = @a and id_mat = @b", MC);
                com0.Parameters.AddWithValue("a", a); com0.Parameters.AddWithValue("b", b);
                prov = com0.ExecuteScalar().ToString();
                if (prov == "1")
                {
                    DialogResult ans = MessageBox.Show(
                            "Запись с таким поставщиком и материалом уже существует",
                            "Ошибка ввода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    return;
                }

            }
            if (e_cb2.Text.Length == 0)         // 
            {
                b = mat;
                i++;
            }
            else
            {
                MySqlCommand com0 = new MySqlCommand("select count(id_prov) from prov_mat where id_prov = @a and id_mat = @b", MC);
                com0.Parameters.AddWithValue("a", a); com0.Parameters.AddWithValue("b", b);
                prov = com0.ExecuteScalar().ToString();
                if (prov == "1")
                {
                    DialogResult ans = MessageBox.Show(
                            "Запись с таким поставщиком и материалом уже существует",
                            "Ошибка ввода",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    return;
                }

            }
            if (c.Length == 0)         // 
            {
                c = kolv;
                i++;
            }
            else
            {
                if (isNum3 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Время поставки' не должно содержать буквы и символы! \nПопробуйте еще раз",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
                int c1 = int.Parse(c);
                if (c1 > 100 || c1 <= 0) //////// PROVERKA NA KOL-VO
                {
                    DialogResult ans = MessageBox.Show(
                    "Недопустимое количество. \nПоле 'Время поставки' не может иметь значение больше 100 или меньше либо равно 0",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            if (g.Length == 0)         // 
            {
                g = seb;
                i++;
            }
            else
            {
                if (isNum4 == false)
                {
                    DialogResult ans = MessageBox.Show(
                    "Полe 'Стоимость/шт.' не должно содержать буквы и символы! \nПопробуйте еще раз",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
                string regseb1 = @"^(0)", s1, s2;
                Regex regex = new Regex(regseb1);
                if (regex.IsMatch(f))
                {
                    DialogResult ans = MessageBox.Show(
                   "Недопустимое значение поля 'Стоимость/шт.'. \nСтоимость/шт. не может начинаться с 0",
                   "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                    return;
                }
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
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Поставляемые материалы','Редактирование',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);
            if (a.Length == 0)
                a = a1;
            if (b.Length == 0)
                b = b1;
            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.Parameters.AddWithValue("c", c);
            com.Parameters.AddWithValue("g", g);
            com.Parameters.AddWithValue("d", d);
            com.Parameters.AddWithValue("f", f);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            string a, b;
            a = Convert.ToString(comboBox3.SelectedItem);
            b = Convert.ToString(comboBox4.SelectedItem);

            MySqlCommand com = new MySqlCommand("delete from prov_mat where id_prov = @a and id_mat = @b", MC);
            MySqlCommand com2 = new MySqlCommand("select id_prov from provider where prov_name = @a", MC);
            MySqlCommand com3 = new MySqlCommand("select id_mat from mat_stor where mat_name = @b", MC);
            com2.Parameters.AddWithValue("a", a);
            com3.Parameters.AddWithValue("b", b);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            a = com2.ExecuteScalar().ToString();
            b = com3.ExecuteScalar().ToString();
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Поставляемые материалы','Удаление',@time)", MC);
            string log = ""; DateTime time = new DateTime();
            log = Autor_R.current_login;
            time = DateTime.Now;
            com11.Parameters.AddWithValue("time", time);
            com11.Parameters.AddWithValue("log", log);
            com.Parameters.AddWithValue("a", a);
            com.Parameters.AddWithValue("b", b);
            com.ExecuteNonQuery(); com11.ExecuteNonQuery();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string z = Convert.ToString(comboBox1.SelectedItem);
            // string z = "";
            //MessageBox.Show(i);

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table prov_matt(id_prov varchar(40),id_mat varchar(40),dt int(3), cost_1 bigint);", con);
                MySqlCommand com12 = new MySqlCommand("insert into prov_matt SELECT provider.prov_name , mat_stor.mat_name , prov_mat.dt , prov_mat.cost_1 " +
                    " FROM prov_mat, mat_stor,provider WHERE mat_stor.id_mat = prov_mat.id_mat and provider.id_prov = prov_mat.id_prov;", con);
                // MySqlCommand com12 = new MySqlCommand("insert into prod_costst SELECT assortment.prod_name, mat_stor.mat_name, prod_costs.one_pice" +
                //   " FROM prod_costs, mat_stor, assortment WHERE mat_stor.id_mat = prod_costs.id_mat and assortment.id_prod = prod_costs.id_prod ", con);
                MySqlCommand com = new MySqlCommand("select id_mat from prov_matt where id_prov = @z", con);
                MySqlCommand com3 = new MySqlCommand("drop table prov_matt", con);
                con.Open();
                com2.ExecuteNonQuery();
                com12.ExecuteNonQuery();
                com.Parameters.AddWithValue("z", z);
                MySqlDataReader rd = com.ExecuteReader();

                while (rd.Read())
                {
                    comboBox2.Items.Add(Convert.ToString(rd["id_mat"]));
                }
                rd.Close();
                com3.ExecuteNonQuery();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            string z = Convert.ToString(comboBox3.SelectedItem);
            // string z = "";
            //MessageBox.Show(i);

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table prov_matt(id_prov varchar(40),id_mat varchar(40),dt int(3), cost_1 bigint);", con);
                MySqlCommand com12 = new MySqlCommand("insert into prov_matt SELECT provider.prov_name , mat_stor.mat_name , prov_mat.dt , prov_mat.cost_1 " +
                    " FROM prov_mat, mat_stor,provider WHERE mat_stor.id_mat = prov_mat.id_mat and provider.id_prov = prov_mat.id_prov;", con);
                // MySqlCommand com12 = new MySqlCommand("insert into prod_costst SELECT assortment.prod_name, mat_stor.mat_name, prod_costs.one_pice" +
                //   " FROM prod_costs, mat_stor, assortment WHERE mat_stor.id_mat = prod_costs.id_mat and assortment.id_prod = prod_costs.id_prod ", con);
                MySqlCommand com = new MySqlCommand("select id_mat from prov_matt where id_prov = @z", con);
                MySqlCommand com3 = new MySqlCommand("drop table prov_matt", con);
                con.Open();
                com2.ExecuteNonQuery();
                com12.ExecuteNonQuery();
                com.Parameters.AddWithValue("z", z);
                MySqlDataReader rd = com.ExecuteReader();

                while (rd.Read())
                {
                    comboBox4.Items.Add(Convert.ToString(rd["id_mat"]));
                }
                rd.Close();
                com3.ExecuteNonQuery();
            }
        }
    }
}
