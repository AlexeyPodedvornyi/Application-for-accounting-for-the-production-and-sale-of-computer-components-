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
    public partial class req_costsForm : Form
    {
        public req_costsForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);

        private void req_costsForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();


            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table req_costst(id_req int(3),id_mat varchar(40),reqm_count int(3));", con);
                MySqlCommand com1 = new MySqlCommand("insert into req_costst SELECT req_costs.id_req AS 'Номер заявки', mat_stor.mat_name AS 'Название материала', req_costs.reqm_count AS 'Количество материала'" +
                    " FROM req_costs, mat_stor WHERE mat_stor.id_mat = req_costs.id_mat;", con);
                MySqlCommand com = new MySqlCommand("select id_req as 'Номер заявки', id_mat as 'Название материала', reqm_count as 'Количество материала' from req_costst", con);
                MySqlCommand com3 = new MySqlCommand("drop table req_costst", con);

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

        private async void cr_post()
        {
            String data,prov,req, mat, count, count_req, count_t2,wbp;
            DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com1 = new MySqlCommand("select * from req_costs", con); //select t1.id_prod from t1,waybill_s where t1.id_wbs =  waybill_s.id_wbs order by waybill_s.wbs_dt limit 1
                MySqlCommand com3 = new MySqlCommand("select req_costs.id_req from req_costs,prod_req where req_costs.id_req = prod_req.id_req order by prod_req.req_term limit 1", con);
                MySqlCommand com3_1 = new MySqlCommand("select req_costs.id_mat from req_costs,prod_req where req_costs.id_req = prod_req.id_req order by prod_req.req_term limit 1", con);
               // MySqlCommand com3_2 = new MySqlCommand("select req_costs.reqm_count from req_costs,prod_req where req_costs.id_req = prod_req.id_req order by prod_req.req_term limit 1", con);
                MySqlCommand com4 = new MySqlCommand("delete from req_costs where id_mat = @mat", con);
               
                MySqlCommand comp = new MySqlCommand("select id_prov from prov_mat where id_mat = @mat order by dt limit 1", con);
                MySqlCommand comp_cs = new MySqlCommand("select cost_1 from prov_mat where id_prov = @prov and id_mat = @mat", con);
                MySqlCommand comdt = new MySqlCommand("select req_term from prod_req where id_req = @req ", con);
                MySqlCommand comlast = new MySqlCommand("select id_wbp from wbp_status order by id_wbp desc limit 1", con);
                MySqlCommand comc1 = new MySqlCommand("select count(*) from wbp_status ", con);
                MySqlCommand comc2 = new MySqlCommand("select count(*) from waybill_p ", con);
                MySqlCommand com6_3 = new MySqlCommand("select count(id_wbp) from wbp_status where id_wbp = @last_id", con);
                MySqlCommand com6_5 = new MySqlCommand("select count(id_wbp) from waybill_p where id_wbp = @last_id", con);
                MySqlCommand commat = new MySqlCommand("select sum(reqm_count) from req_costs where id_mat = @mat", con);
                MySqlCommand comtmp = new MySqlCommand("create temporary table wbp_prov(wbp int (3) ,prov int(3));", con);
                MySqlCommand comtmpd = new MySqlCommand("drop table wbp_prov;", con);
                MySqlCommand comtmp1 = new MySqlCommand("select wbp from wbp_prov where prov = @prov", con);
                MySqlCommand comtmp2 = new MySqlCommand("insert into wbp_prov values (@last_1,@prov)", con);
               // MySqlCommand comtmp3 = new MySqlCommand("insert into wbp_prov (wbp) values (@last_1)", con);
                // MySqlCommand comwbpm = new MySqlCommand("insert into waybill_pm values (@last_1,@mat,@count,@cost,@term)", con);
                await con.OpenAsync();
                System.Data.Common.DbDataReader rd = await com1.ExecuteReaderAsync();  //Для mysql не существует типа асинхрона MySqlDataReader
                
                dt_1.Load(rd);
                string prov_1 = "0", req_1="0",wbp_1 = "0", last_1 = "", td = "",cr="",cr2 = "" ; int kt_1 = 1; int j = 0; int kn2 = 0; int g = 0; bool ja = true;
                while (dt_1.Rows.Count > 0)
                {
                    data = ""; prov=""; wbp = ""; req = ""; mat = ""; count = ""; string cl = "", cost = "", sum = "", k1, k3 = "", k_tov = "", last_id = "", k2, k_n = "",y="",m="",d="";
                    DateTime dat = new DateTime(); dat = DateTime.Today; DateTime term = new DateTime(); bool f = false;
                    if (con.State == System.Data.ConnectionState.Closed)// НЕЛЯЗЬ ИСПОЛЬЗОВАТЬ ОДИН КОНЕКТ ДЛЯ РИДЕРА И ДЛЯ ЕКЗЕКЬЮТА
                        await con.OpenAsync();                          // НЕ ПОЛУЧАЕТСЯ ВЫПОЛНИТЬ ЗАПРОС ВО ВРЕМЯ ЗАПРОС, ПРОБУЮ АСИНХРОН
                    req = com3.ExecuteScalar().ToString();   /// rabotaet
                    mat = com3_1.ExecuteScalar().ToString();
                    commat.Parameters.AddWithValue("mat", mat);
                    count = commat.ExecuteScalar().ToString();  //ПОФИКСИТЬ !!! СПИСЫВАЕТ РАЗНИЦУ В ТОВАРЕ СО СКЛАДА!!!
                    comdt.Parameters.AddWithValue("req", req);
                    data = comdt.ExecuteScalar().ToString();
                    //y = data.Substring(0, data.Length - 8);
                   // y = y.Substring(8);
                   // d = data.Substring(0, data.Length - 16);
                    //m = data.Substring(3);
                    //m = m.Substring(0, m.Length - 13);
                    //data = y + "." + m + "." + d;
                   // MessageBox.Show("data", data);
                    dat = Convert.ToDateTime(data);
                    dat = dat.AddDays(-1);
                   // MessageBox.Show("dat",dat.ToString());
                    term = dat.AddYears(3);
                    
                   // MessageBox.Show("term", term.ToString());
                    //com4.Parameters.AddWithValue("req", req);   // da
                    com4.Parameters.AddWithValue("mat", mat);
                    //com4.ExecuteNonQuery(); // УДАЛЯЕМ ИЗ МАТЕРИАЛОВ
                    comp.Parameters.AddWithValue("mat", mat);  
                    prov = comp.ExecuteScalar().ToString();
                    comp_cs.Parameters.AddWithValue("prov", prov);
                    comp_cs.Parameters.AddWithValue("mat", mat);
                    cost = comp_cs.ExecuteScalar().ToString();
                    
                    //k2 = com8_1.ExecuteScalar().ToString();
                    int c1 = int.Parse(cost); int k = int.Parse(count); int i = 0; //int kt = int.Parse(k_tov);
                    
                    c1 = c1 * k; //int kk2 = int.Parse(k2);
                    cost = Convert.ToString(c1);
                    cr = comc1.ExecuteScalar().ToString();
                    cr2 = comc2.ExecuteScalar().ToString();
                    int kr = int.Parse(cr); int kr2 = int.Parse(cr2);
                    if (kr == 0 && kr2 == 0)
                        last_id = "1";
                    else
                    {
                        if (kr != 0)
                            if (j == 0)
                                last_id = comlast.ExecuteScalar().ToString();
                            else
                                last_id = last_1;
                        else
                            last_id = last_1;
                        //MessageBox.Show("nn");
                    }
                    int last = int.Parse(last_id);
                    if (req!=req_1)
                    {
                           if(j>0)
                           comtmpd.ExecuteNonQuery();
                      comtmp.ExecuteNonQuery();
                    }
                    MySqlCommand com6_47 = new MySqlCommand("select count(*) from wbp_prov where prov = @prov", con);
                    com6_47.Parameters.Clear();
                    com6_47.Parameters.AddWithValue("prov", prov);
                    cl = com6_47.ExecuteScalar().ToString();
                    int cl1 = int.Parse(cl);
                    if (req == req_1 && cl1 > 0)
                    {
                        comtmp1.Parameters.Clear();
                        comtmp1.Parameters.AddWithValue("prov", prov);
                        td = comtmp1.ExecuteScalar().ToString();
                        MySqlCommand com6_46 = new MySqlCommand("select count(*) from waybill_p where id_wbp = @td", con);
                        com6_46.Parameters.Clear();
                        com6_46.Parameters.AddWithValue("td", td);
                        cl = com6_46.ExecuteScalar().ToString();
                        //int cl1 = int.Parse(cl);
                    }
                    else
                        cl = "";
                    if (req == req_1 && prov ==prov_1)
                    {
                        //MessageBox.Show("takaya est", wbs);
                        MySqlCommand com6_4 = new MySqlCommand("update waybill_p set wpb_sum = wpb_sum + @cost where id_wbp = @last_1", con);
                        MySqlCommand com6_41 = new MySqlCommand("insert into waybill_pm values (@last_1,@mat,@count,@cost,@term)", con);        
                        //MySqlCommand comp2 = new MySqlCommand("select id_wbp from waybill_p where id_prov = @ order by dt limit 1", con);
                        com6_4.Parameters.AddWithValue("cost", cost); com6_4.Parameters.AddWithValue("last_1", last_1);
                        com6_41.Parameters.AddWithValue("last_1", last_1);
                        com6_41.Parameters.AddWithValue("cost", cost);
                        com6_41.Parameters.AddWithValue("mat", mat);
                        com6_41.Parameters.AddWithValue("count", count);
                        com6_41.Parameters.AddWithValue("term", term);
                        com6_41.ExecuteNonQuery();
                        com6_4.ExecuteNonQuery();
                        com6_4.Parameters.Clear();
                        com6_41.Parameters.Clear(); 

                        //com2.ExecuteNonQuery();

                    }
                    else
                    {

                        if (req == req_1 && cl == "1")
                        {
                            MySqlCommand com6_44 = new MySqlCommand("update waybill_p set wpb_sum = wpb_sum + @cost where id_wbp = @td", con);
                            MySqlCommand com6_45 = new MySqlCommand("insert into waybill_pm values (@td,@mat,@count,@cost,@term)", con);
                            com6_44.Parameters.AddWithValue("cost", cost); com6_44.Parameters.AddWithValue("td", td);
                            com6_45.Parameters.AddWithValue("td", td);
                            com6_45.Parameters.AddWithValue("cost", cost);
                            com6_45.Parameters.AddWithValue("mat", mat);
                            com6_45.Parameters.AddWithValue("count", count);
                            com6_45.Parameters.AddWithValue("term", term);
                            com6_45.ExecuteNonQuery();
                            com6_44.ExecuteNonQuery();
                            com6_44.Parameters.Clear();
                            com6_45.Parameters.Clear();
                        }
                        else
                        {
                            if (last_1.Length != 0)
                                last_id = last_1;
                            else
                            {
                                last = int.Parse(last_id);
                                last_id = Convert.ToString(last);
                            }
                            last = int.Parse(last_id);
                            last_id = Convert.ToString(last); com6_3.Parameters.Clear(); com6_5.Parameters.Clear();
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

                            //MessageBox.Show(wbs_1, cl);
                            //  MySqlCommand com6_43 = new MySqlCommand("update waybill_p set wbp_sum = wbs_sum - @sum where id_wbs = @wbs", con);
                            sum = "0";
                            MySqlCommand com6 = new MySqlCommand("insert into waybill_p values(@last_id,@prov,@sum,@dat)", con);
                            com6.Parameters.AddWithValue("last_id", last_id);// com6_43.Parameters.AddWithValue("wbs", wbs);
                            com6.Parameters.AddWithValue("prov", prov); //com6_43.Parameters.AddWithValue("sum", sum);
                            com6.Parameters.AddWithValue("sum", sum);
                            com6.Parameters.AddWithValue("dat", dat);
                            com6.ExecuteNonQuery(); com6.Parameters.Clear();
                            MySqlCommand com6_41 = new MySqlCommand("insert into waybill_pm values (@last_1,@mat,@count,@cost,@term)", con);
                            MySqlCommand com6_4 = new MySqlCommand("update waybill_p set wpb_sum = wpb_sum + @cost where id_wbp = @last_1", con);
                            com6_4.Parameters.AddWithValue("cost", cost); com6_4.Parameters.AddWithValue("last_1", last_1);
                            com6_41.Parameters.AddWithValue("last_1", last_1);
                            com6_41.Parameters.AddWithValue("cost", cost);
                            com6_41.Parameters.AddWithValue("mat", mat);
                            com6_41.Parameters.AddWithValue("count", count);
                            com6_41.Parameters.AddWithValue("term", term);
                            com6_41.ExecuteNonQuery();
                            com6_41.Parameters.Clear();
                            com6_4.ExecuteNonQuery();
                            com6_4.Parameters.Clear();
                            comtmp2.Parameters.AddWithValue("last_1", last_1);// com6_43.Parameters.AddWithValue("wbs", wbs);
                            comtmp2.Parameters.AddWithValue("prov", prov);
                            comtmp2.ExecuteNonQuery(); comtmp2.Parameters.Clear();
                        }
                    }
                    com4.ExecuteNonQuery();

                    // kt_1 = kt;
                    //}
                    // else
                    // ja = false;
                     j++;
                    req_1 = req;
                    prov_1 = prov;
                    com4.Parameters.Clear(); commat.Parameters.Clear(); comdt.Parameters.Clear(); comp.Parameters.Clear(); comp_cs.Parameters.Clear();
                    //com4_1.Parameters.Clear(); com71.Parameters.Clear();
                    //com5.Parameters.Clear(); com7.Parameters.Clear(); com8.Parameters.Clear(); 
                    com6_5.Parameters.Clear();
                     //com6_1.Parameters.Clear(); com6_2.Parameters.Clear(); com6_3.Parameters.Clear();
                    dt_1.Clear();
                    rd = await com1.ExecuteReaderAsync();
                    dt_1.Load(rd);
                }

                //com2.ExecuteNonQuery();  //УДАЛЕНИЕ ВРЕМ ТАБЛ
                rd.Close();
            }
        }

        private async void neudovl_zak() ////// ПРОВЕРИТЬ НА РАБОТОСПОСОБНОСТЬ, ВРОДЕ ЦИКЛ НЕ РАБОТАЛ 
                                              //ПРИШЛОСЬ ВРУЧНУЮ ТЫКАТЬ ЧТО ЗАПИСИ УШЛИ
        {
            String req, mat, count, count_req, count_t2;
            DataTable dt_1 = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("call neudovlet_zakaz", con); //select t1.id_wbs from t1,waybill_s where t1.id_wbs = waybill_s.id_wbs order by waybill_s.wbs_dt limit 1
                MySqlCommand com1 = new MySqlCommand("select * from t2", con);
                MySqlCommand com2 = new MySqlCommand("drop table t2", con);
                MySqlCommand com3 = new MySqlCommand("select t2.id_req from t2,prod_req where t2.id_req = prod_req.id_req order by prod_req.req_term limit 1", con);
                MySqlCommand com3_1 = new MySqlCommand("select t2.id_mat from t2,prod_req where t2.id_req = prod_req.id_req order by prod_req.req_term limit 1", con);
                MySqlCommand com3_2 = new MySqlCommand("select t2.req_count from t2,prod_req where t2.id_req = prod_req.id_req order by prod_req.req_term limit 1", con);
                MySqlCommand com_c1 = new MySqlCommand("select count(*) from t2 where id_req = @req", con);
                MySqlCommand com_c2 = new MySqlCommand("select count(*) from req_costs where id_req = @req", con);
                MySqlCommand com4 = new MySqlCommand("delete from req_costs where id_req = @req and id_mat = @mat", con);
                MySqlCommand com4_1 = new MySqlCommand("delete from t2 where id_req = @req and id_mat = @mat", con);
                MySqlCommand com5 = new MySqlCommand("update mat_stor set stor_count = stor_count - @count " +
               "where id_mat = @mat", con);

                await con.OpenAsync();
                await com.ExecuteNonQueryAsync();
                System.Data.Common.DbDataReader rd = await com1.ExecuteReaderAsync();  //Для mysql не существует типа асинхрона MySqlDataReader

                dt_1.Load(rd);

                int a = dt_1.Rows.Count;
               // MessageBox.Show("Rows count is = " + a);
                while (dt_1.Rows.Count > 0)
                {   
                    req = ""; mat = ""; count_t2 = ""; count_req = ""; count = "";
                    if (con.State == System.Data.ConnectionState.Closed)// НЕЛЯЗЬ ИСПОЛЬЗОВАТЬ ОДИН КОНЕКТ ДЛЯ РИДЕРА И ДЛЯ ЕКЗЕКЬЮТА
                        await con.OpenAsync();                          // НЕ ПОЛУЧАЕТСЯ ВЫПОЛНИТЬ ЗАПРОС ВО ВРЕМЯ ЗАПРОС, ПРОБУЮ АСИНХРОН
                    req = com3.ExecuteScalar().ToString();   /// rabotaet
                    mat = com3_1.ExecuteScalar().ToString();
                    com_c1.Parameters.AddWithValue("req", req);
                    com_c2.Parameters.AddWithValue("req", req);
                    count = com3_2.ExecuteScalar().ToString();// iz t2
                    count_t2 = com_c1.ExecuteScalar().ToString();  //кол-во записей по 1-ой заявке из т2
                    count_req = com_c2.ExecuteScalar().ToString();//кол-во записей по 1-ой заявке из затрат на заявку
                   // MessageBox.Show(count);
                    //MessageBox.Show(count_t2, count_req);
                   // if (count_req == count_t2)
                 //  {
                        com4.Parameters.AddWithValue("req", req);   // 
                        com4.Parameters.AddWithValue("mat", mat);
                        com4_1.Parameters.AddWithValue("req", req);
                        com4_1.Parameters.AddWithValue("mat", mat);
                        com5.Parameters.AddWithValue("mat", mat);
                        com5.Parameters.AddWithValue("count", count);//ne
                        com4_1.ExecuteNonQuery();//удаляем обработанные iz t2
                        com4.ExecuteNonQuery();//удаляем обработанные iz req
                        com5.ExecuteNonQuery();//обновляем кол-во на складе
                       
                   // }

                   /* else
                    {
                        MessageBox.Show("else");
                        com4_1.Parameters.AddWithValue("req", req);
                        com4_1.Parameters.AddWithValue("mat", mat);
                        com4_1.ExecuteNonQuery(); //удаляем записи из т2,  где кол-во мат по т2 != кол-ву по заявке
                    }*/

                    com_c1.Parameters.Clear();
                    com_c2.Parameters.Clear();
                    com4.Parameters.Clear();
                    com4_1.Parameters.Clear();
                    com5.Parameters.Clear();
                    rd = await com1.ExecuteReaderAsync();
                    dt_1.Clear();
                    dt_1.Load(rd); // ????????????????????????????????  
                }

                rd.Close();
                com2.ExecuteNonQuery();// drop table t2

            }
        }

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table req_costst(id_req int(3),id_mat varchar(40),reqm_count int(3));", con);
                MySqlCommand com1 = new MySqlCommand("insert into req_costst SELECT req_costs.id_req AS 'Номер заявки', mat_stor.mat_name AS 'Название материала', req_costs.reqm_count AS 'Количество материала'" +
                    " FROM req_costs, mat_stor WHERE mat_stor.id_mat = req_costs.id_mat;", con);
                MySqlCommand com = new MySqlCommand("select id_req as 'Номер заявки', id_mat as 'Название материала', reqm_count as 'Количество материала' from req_costst", con);
                MySqlCommand com3 = new MySqlCommand("drop table req_costst", con);

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
                reqForm.re_table2();
                neudovl_zak();
                cr_post();
            }
            catch(MySqlException)
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
