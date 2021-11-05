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
    public partial class waybill_spForm : Form
    {
        public waybill_spForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private async void waybill_spForm_Load(object sender, EventArgs e)
        {
                tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
                tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
                tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            int lvl = int.Parse(Autor_R.log);
            if (lvl == 1)
                button1.Enabled = false;
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();
            // СПИСАТЬ ТОВАРЫ В ТМП_СПИСОК ПЕРЕД ОБРАБОТКОЙ, ЧТО Б ХРАНИЛИСЬВ ПОЛНОМ ВИДЕ

            
            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                // MySqlCommand com = new MySqlCommand("select tmp_wbsp.id_wbs as 'Номер накладной', assortment.prod_name as 'Название товара', tmp_wbsp.wbs_count as 'Количество товара' from tmp_wbsp, assortment, waybill_s where tmp_wbsp.id_prod = assortment.id_prod and tmp_wbsp.id_wbs = waybill_s.id_wbs", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_spt(id_wbs int(3),id_prod varchar(40),count int(3));", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_spt select waybill_sp.id_wbs as 'Номер накладной', assortment.prod_name as 'Название товара', waybill_sp.wbs_count as 'Количество товара'" +
                    " from waybill_sp, assortment, waybill_s where waybill_sp.id_prod = assortment.id_prod and waybill_sp.id_wbs = waybill_s.id_wbs", con);
                MySqlCommand com = new MySqlCommand("select id_wbs as 'Номер накладной', id_prod as 'Название товара', count as 'Количество товара' from waybill_spt", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_spt", con);

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
        private async void re_table()  //1
        {
            String wbs, prod, count;
            //DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com1 = new MySqlCommand("insert into tmp_wbsp select * from waybill_sp where(id_wbs, id_prod) not in (select id_wbs, id_prod from tmp_wbsp); ", con);
                MySqlCommand com2 = new MySqlCommand("select count(*) from waybill_sp where(id_wbs, id_prod) not in (select id_wbs, id_prod from tmp_wbsp); ", con);
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

        private async void cr_zayvka()
        {
            String wbs, prod, count;
            DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {

                MySqlCommand com1 = new MySqlCommand("select waybill_sp.id_wbs,waybill_sp.id_prod,waybill_sp.wbs_count from waybill_sp,waybill_s where waybill_sp.id_wbs = waybill_s.id_wbs order by waybill_s.wbs_dt", con);
                MySqlCommand com3 = new MySqlCommand("select waybill_sp.id_wbs from waybill_sp,waybill_s where waybill_sp.id_wbs =  waybill_s.id_wbs order by waybill_s.wbs_dt limit 1", con);
                MySqlCommand com3_1 = new MySqlCommand("select waybill_sp.id_prod from waybill_sp,waybill_s where waybill_sp.id_wbs =  waybill_s.id_wbs order by waybill_s.wbs_dt limit 1", con);
                MySqlCommand com3_2 = new MySqlCommand("select sum(wbs_count) from waybill_sp where id_prod = @prod", con);
                MySqlCommand com4 = new MySqlCommand("delete from waybill_sp where id_prod = @prod", con);
                MySqlCommand com6 = new MySqlCommand("insert into prod_req values(@last_id,@prod,@count,@dat)", con);
               // MySqlCommand com6_1 = new MySqlCommand("select id_client from waybill_s where id_wbs = @wbs", con);
                MySqlCommand com6_2 = new MySqlCommand("select wbs_dt from waybill_s where id_wbs = @wbs", con);
                MySqlCommand com6_3 = new MySqlCommand("select count(id_req) from req_status where id_req = @last_id", con);
                MySqlCommand com6_5 = new MySqlCommand("select count(id_req) from prod_req where id_req = @last_id", con);
                MySqlCommand com8 = new MySqlCommand("select id_req from req_status order by id_req desc limit 1", con);
                MySqlCommand com8_1 = new MySqlCommand("select wbs_dt from waybill_s where id_wbs =@wbs limit 1", con);
                MySqlCommand com8_2 = new MySqlCommand("select count(*) from req_status", con);
                MySqlCommand com8_21 = new MySqlCommand("select count(*) from prod_req", con);
                MySqlCommand com66 = new MySqlCommand("insert into req_costs select prod_req.id_req,prod_costs.id_mat,(prod_costs.one_pice * prod_req.req_count) from prod_costs,prod_req " +
                "where prod_costs.id_prod = prod_req.id_prod and prod_req.id_req = @last_id ", con);

                await con.OpenAsync();
                System.Data.Common.DbDataReader rd = await com1.ExecuteReaderAsync();  //Для mysql не существует типа асинхрона MySqlDataReader

                dt_1.Load(rd);

                string wbs_1 = "0", last_1 = "",cr,cr2; int kt_1 = 1; int j = 0; bool ja = true;
                while (dt_1.Rows.Count > 0)
                {
                    wbs = ""; prod = ""; count = ""; string data="", dt = "", sum = "", k1 = "", k_tov = "", last_id = "", k2 = "", k3 = "",y="",m="",d=""; DateTime dat = new DateTime(); //dat = DateTime.Today;
                    if (con.State == System.Data.ConnectionState.Closed)// НЕЛЯЗЬ ИСПОЛЬЗОВАТЬ ОДИН КОНЕКТ ДЛЯ РИДЕРА И ДЛЯ ЕКЗЕКЬЮТА
                        await con.OpenAsync();                          // НЕ ПОЛУЧАЕТСЯ ВЫПОЛНИТЬ ЗАПРОС ВО ВРЕМЯ ЗАПРОС, ПРОБУЮ АСИНХРОН
                    wbs = com3.ExecuteScalar().ToString();   /// rabotaet
                    prod = com3_1.ExecuteScalar().ToString();
                    com3_2.Parameters.Clear(); com8_1.Parameters.Clear();
                    com3_2.Parameters.AddWithValue("prod", prod);
                    count = com3_2.ExecuteScalar().ToString();  //ПОФИКСИТЬ !!! СПИСЫВАЕТ РАЗНИЦУ В ТОВАРЕ СО СКЛАДА!!!

                   // com6_1.Parameters.AddWithValue("wbs", wbs);
                    com6_2.Parameters.AddWithValue("wbs", wbs);

                    com8.Parameters.AddWithValue("wbs", wbs);
                    com8_1.Parameters.AddWithValue("wbs", wbs);
                    //cl = com6_1.ExecuteScalar().ToString();
                    dt = com6_2.ExecuteScalar().ToString();
                    data = com8_1.ExecuteScalar().ToString(); //int kk2 = int.Parse(k2); //maks req_id
                    y = data.Substring(0, data.Length - 8);
                    y = y.Substring(8);
                    d = data.Substring(0, data.Length - 16);
                    m = data.Substring(3);
                    m = m.Substring(0, m.Length - 13);
                    dat = Convert.ToDateTime(data);
                    dat = dat.AddDays(-1);
                    cr = com8_2.ExecuteScalar().ToString();
                    cr2 = com8_21.ExecuteScalar().ToString();
                    int kr = int.Parse(cr); int kr2 = int.Parse(cr2);
                    if (kr == 0 && kr2 == 0)
                        last_id = "1";
                    else
                    {
                        if(kr != 0)
                            if (j == 0)
                             last_id = com8.ExecuteScalar().ToString();
                            else
                                last_id = last_1;
                        else
                            last_id = last_1;
                        //MessageBox.Show("nn");
                    }
                   // MessageBox.Show(last_id);
                    int last = int.Parse(last_id);
                    //if
                    int i = 0;
                   // MessageBox.Show("na4alo");
                   // MessageBox.Show(last_id, last_1);
                   /*
                    if (wbs == wbs_1)
                    {
                       // MessageBox.Show("takaya est");
                        MySqlCommand com6_4 = new MySqlCommand("insert into prod_req values(@last_1,@prod,@count,@cl,@dat)", con);
                        com6_4.Parameters.AddWithValue("last_1", last_1);
                        com6_4.Parameters.AddWithValue("prod", prod);
                        com6_4.Parameters.AddWithValue("count", count);
                        com6_4.Parameters.AddWithValue("cl", cl);
                        com6_4.Parameters.AddWithValue("dat", dat);
                        com6_4.ExecuteNonQuery();
                        com6_4.Parameters.Clear();
                        int l1 = int.Parse(last_1);
                        l1++;
                        last_1 = Convert.ToString(l1);
                        //com2.ExecuteNonQuery();

                    }
                    */

                        if (last_1.Length != 0)
                            last_id = last_1;
                        else
                        {
                            last = int.Parse(last_id);
                            last_id = Convert.ToString(last);
                        }
                       // MessageBox.Show("last", last.ToString());
                        com6_3.Parameters.AddWithValue("last_id", last_id); com6_5.Parameters.AddWithValue("last_id", last_id);
                        k1 = com6_3.ExecuteScalar().ToString();
                        k3 = com6_5.ExecuteScalar().ToString();
                        while (k1 != "0" || k3 != "0")
                        {
                            com6_3.Parameters.Clear(); com6_5.Parameters.Clear(); last++;
                            last_id = Convert.ToString(last);
                            com6_3.Parameters.AddWithValue("last_id", last_id); com6_5.Parameters.AddWithValue("last_id", last_id);
                            k1 = com6_3.ExecuteScalar().ToString();
                            k3 = com6_5.ExecuteScalar().ToString();
                            i++; //MessageBox.Show("last", last.ToString());
                        }
                        //last = last - i;
                        last_id = Convert.ToString(last);
                        //MessageBox.Show("last", last.ToString());
                        last_1 = last_id;   ///zapisivaet 1 po4emu?
                        //MessageBox.Show("last", last.ToString());
                        com6.Parameters.AddWithValue("last_id", last_id);
                        com6.Parameters.AddWithValue("prod", prod);
                        com6.Parameters.AddWithValue("count", count);
                       // com6.Parameters.AddWithValue("cl", cl);
                        com6.Parameters.AddWithValue("dat", dat);
                        com6.ExecuteNonQuery();

                    // last_1 = last_id;

                    com66.Parameters.AddWithValue("last_id", last_id);
                    await com66.ExecuteNonQueryAsync();
                    com66.Parameters.Clear();
                    j++;
                    wbs_1 = wbs;
                    
                    com8.Parameters.Clear();
                    com6.Parameters.Clear();  com6_2.Parameters.Clear(); com6_3.Parameters.Clear(); com6_5.Parameters.Clear();
                    //com4.Parameters.AddWithValue("wbs", wbs);   // da
                    com4.Parameters.AddWithValue("prod", prod);
                    com4.ExecuteNonQuery(); // УДАЛЯЕМ ИЗ МАТЕРИАЛОВ
                    dt_1.Clear(); com4.Parameters.Clear();
                    rd = await com1.ExecuteReaderAsync();
                    dt_1.Load(rd);
                }

                //УДАЛЕНИЕ ВРЕМ ТАБЛ
                rd.Close();

            }
        } 
        private async void udovl_zakazov() // ОБРАБОТКА УДОВЛЕТВОРИТЕЛЬНЫХ ТОВАРОВ
        {
            String wbs, prod, count;
            DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("call udovlet_zakaz", con);
                MySqlCommand com1 = new MySqlCommand("select * from t1 order by wbs_dt", con);
                MySqlCommand com2 = new MySqlCommand("drop table t1", con);
                MySqlCommand com3 = new MySqlCommand("select t1.id_wbs from t1,waybill_s where t1.id_wbs =  waybill_s.id_wbs order by waybill_s.wbs_dt limit 1", con);
                MySqlCommand com3_1 = new MySqlCommand("select t1.id_prod from t1,waybill_s where t1.id_wbs =  waybill_s.id_wbs order by waybill_s.wbs_dt limit 1", con);
                MySqlCommand com3_2 = new MySqlCommand("select t1.wbs_count from t1,waybill_s where t1.id_wbs =  waybill_s.id_wbs order by waybill_s.wbs_dt limit 1", con);
                MySqlCommand com4 = new MySqlCommand("delete from waybill_sp where id_wbs = @wbs and id_prod = @prod", con);
                MySqlCommand com4_1 = new MySqlCommand("delete from t1 where id_wbs = @wbs and id_prod = @prod", con);
                MySqlCommand com5 = new MySqlCommand("update prod_stor set stor_count = stor_count - @count " +
               "where id_prod = @prod", con);
                MySqlCommand com6 = new MySqlCommand("insert into waybill_s values(@last_id,@cl,@sum,@dat)", con);
                MySqlCommand com6_1 = new MySqlCommand("select id_client from waybill_s where id_wbs = @wbs", con);
                MySqlCommand com6_2 = new MySqlCommand("select cost_1 from assortment where id_prod = @prod", con);
                MySqlCommand com6_3 = new MySqlCommand("select count(id_wbs) from wbs_status where id_wbs = @last_id", con);
                MySqlCommand com7 = new MySqlCommand("select count(id_wbs) from t1 where id_wbs = @wbs", con);
                MySqlCommand com8 = new MySqlCommand("select id_wbs from wbs_status order by id_wbs desc limit 1", con);
                MySqlCommand com8_1 = new MySqlCommand("select count(id_wbs) from wbs_status ", con);
                MySqlCommand com6_5 = new MySqlCommand("select count(id_wbs) from waybill_s where id_wbs = @last_id", con);
                MySqlCommand com71 = new MySqlCommand("select count(id_wbs) from waybill_sp where id_wbs = @wbs", con);
                await con.OpenAsync();
               
                await com.ExecuteNonQueryAsync();
                System.Data.Common.DbDataReader rd = await com1.ExecuteReaderAsync();  //Для mysql не существует типа асинхрона MySqlDataReader
                
                dt_1.Load(rd);

                string wbs_1 = "0",last_1="",td = ""; int kt_1=1; int j = 0; int kn2 = 0; int g = 0;bool ja = true;
                while (dt_1.Rows.Count > 0)
                {
                    wbs = ""; prod = ""; count = ""; string cl = "", cost = "", sum = "", k1, k3 = "", k_tov = "", last_id = "", k2, k_n = "" ,term=""; DateTime dat = new DateTime();term = Convert.ToString(dat); dat = DateTime.Today ;
                    if (con.State == System.Data.ConnectionState.Closed)// НЕЛЯЗЬ ИСПОЛЬЗОВАТЬ ОДИН КОНЕКТ ДЛЯ РИДЕРА И ДЛЯ ЕКЗЕКЬЮТА
                        await con.OpenAsync();                          // НЕ ПОЛУЧАЕТСЯ ВЫПОЛНИТЬ ЗАПРОС ВО ВРЕМЯ ЗАПРОС, ПРОБУЮ АСИНХРОН
                    wbs = com3.ExecuteScalar().ToString();   /// rabotaet
                    prod = com3_1.ExecuteScalar().ToString();
                    count = com3_2.ExecuteScalar().ToString();  //ПОФИКСИТЬ !!! СПИСЫВАЕТ РАЗНИЦУ В ТОВАРЕ СО СКЛАДА!!!
                    com7.Parameters.AddWithValue("wbs", wbs);
                    
                    
                    com5.Parameters.AddWithValue("prod", prod);
                    com5.Parameters.AddWithValue("count", count);//ne
                    
                    com6_2.Parameters.AddWithValue("prod", prod);
                    com71.Parameters.AddWithValue("wbs", wbs);
                    
                    com5.ExecuteNonQuery(); // ОБНОВЛЯЕМ КОЛ-ВО ТОВАРА НА СКЛАДЕ
                    com8.Parameters.AddWithValue("wbs", wbs);
                    com6_1.Parameters.AddWithValue("wbs", wbs);
                    com4.Parameters.AddWithValue("wbs", wbs);   // da
                    com4.Parameters.AddWithValue("prod", prod);
                    com4_1.Parameters.AddWithValue("wbs", wbs);
                    com4_1.Parameters.AddWithValue("prod", prod);
                    com4_1.ExecuteNonQuery();   //УДАЛЯЕМ ЗАПИСЬ ИЗ ТМП
                    com4.ExecuteNonQuery(); // УДАЛЯЕМ ИЗ МАТЕРИАЛОВ
                    k_tov = com7.ExecuteScalar().ToString();
                    cl = com6_1.ExecuteScalar().ToString();
                    cost = com6_2.ExecuteScalar().ToString();
                    last_id = com8.ExecuteScalar().ToString();
                    k2 = com8_1.ExecuteScalar().ToString();
                    int c1 = int.Parse(cost); int k = int.Parse(count); int i = 0;int kt = int.Parse(k_tov); int last = int.Parse(last_id);
                    c1 = c1 * k; int kk2 = int.Parse(k2);
                    sum = Convert.ToString(c1); 
                    
                    
                    k_n = com71.ExecuteScalar().ToString();
                    int kn = int.Parse(k_n);
                        kn2 = kn; //kolvo ne obrabot tovara
                    //MessageBox.Show(wbs,wbs_1);

                    if (kn != kt)
                    {
                        if (wbs == wbs_1)
                            kt = kt_1;

                       // MessageBox.Show(kn.ToString(), kt.ToString());

                        if (kn2 > 0)
                        {
                            MySqlCommand com6_42 = new MySqlCommand("insert into tmp_wbsp values(@last_1,@prod,@count)", con);
                            if (wbs == wbs_1)
                            {
                                //MessageBox.Show("takaya est", wbs);
                                MySqlCommand com6_4 = new MySqlCommand("update waybill_s set wbs_sum = wbs_sum + @sum where id_wbs = @last_1", con);
                                MySqlCommand com6_41 = new MySqlCommand("update waybill_s set wbs_sum = wbs_sum - @sum where id_wbs = @wbs", con);
                                
                                com6_4.Parameters.AddWithValue("sum", sum); com6_4.Parameters.AddWithValue("last_1", last_1);
                                com6_41.Parameters.AddWithValue("wbs", wbs);
                                com6_41.Parameters.AddWithValue("sum", sum);
                                com6_42.Parameters.AddWithValue("last_1", last_1);
                               com6_42.Parameters.AddWithValue("prod", prod);
                               com6_42.Parameters.AddWithValue("count", count);
                                com6_4.ExecuteNonQuery(); com6_41.ExecuteNonQuery(); com6_42.ExecuteNonQuery();
                                com6_4.Parameters.Clear(); com6_41.Parameters.Clear(); com6_42.Parameters.Clear();

                                //com2.ExecuteNonQuery();

                            }
                            else
                            {
                                if (kk2 == 0)
                                {
                                    wbs = "200";
                                }
                                else
                                {
                                    if (j > 0 && ja == true)
                                        last_id = last_1;
                                    last = int.Parse(last_id);
                                    last_id = Convert.ToString(last);
                                    com6_3.Parameters.AddWithValue("last_id", last_id); com6_5.Parameters.AddWithValue("last_id", last_id);
                                    k1 = com6_3.ExecuteScalar().ToString();
                                    k3 = com6_5.ExecuteScalar().ToString();
                                    while (k1 != "0" || k3 != "0")
                                    {
                                        com6_3.Parameters.Clear(); com6_5.Parameters.Clear();
                                        last++;
                                        last_id = Convert.ToString(last);
                                        com6_3.Parameters.AddWithValue("last_id", last_id);
                                        com6_5.Parameters.AddWithValue("last_id", last_id);
                                        k1 = com6_3.ExecuteScalar().ToString();
                                        k3 = com6_5.ExecuteScalar().ToString();
                                        i++;
                                    }
                                    //last = last - i;
                                    last_id = Convert.ToString(last);
                                    last_1 = last_id;
                                    ja = true;
                                }
                                //MessageBox.Show(wbs_1, cl);
                                MySqlCommand com6_43 = new MySqlCommand("update waybill_s set wbs_sum = wbs_sum - @sum where id_wbs = @wbs", con);
                                com6.Parameters.AddWithValue("last_id", last_id); com6_43.Parameters.AddWithValue("wbs", wbs);
                                com6.Parameters.AddWithValue("cl", cl); com6_43.Parameters.AddWithValue("sum", sum);
                                com6.Parameters.AddWithValue("sum", sum);
                                com6.Parameters.AddWithValue("dat", dat);
                                com6_42.Parameters.AddWithValue("last_1", last_1);
                                com6_42.Parameters.AddWithValue("prod", prod);
                                com6_42.Parameters.AddWithValue("count", count);
                                com6.ExecuteNonQuery(); com6_43.ExecuteNonQuery(); com6_42.ExecuteNonQuery();
                                com6_43.Parameters.Clear(); com6_42.Parameters.Clear();
                            }
                            // }
                        }

                        kt_1 = kt;
                    }
                    else
                        ja = false;
                    j++;
                    wbs_1 = wbs;
                    
                    com4.Parameters.Clear();
                    com4_1.Parameters.Clear(); com71.Parameters.Clear();
                    com5.Parameters.Clear(); com7.Parameters.Clear(); com8.Parameters.Clear(); com6_5.Parameters.Clear();
                    com6.Parameters.Clear(); com6_1.Parameters.Clear(); com6_2.Parameters.Clear(); com6_3.Parameters.Clear(); 
                    dt_1.Clear();
                    rd = await com1.ExecuteReaderAsync();
                    dt_1.Load(rd);
                 }
         
                com2.ExecuteNonQuery();  //УДАЛЕНИЕ ВРЕМ ТАБЛ
                rd.Close();
            }
        }

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_spt(id_wbs int(3),id_prod varchar(40),count int(3));", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_spt select waybill_sp.id_wbs as 'Номер накладной', assortment.prod_name as 'Название товара', waybill_sp.wbs_count as 'Количество товара'" +
                    " from waybill_sp, assortment, waybill_s where waybill_sp.id_prod = assortment.id_prod and waybill_sp.id_wbs = waybill_s.id_wbs", con);
                MySqlCommand com = new MySqlCommand("select id_wbs as 'Номер накладной', id_prod as 'Название товара', count as 'Количество товара' from waybill_spt", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_spt", con);

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                waybill_sForm.re_table1();
                re_table();
                udovl_zakazov();
                cr_zayvka();
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

