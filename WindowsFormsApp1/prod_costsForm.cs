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
    public partial class form : Form
    {
        public form()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void form_Load(object sender, EventArgs e)
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
               // MySqlCommand com = new MySqlCommand("select id_prod as 'Номер товара',id_mat as 'Номер материала', one_pice as 'Кол-во на шт. товара', self_cost as 'Себестоимость' from prod_costs ", con);
                MySqlCommand com1 = new MySqlCommand("select assortment.prod_name from prod_costs,assortment where prod_costs.id_prod = assortment.id_prod group by prod_costs.id_prod", con);
                MySqlCommand com1_1 = new MySqlCommand("select assortment.prod_name from prod_costs,assortment where prod_costs.id_prod = assortment.id_prod group by prod_costs.id_prod", con);
                MySqlCommand com1_2 = new MySqlCommand("select mat_stor.mat_name from mat_stor,prod_costs where prod_costs.id_mat = mat_stor.id_mat group by mat_stor.mat_name", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table prod_costst(id_req varchar(40),id_mat varchar(40),reqm_count int(3),self_cost int(3));", con);
                MySqlCommand com12 = new MySqlCommand("insert into prod_costst SELECT assortment.prod_name AS 'Номер товар', mat_stor.mat_name AS 'Название материала', prod_costs.one_pice AS 'Количество материала',  prod_costs.self_cost AS 'Себестоимость'"+
                    " FROM prod_costs, mat_stor, assortment WHERE mat_stor.id_mat = prod_costs.id_mat and assortment.id_prod = prod_costs.id_prod ", con);
                MySqlCommand com = new MySqlCommand("select id_req as 'Название товара',id_mat as 'Номер материала', reqm_count as 'Кол-во на шт. товара', self_cost as 'Себестоимость' from prod_costst order by id_req", con);
                MySqlCommand com3 = new MySqlCommand("drop table prod_costst", con);
                con.Open();
                com2.ExecuteNonQuery();
                com12.ExecuteNonQuery();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }
                else
                    label12.Visible = true;
                rd = com1.ExecuteReader();
                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(rd["prod_name"]));
                    comboBox3.Items.Add(Convert.ToString(rd["prod_name"]));
                }
                rd.Close();
                rd = com1_1.ExecuteReader();
                while (rd.Read())
                {
                    a_cb1.Items.Add(Convert.ToString(rd["prod_name"]));
                    e_cb1.Items.Add(Convert.ToString(rd["prod_name"]));
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
                MySqlCommand com1 = new MySqlCommand("select assortment.prod_name from prod_costs,assortment where prod_costs.id_prod = assortment.id_prod group by prod_costs.id_prod", con);
                MySqlCommand com1_1 = new MySqlCommand("select assortment.prod_name from prod_costs,assortment where prod_costs.id_prod = assortment.id_prod group by prod_costs.id_prod", con);
                MySqlCommand com1_2 = new MySqlCommand("select mat_stor.mat_name from mat_stor,prod_costs where prod_costs.id_mat = mat_stor.id_mat group by mat_stor.mat_name", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table prod_costst(id_req varchar(40),id_mat varchar(40),reqm_count int(3),self_cost int(3));", con);
                MySqlCommand com12 = new MySqlCommand("insert into prod_costst SELECT assortment.prod_name AS 'Номер товар', mat_stor.mat_name AS 'Название материала', prod_costs.one_pice AS 'Количество материала',  prod_costs.self_cost AS 'Себестоимость'" +
                    " FROM prod_costs, mat_stor, assortment WHERE mat_stor.id_mat = prod_costs.id_mat and assortment.id_prod = prod_costs.id_prod ", con);
                MySqlCommand com = new MySqlCommand("select id_req as 'Название товара',id_mat as 'Номер материала', reqm_count as 'Кол-во на шт. товара', self_cost as 'Себестоимость' from prod_costst order by id_req", con);
                MySqlCommand com3 = new MySqlCommand("drop table prod_costst", con);
                con.Open();
                com2.ExecuteNonQuery();
                com12.ExecuteNonQuery();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }
                else
                    label12.Visible = true;
                rd = com1.ExecuteReader();
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
                a_cb1.Items.Clear();
                a_cb2.Items.Clear();
                e_cb1.Items.Clear();
                e_cb2.Items.Clear();
                while (rd.Read())
                {
                    comboBox1.Items.Add(Convert.ToString(rd["prod_name"]));
                    comboBox3.Items.Add(Convert.ToString(rd["prod_name"]));
                }
                rd.Close();
                rd = com1_1.ExecuteReader();
                while (rd.Read())
                {
                    a_cb1.Items.Add(Convert.ToString(rd["prod_name"]));
                    e_cb1.Items.Add(Convert.ToString(rd["prod_name"]));
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
            string a, b, c, f, seb = "", prov; int num;bool isNum3 = int.TryParse(a_box3.Text.Trim(), out num); bool isNum4 = int.TryParse(a_box4.Text.Trim(), out num);
            a = Convert.ToString(a_cb1.SelectedItem);
            b = Convert.ToString(a_cb2.SelectedItem);
            c = a_box3.Text;
            f = a_box4.Text;

            MySqlCommand com = new MySqlCommand("insert into prod_costs values (@b, @a, @c, @f)", MC);
            MySqlCommand com1 = new MySqlCommand("select count(id_prod) from prod_costs where id_prod = @a and id_mat = @b", MC);
            MySqlCommand com2 = new MySqlCommand("select id_prod from assortment where prod_name = @a", MC);
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

            if (a.Length == 0 || b.Length == 0 || c.Length == 0  || f.Length == 0)
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
                        "Запись с таким название товара и материала уже существует",
                        "Ошибка ввода",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum3 == false)
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
            if (c1 > 100 || c1 <= 0) //////// PROVERKA NA KOL-VO
            {
                DialogResult ans = MessageBox.Show(
                "Недопустимое количество. \nПоле 'Количество' не может иметь значение больше 100 или меньше либо равно 0",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            if (isNum4 == false)
            {
                DialogResult ans = MessageBox.Show(
                "Полe 'Себестоимость' не должно содержать буквы и символы! \nПопробуйте еще раз",
                "Ошибка ввода",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1);
                return;
            }
            string  regseb1 = @"^(0)" ,s1,s2;
            Regex regex = new Regex(regseb1);
            if(regex.IsMatch(f))
            {
                DialogResult ans = MessageBox.Show(
               "Недопустимое значение поля 'Себестоимость'. \nСебестоимость не может начинаться с 0",
               "Ошибка ввода",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
                return;
            }

           
            MySqlCommand com0 = new MySqlCommand("select self_cost from prod_costs where id_prod = @a", MC);
            MySqlCommand comc = new MySqlCommand("select count(*) from prod_costs where id_prod = @a", MC);
            com0.Parameters.AddWithValue("a", a);
            comc.Parameters.AddWithValue("a", a);
           
            s1 = comc.ExecuteScalar().ToString();
            if (s1 != "0")
            {
                seb = com0.ExecuteScalar().ToString();
                if (f != seb)
                {
                    DialogResult ans = MessageBox.Show(
                  "Недопустимое значение поля 'Себестоимость'. \nСебестоимость для одного и того же товара не может иметь разные значения",
                  "Ошибка ввода",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1);
                    return;
                }
            }
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Затраты на производство','Добавление записей',@time)", MC);
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
            string a,a1, b,b1, c, d, f, g, prod = "", mat = "", kolv = "", seb = ""; int i = 0,num; bool isNum3 = int.TryParse(e_box3.Text.Trim(), out num);
            a = Convert.ToString(e_cb1.SelectedItem);  bool isNum4 = int.TryParse(e_box4.Text.Trim(), out num);
            b = Convert.ToString(e_cb2.SelectedItem);
            c = e_box3.Text;
            g = e_box4.Text;
            d = Convert.ToString(comboBox1.SelectedItem);
            f = Convert.ToString(comboBox2.SelectedItem);


            MySqlCommand com = new MySqlCommand("update prod_costs set id_prod = @a, id_mat = @b, one_pice = @c, self_cost = @g  " +
               "where id_prod = @d and id_mat = @f", MC);
            MySqlCommand com1 = new MySqlCommand("select id_prod from prod_costs where id_prod = @d and id_mat = @f", MC);
            MySqlCommand com2 = new MySqlCommand("select id_mat from prod_costs where id_prod = @d and id_mat = @f", MC);
            MySqlCommand com3 = new MySqlCommand("select one_pice from prod_costs where id_prod = @d and id_mat = @f", MC);
            MySqlCommand com4 = new MySqlCommand("select self_cost from prod_costs where id_prod = @d and id_mat = @f", MC);
            MySqlCommand com00 = new MySqlCommand("select id_prod from assortment where prod_name = @d", MC);
            MySqlCommand com01 = new MySqlCommand("select id_mat from mat_stor where mat_name = @f", MC);
            MySqlCommand com02 = new MySqlCommand("select id_prod from assortment where prod_name = @d", MC);
            MySqlCommand com03 = new MySqlCommand("select id_mat from mat_stor where mat_name = @f", MC);
            com00.Parameters.AddWithValue("d", d);
            com01.Parameters.AddWithValue("f", f);
            com02.Parameters.AddWithValue("d", d);
            com03.Parameters.AddWithValue("f", f);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            if (d.Length == 0 || f.Length ==0)
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
            prod = com1.ExecuteScalar().ToString(); //null reference ...
            mat = com2.ExecuteScalar().ToString();
            kolv = com3.ExecuteScalar().ToString();
            seb = com4.ExecuteScalar().ToString();
            if (e_cb1.Text.Length == 0)
            {
                a = prod;
                i++;
            }
            else
            {
                if (b.Length == 0)
                    b = mat;
                MySqlCommand com0 = new MySqlCommand("select count(id_prod) from prod_costs where id_prod = @a and id_mat = @b", MC);
                com0.Parameters.AddWithValue("a", a); com0.Parameters.AddWithValue("b", b);
                prod = com0.ExecuteScalar().ToString();
                if (prod == "1")
                {
                    DialogResult ans = MessageBox.Show(
                            "Запись с таким номером товара и материала уже существует",
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
                MySqlCommand com0 = new MySqlCommand("select count(id_prod) from prod_costs where id_prod = @a and id_mat = @b", MC);
                com0.Parameters.AddWithValue("a", a); com0.Parameters.AddWithValue("b", b);
                prod = com0.ExecuteScalar().ToString();
                if (prod == "1")
                {
                    DialogResult ans = MessageBox.Show(
                            "Запись с таким номером товара и материала уже существует",
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
                    "Полe 'Кол-во' не должно содержать буквы и символы! \nПопробуйте еще раз",
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
                    "Недопустимое количество. \nПоле 'Количество' не может иметь значение больше 100 или меньше либо равно 0",
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
                    "Полe 'Себестоимость' не должно содержать буквы и символы! \nПопробуйте еще раз",
                    "Ошибка ввода",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                    return;
                }
                string regseb1 = @"^(0)", s1, s2;
                Regex regex = new Regex(regseb1);
                if (regex.IsMatch(g))
                {
                    DialogResult ans = MessageBox.Show(
                   "Недопустимое значение поля 'Себестоимость'. \nСебестоимость не может начинаться с 0",
                   "Ошибка ввода",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                    return;
                }
 

                MySqlCommand com0 = new MySqlCommand("select self_cost from prod_costs where id_prod = @a", MC);
                MySqlCommand comc = new MySqlCommand("select count(*) from prod_costs where id_prod = @a", MC);
                comc.Parameters.AddWithValue("a", a);
                s1 = comc.ExecuteScalar().ToString();
                if (s1 != "0")
                {
                    com0.Parameters.AddWithValue("a", a);
                    seb = com0.ExecuteScalar().ToString();
                    if (g != seb)
                    {
                        DialogResult ans = MessageBox.Show(
                      "Недопустимое значение поля 'Себестоимость'. \nСебестоимость для одного и того же товара не может иметь разные значения",
                      "Ошибка ввода",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Error,
                       MessageBoxDefaultButton.Button1);
                        return;
                    }
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
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Затраты на производство','Редактирование',@time)", MC);
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

            MySqlCommand com = new MySqlCommand("delete from prod_costs where id_prod = @a and id_mat = @b", MC);
            MySqlCommand com2 = new MySqlCommand("select id_prod from assortment where prod_name = @a", MC);
            MySqlCommand com3 = new MySqlCommand("select id_mat from mat_stor where mat_name = @b", MC);
            com2.Parameters.AddWithValue("a", a);
            com3.Parameters.AddWithValue("b", b);
            if (MC.State == System.Data.ConnectionState.Closed)
                MC.Open();
            a = com2.ExecuteScalar().ToString();
            b = com3.ExecuteScalar().ToString();
            MySqlCommand com11 = new MySqlCommand("insert into log values (@log,'Затраты на производство','Удаление',@time)", MC);
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
                MySqlCommand com2 = new MySqlCommand("create temporary table prod_costst(id_req varchar(40),id_mat varchar(40),reqm_count int(3));", con);
                MySqlCommand com12 = new MySqlCommand("insert into prod_costst SELECT assortment.prod_name, mat_stor.mat_name, prod_costs.one_pice" +
                    " FROM prod_costs, mat_stor, assortment WHERE mat_stor.id_mat = prod_costs.id_mat and assortment.id_prod = prod_costs.id_prod ", con);
                MySqlCommand com = new MySqlCommand("select id_mat from prod_costst where id_req = @z", con);
                MySqlCommand com3 = new MySqlCommand("drop table prod_costst", con);
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
            //MessageBox.Show(i);

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table prod_costst(id_req varchar(40),id_mat varchar(40),reqm_count int(3));", con);
                MySqlCommand com12 = new MySqlCommand("insert into prod_costst SELECT assortment.prod_name, mat_stor.mat_name, prod_costs.one_pice" +
                    " FROM prod_costs, mat_stor, assortment WHERE mat_stor.id_mat = prod_costs.id_mat and assortment.id_prod = prod_costs.id_prod ", con);
                MySqlCommand com = new MySqlCommand("select id_mat from prod_costst where id_req = @z", con);
                MySqlCommand com3 = new MySqlCommand("drop table prod_costst", con);
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
