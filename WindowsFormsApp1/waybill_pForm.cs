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
    public partial class waybill_pForm : Form
    {
        public waybill_pForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void waybill_pForm_Load(object sender, EventArgs e)
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
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_pt(id_wbp int(3),id_prov varchar(40),wpb_sum bigint, wpb_dt date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_pt SELECT waybill_p.id_wbp AS 'Номер накладной', provider.prov_name AS 'Название поставщика', waybill_p.wpb_sum " +
                    "AS 'Сума по накладной', waybill_p.wpb_dt AS 'Дата поставки' FROM provider, waybill_p WHERE waybill_p.id_prov = provider.id_prov;", con);
                MySqlCommand com = new MySqlCommand("select id_wbp as 'Номер накладной', id_prov as 'Название поставщика', wpb_sum as 'Сума по накладной',wpb_dt as 'Дата поставки' from waybill_pt", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_pt", con);

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
        public static async void re_table3()  //1
        {
            String wbs, prod, count;
            //DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com1 = new MySqlCommand("insert into wbp_status (id_wbp,id_prov,wpb_sum,wpb_dt) select * from waybill_p where(id_wbp) not in (select id_wbp from wbp_status); ", con);
                MySqlCommand com2 = new MySqlCommand("select count(*) from waybill_p where(id_wbp) not in (select id_wbp from wbp_status);;", con);
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
        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_pt(id_wbp int(3),id_prov varchar(40),wpb_sum bigint, wpb_dt date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_pt SELECT waybill_p.id_wbp AS 'Номер накладной', provider.prov_name AS 'Название поставщика', waybill_p.wpb_sum " +
                    "AS 'Сума по накладной', waybill_p.wpb_dt AS 'Дата поставки' FROM provider, waybill_p WHERE waybill_p.id_prov = provider.id_prov;", con);
                MySqlCommand com = new MySqlCommand("select id_wbp as 'Номер накладной', id_prov as 'Название поставщика', wpb_sum as 'Сума по накладной',wpb_dt as 'Дата поставки' from waybill_pt", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_pt", con);

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



        last_wbp last_wbp; int j = 0;
        private void last_but_Click(object sender, EventArgs e)
        {


            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "last_wbp")
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

            last_wbp = new last_wbp();
            last_wbp.Show();
        }

        private void udal()
        {
            String wbp, data, s_dat, req_dat, client, sum, y, d, m;
            DataTable dt_1 = new DataTable();
           
            // var a = s_dat.Substring(0, 10);

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand comc = new MySqlCommand("create temporary table wbpt(id_wbp int (3), wpb_dt date);", con);
                MySqlCommand comci = new MySqlCommand("insert into wbpt select id_wbp, wpb_dt from waybill_p", con);
                MySqlCommand comcd = new MySqlCommand("drop table wbpt", con);
                MySqlCommand com = new MySqlCommand("select * from wbpt", con);
                MySqlCommand com3 = new MySqlCommand("select id_wbp from wbpt order by wpb_dt limit 1 ", con);
                MySqlCommand com33 = new MySqlCommand("select wpb_dt from wbpt order by wpb_dt limit 1", con);
               //MySqlCommand com333 = new MySqlCommand("select id_wbp from waybill_p order by wpb_dt limit 1", con);
                MySqlCommand com333 = new MySqlCommand("insert into sklad_m select waybill_pm.id_mat,waybill_pm.wbp_count,waybill_p.wpb_dt from waybill_p,waybill_pm where waybill_p.id_wbp = @wbp and waybill_p.id_wbp = waybill_pm.id_wbp order by waybill_p.wpb_dt", con);
               // MySqlCommand com1 = new MySqlCommand("select * from t4", con);
                MySqlCommand com2 = new MySqlCommand("delete from wbpt where id_wbp = @wbp", con);
                MySqlCommand com2_1 = new MySqlCommand("delete from waybill_p where id_wbp = @wbp", con);
                MySqlCommand com2_2 = new MySqlCommand("delete from waybill_pm where id_wbp = @wbp", con);
                MySqlCommand com1 = new MySqlCommand("insert into tmp_wbp select * from waybill_p where id_wbp = @wbp", con);

                con.Open();
                comc.ExecuteNonQuery();
                comci.ExecuteNonQuery();
                System.Data.Common.DbDataReader rd = com.ExecuteReader();  //Для mysql не существует типа асинхрона MySqlDataReader
                
                dt_1.Load(rd);
                string stat = "";

                while (dt_1.Rows.Count > 0)
                {
                    
                    wbp = ""; data = ""; req_dat = ""; client = ""; sum = "";
                    if (con.State == System.Data.ConnectionState.Closed)// НЕЛЯЗЬ ИСПОЛЬЗОВАТЬ ОДИН КОНЕКТ ДЛЯ РИДЕРА И ДЛЯ ЕКЗЕКЬЮТА
                        con.Open();                          // НЕ ПОЛУЧАЕТСЯ ВЫПОЛНИТЬ ЗАПРОС ВО ВРЕМЯ ЗАПРОС, ПРОБУЮ АСИНХРОН
                    wbp = com3.ExecuteScalar().ToString();   /// rabotaet
                    data = com33.ExecuteScalar().ToString();
                    y = data.Substring(0, data.Length - 8);
                    y = y.Substring(8);
                    d = data.Substring(0, data.Length - 16);
                    m = data.Substring(3);
                    m = m.Substring(0, m.Length - 13);
                   // MessageBox.Show(wbp);
                    //MessageBox.Show(dt_1.Rows.Count.ToString());
                    DateTime dat = new DateTime();
                   dat = DateTime.Today;
                   // MessageBox.Show(data, dat.ToString());
                    if (data != dat.ToString())  //ЕСЛИ = сегодня и нет заявок на произв
                    {
                      // MessageBox.Show("udalit`");
                        com2.Parameters.AddWithValue("wbp", wbp);
                        com2.ExecuteNonQuery(); //com1.Parameters.AddWithValue("wbp", wbp); com1.ExecuteNonQuery();
                    }
                    else
                    {
                        com333.Parameters.AddWithValue("wbp", wbp);
                        com333.ExecuteNonQuery();
                        com1.Parameters.AddWithValue("wbp", wbp);
                        com2.Parameters.AddWithValue("wbp", wbp);
                        com2_1.Parameters.AddWithValue("wbp", wbp);
                        com2_2.Parameters.AddWithValue("wbp", wbp);
                        com1.ExecuteNonQuery();
                        com2.ExecuteNonQuery(); com2_1.ExecuteNonQuery(); com2_2.ExecuteNonQuery();
                    }

                    com2.Parameters.Clear(); com2_1.Parameters.Clear(); com2_2.Parameters.Clear(); com333.Parameters.Clear(); com1.Parameters.Clear();
                    rd = com.ExecuteReader();
                    dt_1.Clear();
                    dt_1.Load(rd);
                }
               
                comcd.ExecuteNonQuery();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                re_table3();
                udal();
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
