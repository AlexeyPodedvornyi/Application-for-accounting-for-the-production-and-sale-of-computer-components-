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
    public partial class waybill_pmForm : Form
    {
        public waybill_pmForm()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);


        private void waybill_pmForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ
            
            // ВЫВОД ТАБЛИЦЫ
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
               // MySqlCommand com = new MySqlCommand("select id_wbp as 'Номер накладной',id_mat as 'Номер материала', wbp_count as 'Количество', wbp_cost as 'Цена', wbp_term as 'Срок годности' from waybill_pm", con);
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_pmt(id_wbp int(3),id_mat varchar(40),count int(3),cost bigint,term date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_pmt SELECT waybill_pm.id_wbp AS 'Номер накладной', mat_stor.mat_name AS 'Название материала', waybill_pm.wbp_count AS" +
                    " 'Количество по накладной', waybill_pm.wbp_cost AS 'Цена', waybill_pm.wbp_term 'Срок годности' FROM waybill_pm, mat_stor WHERE waybill_pm.id_mat = mat_stor.id_mat;", con);
                MySqlCommand com = new MySqlCommand("select id_wbp as 'Номер накладной', id_mat as 'Название материала', count as 'Количество материала', cost as 'Цена', term as 'Срок годности' from waybill_pmt", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_pmt", con);

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

        private void refresh_but_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com2 = new MySqlCommand("create temporary table waybill_pmt(id_wbp int(3),id_mat varchar(40),count int(3),cost bigint,term date);", con);
                MySqlCommand com1 = new MySqlCommand("insert into waybill_pmt SELECT waybill_pm.id_wbp AS 'Номер накладной', mat_stor.mat_name AS 'Название материала', waybill_pm.wbp_count AS" +
                    " 'Количество по накладной', waybill_pm.wbp_cost AS 'Цена', waybill_pm.wbp_term 'Срок годности' FROM waybill_pm, mat_stor WHERE waybill_pm.id_mat = mat_stor.id_mat;", con);
                MySqlCommand com = new MySqlCommand("select id_wbp as 'Номер накладной', id_mat as 'Название материала', count as 'Количество материала', cost as 'Цена', term as 'Срок годности' from waybill_pmt", con);
                MySqlCommand com3 = new MySqlCommand("drop table waybill_pmt", con);

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
    }
}
