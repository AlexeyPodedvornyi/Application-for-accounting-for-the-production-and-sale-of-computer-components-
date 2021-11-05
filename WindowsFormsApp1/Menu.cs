using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Menu : Form
    {
        public static string connect_1 = Autor_R.connect_1;
        MySqlConnection MC = new MySqlConnection(connect_1);
        

        public Menu(ref string log, ref string login)
        {
            MySqlCommand com0 = new MySqlCommand("insert into log(user_log,action,query_time) value (@login,'Авторизация',@time)", MC);
            DateTime time = new DateTime(); time = DateTime.Now;
            com0.Parameters.AddWithValue("login", login); com0.Parameters.AddWithValue("time", time);
            if (MC.State == ConnectionState.Closed)
                MC.Open();
            com0.ExecuteNonQuery(); //com1.ExecuteNonQuery(); 
            int lvl = int.Parse(log);
            
            InitializeComponent();
            dez();
            if(lvl ==0)
            {
                butAdmin.Enabled = false; 
                butProizv.Enabled = false;
                butSklad.Enabled = false;
                but2P3.Enabled = false;
                but3P3.Enabled = false;
                but2P4.Enabled = false;
                but3P4.Enabled = false;
            }
            if(lvl == 1)    //dorabotat
            {
                butAdmin.Enabled = false;
                butProizv.Enabled = false;
                but1P3.Enabled = false;
                but1P4.Enabled = false;
                but1P5.Enabled = false;
                but4P3.Enabled = false;
            }
            if (lvl == 2)
            {
                butAdmin.Enabled = false;
                butSklad.Enabled = false;
                butPokup.Enabled = false;
                butProdaj.Enabled = false;
            }
            if (lvl == 3)
            {
                butAdmin.Enabled = false;
                butProizv.Enabled = false;

                but2P5.Enabled = false;
                but3P5.Enabled = false;
            }

        }

        
       
       
        private void dez () ////////скрываем панели////////////
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;

        }
        
        private void hidePanel()
        {
            if (panel1.Visible == true)
                panel1.Visible = false;
            if (panel2.Visible == true)
                panel2.Visible = false;
            if (panel3.Visible == true)
                panel3.Visible = false;
            if (panel4.Visible == true)
                panel4.Visible = false;
            if (panel5.Visible == true)
                panel5.Visible = false;
        }

        ///////////СОЗДАНИЕ МЕТОДА КОТОРЫЙ УПРАВЛЯЕТ ВИДИМОСТЬЮ//////////////////
        private void showPanel (Panel subPanel) // subPanel - объект класса Panel
        {
            if (subPanel.Visible == false)
            {
                //hidePanel();
                subPanel.Visible = true;
            }
            else
                subPanel.Visible = false;
        }

       

        /////////////// Загрузка списка таблиц /////////////////////
        private void Menu_Load(object sender, EventArgs e)
        {
            
        }
        

        //////////////КНОПКА ВЫХОДА ИЗ ПРОГРАММЫ//////////////////
        private void button_ex_Click(object sender, EventArgs e)
        {
           
            if (MC != null && MC.State != ConnectionState.Closed)//Закрываем соединение с БД
            {
                MC.Close();
                Application.Exit();
            }
            MC.Close();
            Application.Exit();
            // Menu menu = new Menu();
            //menu.Close();
        }

        //////////ВЫХОД ИЗ ПРОГРАММЫ С ПОМОЩЬЮ КРЕСТИКА////////////
        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            MySqlCommand com0 = new MySqlCommand("insert into log(user_log,action,query_time) value (@login,'Выход из системы',@time)", MC);
            DateTime time = new DateTime(); time = DateTime.Now; string login = Autor_R.current_login;
            com0.Parameters.AddWithValue("login", login); com0.Parameters.AddWithValue("time", time);
            if (MC.State == ConnectionState.Closed)
                MC.Open();
            com0.ExecuteNonQuery();
            if (MC != null && MC.State != ConnectionState.Closed)//Закрываем соединение с БД
            {
                MC.Close();
                Application.Exit();
            }
            MC.Close();
            Application.Exit();
        }

        public string name;        

        ///PANEL 1
        private void butAdmin_Click(object sender, EventArgs e)
        {
            hidePanel();
            showPanel(panel1); //В КАЧЕСТВЕ ПАРАМЕТРА УКАЗЫВАЕМ ПАНЕЛЬ КОТОРАЯ БУДЕТ ВЫПАДАТЬ ПО НАЖАТИЮ КНОПКИ

        }

        private void but1P1_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(70, 70, 82);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);

            openForm(new adm_Form());
        }

        ///PANEL 2
        private void butProizv_Click(object sender, EventArgs e)
        {
            hidePanel();
            showPanel(panel2);
        }

        private void but1P2_Click(object sender, EventArgs e)
        {   
            but1P2.BackColor = Color.FromArgb(70, 70, 82);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new reqForm());
        }

        private void but2P2_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(70, 70, 82);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new req_costsForm());
        }

        private void but3P2_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(70, 70, 82);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new form()); ///НАЗВАНИЕ КЛАССА ПРОСТО ФОРМ, ХЗ КАК ТАК ВЫШЛО?
        }


        ///PANEL 3
        private void butPokup_Click(object sender, EventArgs e)
        {
            hidePanel();
            showPanel(panel3);
        }

        private void but1P3_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(70, 70, 82);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new providerForm());
            //...
        }

        private void but2P3_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(70, 70, 82);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new waybill_pForm());
            //...

        }

        private void but3P3_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(70, 70, 82);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new waybill_pmForm());
            //...

        }

        ///PANEL 4
        private void butProdaj_Click(object sender, EventArgs e)
        {
            hidePanel();
            showPanel(panel4);
        }

        private void but1P4_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(70, 70, 82);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new clientsForm());

        }

        private void but2P4_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(70, 70, 82);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new waybill_sForm());

        }

        private void but3P4_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(70, 70, 82);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);

            openForm(new waybill_spForm());

        }

        ///PANEL 5
        private void butSklad_Click(object sender, EventArgs e)
        {
            hidePanel();
            showPanel(panel5);
        }

        private void but1P5_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(70, 70, 82);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new assortmentForm());
        }

        private void but2P5_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(70, 70, 82);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new prod_storForm());

        }

        private void but3P5_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(70, 70, 82);
            but4P3.BackColor = Color.FromArgb(54, 56, 69);
            openForm(new mat_storForm());
            
        }
        private void but4P3_Click(object sender, EventArgs e)
        {
            but1P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P1.BackColor = Color.FromArgb(54, 56, 69);
            but2P2.BackColor = Color.FromArgb(54, 56, 69);
            but3P2.BackColor = Color.FromArgb(54, 56, 69);
            but1P3.BackColor = Color.FromArgb(54, 56, 69);
            but2P3.BackColor = Color.FromArgb(54, 56, 69);
            but3P3.BackColor = Color.FromArgb(54, 56, 69);
            but1P4.BackColor = Color.FromArgb(54, 56, 69);
            but2P4.BackColor = Color.FromArgb(54, 56, 69);
            but3P4.BackColor = Color.FromArgb(54, 56, 69);
            but1P5.BackColor = Color.FromArgb(54, 56, 69);
            but2P5.BackColor = Color.FromArgb(54, 56, 69);
            but3P5.BackColor = Color.FromArgb(54, 56, 69);
            but4P3.BackColor = Color.FromArgb(70, 70, 82);
            openForm(new prov_matForm());
        }
        private Form activeForm = null;
        private void openForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

    }
}
