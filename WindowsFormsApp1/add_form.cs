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
    public partial class add_form : Form
    {
        public add_form()
        {
            InitializeComponent();
        }

        private void add_form_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Button add = new System.Windows.Forms.Button(); // создаем контрол
            add.Location = new System.Drawing.Point(101, 50); // устанавливаем необходимые свойства
            add.Name = "add";
            add.Size = new System.Drawing.Size(75, 23);
            add.TabIndex = 0;
            add.Text = "Добавить запись";
            add.UseVisualStyleBackColor = true;
            add.Click += new System.EventHandler(add_Click); // button1_Click - функция обработчик события нажатия на кнопку
            Controls.Add(add); // добавляем на форму
        }

        private void add_Click(object sender, EventArgs e) /////// СОБЫТИЕ ПРИ НАЖАТИИ НА КНОПКУ
        {

        }
    }
}
