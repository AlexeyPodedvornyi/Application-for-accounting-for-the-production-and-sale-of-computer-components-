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
    public partial class reqForm : Form
    {
        public reqForm()
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
                // MySqlCommand com = new MySqlCommand("select prod_req.id_req as 'Номер заявки', assortment.prod_name as 'Название товара', prod_req.req_count as 'Количество товара',"   
                //+ " prod_req.req_term as 'Дата выполнения' from prod_req, assortment where prod_req.id_prod = assortment.id_prod", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table prod_reqt(id_req int(3),id_prod varchar(40),req_count int(3), req_term date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into prod_reqt select prod_req.id_req as 'Номер заявки', assortment.prod_name as 'Название товара', prod_req.req_count as 'Количество товара',"
                + " prod_req.req_term as 'Дата выполнения' from prod_req, assortment where prod_req.id_prod = assortment.id_prod", con);
                MySqlCommand com = new MySqlCommand("select prod_req.id_req as 'Номер заявки', assortment.prod_name as 'Название товара', prod_req.req_count as 'Количество товара',"
                + " prod_req.req_term as 'Дата выполнения' from prod_req, assortment where prod_req.id_prod = assortment.id_prod", con);
                MySqlCommand com3 = new MySqlCommand("drop table prod_reqt", con);

                con.Open();
                com2.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tab1.DataSource = dt;
                }
                else
                    label2.Visible = true;

                com3.ExecuteNonQuery();
                rd.Close();
            }
        }

        private void reqForm_Load(object sender, EventArgs e)
        {
            tab1.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tab1.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tab1.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
           
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();
            

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                // MySqlCommand com = new MySqlCommand("select prod_req.id_req as 'Номер заявки', assortment.prod_name as 'Название товара', prod_req.req_count as 'Количество товара',"   
                //+ " prod_req.req_term as 'Дата выполнения' from prod_req, assortment where prod_req.id_prod = assortment.id_prod", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table prod_reqt(id_req int(3),id_prod varchar(40),req_count int(3), req_term date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into prod_reqt select prod_req.id_req as 'Номер заявки', assortment.prod_name as 'Название товара', prod_req.req_count as 'Количество товара',"
                + " prod_req.req_term as 'Дата выполнения' from prod_req, assortment where prod_req.id_prod = assortment.id_prod", con);
                MySqlCommand com = new MySqlCommand("select prod_req.id_req as 'Номер заявки', assortment.prod_name as 'Название товара', prod_req.req_count as 'Количество товара',"
                + " prod_req.req_term as 'Дата выполнения' from prod_req, assortment where prod_req.id_prod = assortment.id_prod", con);
                MySqlCommand com3 = new MySqlCommand("drop table prod_reqt", con);

                con.Open();
                com2.ExecuteNonQuery();
                com1.ExecuteNonQuery();
                MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd);

                if (dt.Rows.Count > 0)
                {
                    tab1.DataSource = dt;
                }
                else
                    label2.Visible = true;

                com3.ExecuteNonQuery();
                rd.Close();
            }
        }
        public static async void re_table2()  //1
        {
            String wbs, prod, count;
            //DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com1 = new MySqlCommand("insert into req_status (id_req,id_prod,req_count,req_term) select * from prod_req where(id_req,id_prod) not in (select id_req,id_prod from req_status); ", con);
                MySqlCommand com2 = new MySqlCommand("select count(*) from prod_req where(id_req,id_prod) not in (select id_req,id_prod from req_status);", con);
                await con.OpenAsync();
                count = com2.ExecuteScalar().ToString();
                int c1 = int.Parse(count);
                if (c1 > 0)
                    com1.ExecuteNonQuery();
                else
                    return;
                // System.Data.Common.DbDataReader rd = await com1.ExecuteReaderAsync();  //Для mysql не существует типа асинхрона MySqlDataReader
                //dt_1.Load(rd);
            }
        }

        private async void Obrabotka()
        {
            String cc,req, data,s_dat, count, id, c2; string c3,stat,y,m,d;
            DataTable dt_1 = new DataTable();
            DateTime date = new DateTime();
            date = DateTime.Today;
            s_dat = Convert.ToString(date);
           // var a = s_dat.Substring(0, 10);

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("call obrabotka", con);
                MySqlCommand com1 = new MySqlCommand("select * from t3", con);
                MySqlCommand com2 = new MySqlCommand("drop table t3", con);
                MySqlCommand com3 = new MySqlCommand("select id_req from t3 order by req_term limit 1", con);
                MySqlCommand com3_1 = new MySqlCommand("select req_term from t3 order by req_term limit 1", con);
               // MySqlCommand com3_11 = new MySqlCommand("select id_prod from t3 order by req_term limit 1", con);
                MySqlCommand com_c1 = new MySqlCommand("select count(*) from req_status where id_req = @req", con);
               // MySqlCommand com3_2 = new MySqlCommand("select id_client from prod_req where id_req = @req", con);
                MySqlCommand com4 = new MySqlCommand("delete from t3 where id_req = @req", con);
                MySqlCommand com5 = new MySqlCommand("insert into tmp_prod select * from prod_req where id_req = @req limit 1", con);
                MySqlCommand com6 = new MySqlCommand("delete from prod_req where id_req = @req", con);
                MySqlCommand com7_1 = new MySqlCommand("insert into req_status values (@req,@prod,@countr,@data,@stat)", con);
                MySqlCommand com66 = new MySqlCommand("insert into req_costs select prod_req.id_req,prod_costs.id_mat,(prod_costs.one_pice * prod_req.req_count) from prod_costs,prod_req " +
                "where prod_costs.id_prod = prod_req.id_prod and prod_req.id_req = @req ", con);
                MySqlCommand com333 = new MySqlCommand("insert into sklad_p select id_prod,req_count,req_term from prod_req where id_req = @req order by req_term", con);
                await con.OpenAsync();
                await com.ExecuteNonQueryAsync();
                System.Data.Common.DbDataReader rd = await com1.ExecuteReaderAsync();  //Для mysql не существует типа асинхрона MySqlDataReader

                dt_1.Load(rd);

                string cl, prod, countr;
                    while (dt_1.Rows.Count > 0)
                    {
                    //com3_2.Parameters.Clear();
                    com_c1.Parameters.Clear();
                    req = ""; data = ""; count = ""; id = ""; c2 = "";c3 = "";stat = "";cl = ""; prod = "";countr = "";
                        MySqlCommand com7 = new MySqlCommand("select id_prod from t3 order by req_term limit 1", con);
                        MySqlCommand com8 = new MySqlCommand("select req_count from t3 order by req_term limit 1", con);
                        //MySqlCommand com9 = new MySqlCommand("update prod_stor set stor_count = stor_count + @c2 where id_prod = @prod", con);
                        MySqlCommand com10 = new MySqlCommand("select stor_count from prod_stor where id_prod = @prod", MC);

                        if (con.State == System.Data.ConnectionState.Closed)// НЕЛЯЗЬ ИСПОЛЬЗОВАТЬ ОДИН КОНЕКТ ДЛЯ РИДЕРА И ДЛЯ ЕКЗЕКЬЮТА
                            await con.OpenAsync();                          // НЕ ПОЛУЧАЕТСЯ ВЫПОЛНИТЬ ЗАПРОС ВО ВРЕМЯ ЗАПРОС, ПРОБУЮ АСИНХРОН
                        req = com3.ExecuteScalar().ToString();   /// rabotaet
                        data = com3_1.ExecuteScalar().ToString();
                        com_c1.Parameters.AddWithValue("req", req);
                   // com3_2.Parameters.AddWithValue("req", req);
                   // count = com_c1.ExecuteScalar().ToString();
                        cc = com_c1.ExecuteScalar().ToString(); int kol = int.Parse(cc);
                  //  cl = com3_2.ExecuteScalar().ToString();
                    prod = com7.ExecuteScalar().ToString();
                    countr = com8.ExecuteScalar().ToString();
                    y = data.Substring(0, data.Length - 8);
                    y = y.Substring(8);
                    d = data.Substring(0, data.Length - 16);
                    m = data.Substring(3);
                    m = m.Substring(0, m.Length - 13);
                    
                    if (data != s_dat )  //ЕСЛИ ДАТА ЗАКАЗА НЕ РАВНО СЕГОДНЯЕШНЕЙ МЫ ЕГО ПРОПУСКАЕМ
                        {
                        if (kol > 0) //esli est takay nakladn, udalyaem i zanosim opyat`
                        {
                            MySqlCommand com11 = new MySqlCommand("delete from req_status where id_req = @req ", con);
                            com11.Parameters.AddWithValue("req", req);
                            com11.ExecuteNonQuery();
                            com11.Parameters.Clear();
                        }
                        stat = "В обработке"; data = y + "." + m + "." + d; //MessageBox.Show(data);
                        com4.Parameters.AddWithValue("req", req);
                        com7_1.Parameters.AddWithValue("req", req);
                        com7_1.Parameters.AddWithValue("prod", prod);
                        //com7_1.Parameters.AddWithValue("cl", cl);
                        com7_1.Parameters.AddWithValue("countr", countr);
                        com7_1.Parameters.AddWithValue("data", data);
                        com7_1.Parameters.AddWithValue("stat", stat);
                        await com4.ExecuteNonQueryAsync(); await com7_1.ExecuteNonQueryAsync();
                        com3_1.Parameters.Clear();
                            com4.Parameters.Clear(); com7_1.Parameters.Clear();
                    }

                        else                // ЕСЛИ ДАТА ЗАКАЗА РАВНА СЕГОДНЯШНЕЙ ТО УДОВЛЕТВОРЯЕМ ЕГО Т.Е. УДАЛЯЕМ ИЗ 2-УХ ТАБЛИЦ
                        {
                        if (kol > 0) //esli est takay nakladn, udalyaem i zanosim opyat`
                        {
                            MySqlCommand com11 = new MySqlCommand("delete from req_status where id_req = @req ", con);
                            com11.Parameters.AddWithValue("req", req);
                            com11.ExecuteNonQuery();
                            com11.Parameters.Clear();
                        }
                        com333.Parameters.AddWithValue("req", req);
                        com333.ExecuteNonQuery(); com333.Parameters.Clear();
                         stat = "Выполнено"; data = y + "." + m + "." + d;
                        id = com7.ExecuteScalar().ToString();
                            c2 = com8.ExecuteScalar().ToString();
                            com10.Parameters.AddWithValue("prod", prod);
                            //com9.Parameters.AddWithValue("c2", c2);
                           // com9.Parameters.AddWithValue("prod", prod);
                            com5.Parameters.AddWithValue("req", req);
                            com6.Parameters.AddWithValue("req", req);
                            com4.Parameters.AddWithValue("req", req);
                        com7_1.Parameters.AddWithValue("req", req);
                        com7_1.Parameters.AddWithValue("prod", prod);
                        //com7_1.Parameters.AddWithValue("cl", cl);
                        com7_1.Parameters.AddWithValue("countr", countr);
                        com7_1.Parameters.AddWithValue("data", data);
                        com7_1.Parameters.AddWithValue("stat", stat);
                        //c3 = com10.ExecuteScalar().ToString();
                            /*int c1 = int.Parse(c2), c4 = int.Parse(c3);
                            c4 = c4 + c1;
                            if (c4 >= 10000)
                            {
                                DialogResult ans1 = MessageBox.Show(
                              "Недостаточно места на складе. \nКол-во товара на складе не может превышать 9999",
                              "Ошибка",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
                                return;
                            }*/
                           // await com9.ExecuteNonQueryAsync();
                        await com7_1.ExecuteNonQueryAsync();
                        await com4.ExecuteNonQueryAsync();
                            await com5.ExecuteNonQueryAsync();
                            await com6.ExecuteNonQueryAsync();
                        com4.Parameters.Clear(); com7_1.Parameters.Clear();
                        com5.Parameters.Clear();
                            com6.Parameters.Clear(); 
                       // com9.Parameters.Clear();
                        //com3_2.Parameters.Clear();
                    }
                    

                        rd = await com1.ExecuteReaderAsync();
                        dt_1.Clear();
                        dt_1.Load(rd);
                    }
              /*  MySqlCommand com15 = new MySqlCommand("select count(*) from prod_req where req_term < @dat12", con);
                MySqlCommand com16 = new MySqlCommand("update req_status set stat = 'Просрочено' where req_term < @dat12", con);
                DateTime dat1 = DateTime.Today; string dat12 = dat1.ToString(), cont;
                y = dat12.Substring(0, dat12.Length - 8);
                y = y.Substring(6);
                d = dat12.Substring(0, dat12.Length - 17);
                m = dat12.Substring(3);
                m = m.Substring(0, m.Length - 14);
                dat12 = y + "." + m + "." + d;
                com15.Parameters.AddWithValue("dat12", dat12);
                com16.Parameters.AddWithValue("dat12", dat12);
                cont = com15.ExecuteScalar().ToString();
                int cw = int.Parse(cont);
                if (cw > 0)
                {
                    label2.Text = "Внимание! Имеються просроченые заявки \nв количистве " + cont + "шт. \nДля их удаления обратитесь в администратору!";
                    label2.Visible = true;
                }
                com16.ExecuteNonQuery();
                com15.Parameters.Clear(); com16.Parameters.Clear();*/

                rd.Close();
                com2.ExecuteNonQuery();// drop table t3
            }
        }
        last_prod_req last_prod;
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "last_prod")
                {
                    DialogResult ans = MessageBox.Show(
                    "Запрошенная форма уже открыта. Нельзя открыть несколько копий одной формы!",
                    "Внимание",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1
                   );//MessageBoxOptions.DefaultDesktopOnly
                    return;
                }
            }

            last_prod = new last_prod_req();
            last_prod.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                re_table2();
                Obrabotka();
            }
            catch (MySqlException)
            {
                DialogResult ans = MessageBox.Show(
              "Произошёл неожиданный сбой при обработке \nОбратитесь к администратору",
              "Ошибка",
               MessageBoxButtons.OK,
               MessageBoxIcon.Error,
               MessageBoxDefaultButton.Button1);
                return;
            }
        }
    }
}
