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
    public partial class waybill_sForm : Form
    {
        public waybill_sForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void waybill_sForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            int lvl = int.Parse(Autor_R.log);
            if (lvl == 1)
                button1.Enabled = false;
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();
            

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                //MySqlCommand com = new MySqlCommand("select waybill_s.id_wbs as 'Номер накладной', clients.dir_FIO as 'ФИ клиента', waybill_s.wbs_sum as 'Сумма по накладной', " +
                    //"waybill_s.wbs_dt as 'Дата поставки' from waybill_s,clients where waybill_s.id_client = clients.id_client", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_st(id_wbs int(3),id_client varchar(40),wbs_sum bigint, wbs_dt date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_st SELECT  waybill_s.id_wbs AS 'Номер накладной', clients.dir_FIO AS 'ФИ клиента',  waybill_s.wbs_sum AS 'Сума по накладной', " +
                    "waybill_s.wbs_dt AS 'Дата выполнения' from clients, waybill_s  WHERE waybill_s.id_client = clients.id_client;", con);
                MySqlCommand com = new MySqlCommand("select id_wbs as 'Номер накладной', id_client as 'ФИ клиента', wbs_sum as 'Сума по накладной',wbs_dt as 'Дата выполнения' from waybill_st", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_st", con);

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
                    label2.Visible = true;

                com3.ExecuteNonQuery();
                rd.Close();
            }
        }

        public static async void re_table1()  //1
        {
            String wbs, prod, count;
            //DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com1 = new MySqlCommand("insert into wbs_status (id_wbs,id_client,wbs_sum,wbs_dt) select * from waybill_s where(id_wbs) not in (select id_wbs from wbs_status); ", con);
                MySqlCommand com2 = new MySqlCommand("select count(*) from waybill_s where(id_wbs) not in (select id_wbs from tmp_wbs);", con);
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

        private void obrabotka_wbs ()
        {
            String wbs, data, s_dat, req_dat, client, sum,y,d,m;
            DataTable dt_1 = new DataTable();
            DateTime date = new DateTime();
            date = DateTime.Today;
            s_dat = Convert.ToString(date);
            // var a = s_dat.Substring(0, 10);

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("call obrabotka_wbs", con);
                MySqlCommand com1 = new MySqlCommand("select * from t4", con);
                MySqlCommand com2 = new MySqlCommand("drop table t4", con);
                MySqlCommand com3 = new MySqlCommand("select id_wbs from t4 order by wbs_dt limit 1", con);
                MySqlCommand com3_1 = new MySqlCommand("select wbs_dt from t4 order by wbs_dt limit 1", con);
                MySqlCommand com_c1 = new MySqlCommand("select count(*) from wbs_status where id_wbs =@wbs", con);
                MySqlCommand com4 = new MySqlCommand("delete from t4 where id_wbs = @wbs", con);
               MySqlCommand com5 = new MySqlCommand("insert into tmp_wbs select * from waybill_s where id_wbs = @wbs", con);
                MySqlCommand com6 = new MySqlCommand("delete from waybill_s where id_wbs = @wbs", con);
                MySqlCommand com7 = new MySqlCommand("insert into wbs_status values (@wbs,@client,@sum,@data,@stat)", con);
               // MySqlCommand com7_1 = new MySqlCommand("update wbs_status set stat = @stat where id_wbs = @wbs", con);
                MySqlCommand com7_2 = new MySqlCommand("delete from tmp_wbsp where id_wbs = @wbs", con);
                MySqlCommand com8 = new MySqlCommand("select id_client from waybill_s where id_wbs = @wbs", con);
                MySqlCommand com9 = new MySqlCommand("select wbs_sum from waybill_s where id_wbs = @wbs", con);
               // MySqlCommand com10 = new MySqlCommand("insert into tmp_wbs select * from waybill_s where id_wbs = @wbs", con);
                con.Open();
                 com.ExecuteNonQuery();
                System.Data.Common.DbDataReader rd =  com1.ExecuteReader();  //Для mysql не существует типа асинхрона MySqlDataReader

                dt_1.Load(rd);
                string stat = "";
               
                    while (dt_1.Rows.Count > 0)
                    {
                        wbs = ""; data = ""; req_dat = ""; client = ""; sum = "";
                        if (con.State == System.Data.ConnectionState.Closed)// НЕЛЯЗЬ ИСПОЛЬЗОВАТЬ ОДИН КОНЕКТ ДЛЯ РИДЕРА И ДЛЯ ЕКЗЕКЬЮТА
                             con.Open();                          // НЕ ПОЛУЧАЕТСЯ ВЫПОЛНИТЬ ЗАПРОС ВО ВРЕМЯ ЗАПРОС, ПРОБУЮ АСИНХРОН
                        wbs = com3.ExecuteScalar().ToString();   /// rabotaet
                        data = com3_1.ExecuteScalar().ToString();
           
                    com8.Parameters.AddWithValue("wbs", wbs); com9.Parameters.AddWithValue("wbs", wbs);
                    client = com8.ExecuteScalar().ToString();
                    com_c1.Parameters.AddWithValue("wbs", wbs);
                    req_dat = com_c1.ExecuteScalar().ToString();int kol = int.Parse(req_dat);
                    sum = com9.ExecuteScalar().ToString();
                    DateTime dat = new DateTime(); dat = DateTime.Today;
                    // DateTime datr = new DateTime();datr = Convert.ToDateTime(req_dat);datr = datr.AddDays(1);
                    y = data.Substring(0, data.Length - 8);
                    y = y.Substring(8);
                    d = data.Substring(0, data.Length - 16);
                    m = data.Substring(3);
                    m = m.Substring(0, m.Length - 13);

                    if (data != dat.ToString())  //ЕСЛИ = сегодня и нет заявок на произв
                        {
                        if (kol >0) //esli est takay nakladn, udalyaem i zanosim opyat`
                        {
                            MySqlCommand com11 = new MySqlCommand("delete from wbs_status where id_wbs = @wbs ", con);
                            com11.Parameters.AddWithValue("wbs", wbs);
                            com11.ExecuteNonQuery();
                            com11.Parameters.Clear();
                        }
                        stat = "В обработке"; data = y + "." + m + "." + d;
                        com4.Parameters.AddWithValue("wbs", wbs);
                        com7.Parameters.AddWithValue("wbs", wbs);
                        com7.Parameters.AddWithValue("client", client);
                        com7.Parameters.AddWithValue("sum", sum);
                        com7.Parameters.AddWithValue("data", data);
                        com7.Parameters.AddWithValue("stat", stat);
                        // com7_1.Parameters.AddWithValue("wbs", wbs);
                        // com7_1.Parameters.AddWithValue("stat", stat);
                        com4.ExecuteNonQuery();
                            com3_1.Parameters.Clear();
                            com4.Parameters.Clear();
                             com7.ExecuteNonQuery(); com7.Parameters.Clear();
                        //  com7_1.ExecuteNonQuery();
                    }

                        else                // ЕСЛИ ДАТА ЗАКАЗА РАВНА СЕГОДНЯШНЕЙ ТО УДОВЛЕТВОРЯЕМ ЕГО Т.Е. УДАЛЯЕМ ИЗ 2-УХ ТАБЛИЦ
                        {
                        if (kol > 0)
                        {
                            MySqlCommand com11 = new MySqlCommand("delete from wbs_status where id_wbs = @wbs ", con);
                            com11.Parameters.AddWithValue("wbs", wbs);
                            com11.ExecuteNonQuery();
                            com11.Parameters.Clear();  
                        }
                        stat = "Выполнено"; data = y + "." + m + "." + d;
                        com5.Parameters.AddWithValue("wbs", wbs);
                        com6.Parameters.AddWithValue("wbs", wbs);
                        com4.Parameters.AddWithValue("wbs", wbs);
                        com7.Parameters.AddWithValue("wbs", wbs);
                        com7.Parameters.AddWithValue("client", client);
                        com7.Parameters.AddWithValue("sum", sum);
                        com7.Parameters.AddWithValue("data", data);
                        com7.Parameters.AddWithValue("stat", stat);
                        // com7_1.Parameters.AddWithValue("wbs", wbs);
                        //  com7_1.Parameters.AddWithValue("stat", stat);
                        com7_2.Parameters.AddWithValue("wbs", wbs); 
                        com4.ExecuteNonQuery();
                            com5.ExecuteNonQuery();
                            com6.ExecuteNonQuery();
                            com7.ExecuteNonQuery();
                       // com7_1.ExecuteNonQuery();
                        com7_2.ExecuteNonQuery();
                        com4.Parameters.Clear();
                            com5.Parameters.Clear();
                            com6.Parameters.Clear(); com7.Parameters.Clear();
                    }

                        com_c1.Parameters.Clear();
                        rd = com1.ExecuteReader();
                        dt_1.Clear();
                        dt_1.Load(rd);
                    com8.Parameters.Clear();
                    com9.Parameters.Clear();
                    com7.Parameters.Clear(); com7_2.Parameters.Clear();
                }
                /*MySqlCommand com10 = new MySqlCommand("select count(*) from waybill_s where wbs_dt < @dat12", con);
                DateTime dat1 = DateTime.Today; string dat12 = dat1.ToString(), cont;
                y = dat12.Substring(0, dat12.Length - 8);
                y = y.Substring(8);
                d = dat12.Substring(0, dat12.Length - 16);
                m = dat12.Substring(3);
                m = m.Substring(0, m.Length - 13);
                dat12 = y + "." + m + "." + d;
                com10.Parameters.AddWithValue("dat12", dat12);
                cont = com10.ExecuteScalar().ToString();
                int cw = int.Parse(cont);
                if (cw > 0)
                {
                    label2.Text = "Внимание! Имеються просроченые накладные \nв количистве " + cont + "шт. \nДля их удаления обратитесь в администратору!";
                    label2.Visible = true;
                }
                com10.Parameters.Clear();*/
                rd.Close();
                com2.ExecuteNonQuery();// drop table t4
            }
        }


        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_st(id_wbs int(3),id_client varchar(40),wbs_sum bigint, wbs_dt date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_st SELECT  waybill_s.id_wbs AS 'Номер накладной', clients.dir_FIO AS 'ФИ клиента',  waybill_s.wbs_sum AS 'Сума по накладной', " +
                    "waybill_s.wbs_dt AS 'Дата выполнения' from clients, waybill_s  WHERE waybill_s.id_client = clients.id_client;", con);
                MySqlCommand com = new MySqlCommand("select id_wbs as 'Номер накладной', id_client as 'ФИ клиента', wbs_sum as 'Сума по накладной',wbs_dt as 'Дата выполнения' from waybill_st", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_st", con);

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
                    label2.Visible = true;

                com3.ExecuteNonQuery();
                rd.Close();
            }
        }

        last_wbs last_wbs; int j = 0;
        private void last_but_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "last_wbs")
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

            last_wbs = new last_wbs();
            last_wbs.Show();
        }

        //wbs_statusForm wbs_stat; 
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                re_table1();
                obrabotka_wbs();
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
