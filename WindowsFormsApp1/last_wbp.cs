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
    public partial class last_wbp : Form
    {
        public last_wbp()
        {
            InitializeComponent();
        }

        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);


        private void button1_Click(object sender, EventArgs e)
        {
          this.Close();
        }

        private void last_wbp_Load(object sender, EventArgs e)
        {
            tabl.AllowUserToAddRows = false;    /////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ ДОБАВЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.AllowUserToDeleteRows = false; ////ЗАПРЩАЕМ ПОЛЬЗОВАТЕЛЯМ УДАЛЯТЬ СТРОЧКИ КЛИКОМ НА ПОЛЕ
            tabl.ReadOnly = true;               ////АКТИВИРУЕМ РЕЖИМ ТОЛЬКО ЧТЕНИЕ

            DataTable dt = new DataTable();


            using (MySqlConnection con = new MySqlConnection(connect_1))
            {
                MySqlCommand com = new MySqlCommand("select id_wbp as 'Номер накладной',id_prov as 'Номер поставщика', " +
                    "wbp_sum as 'Сумма по накладной', wbp_dt as 'Дата поставки' from tmp_wbp limit 10", con);

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

    }
}
