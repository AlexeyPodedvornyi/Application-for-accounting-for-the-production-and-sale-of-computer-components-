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
    public partial class wbs_statusForm : Form
    {
        public wbs_statusForm()
        {
            InitializeComponent();
        }
        public static string connect_1 = "server=localhost;port=3306;username=root;password=root;database=lab";
        MySqlConnection MC = new MySqlConnection(connect_1);
        private void wbs_statusForm_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ

            DataTable dt = new DataTable();


            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select last_wbs.id_wbs as 'Номер накладной', clients.dir_FIO as 'ФИ клиента', last_wbs.wbs_sum as 'Сумма по накладной', " +
                    "last_wbs.wbs_dt as 'Дата поставки' from last_wbs,clients where last_wbs.id_client = clients.id_client limit 10", con);

                con.Open();
                MySqlDataReader rd_1 = com.ExecuteReader();
                // MySqlDataReader rd = com.ExecuteReader();
                dt.Load(rd_1);

                if (dt.Rows.Count > 0)
                {
                    tabl.DataSource = dt;
                }

                rd_1.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
