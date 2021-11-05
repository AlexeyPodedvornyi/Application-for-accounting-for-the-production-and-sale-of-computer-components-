namespace WindowsFormsApp1
{
    partial class prov_matForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.refresh_but = new System.Windows.Forms.Button();
            this.tabl = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.a_box4 = new System.Windows.Forms.TextBox();
            this.a_cb2 = new System.Windows.Forms.ComboBox();
            this.a_cb1 = new System.Windows.Forms.ComboBox();
            this.butAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.a_box3 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.e_box4 = new System.Windows.Forms.TextBox();
            this.e_cb2 = new System.Windows.Forms.ComboBox();
            this.e_cb1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.butEdit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.e_box3 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.butDel = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tabl)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // refresh_but
            // 
            this.refresh_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_but.ForeColor = System.Drawing.SystemColors.Control;
            this.refresh_but.Location = new System.Drawing.Point(739, 481);
            this.refresh_but.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.refresh_but.Name = "refresh_but";
            this.refresh_but.Size = new System.Drawing.Size(112, 36);
            this.refresh_but.TabIndex = 38;
            this.refresh_but.Text = "Обновить";
            this.refresh_but.UseVisualStyleBackColor = true;
            this.refresh_but.Click += new System.EventHandler(this.refresh_but_Click);
            // 
            // tabl
            // 
            this.tabl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabl.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.tabl.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.tabl.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tabl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabl.Location = new System.Drawing.Point(14, 31);
            this.tabl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabl.Name = "tabl";
            this.tabl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.tabl.RowTemplate.Height = 24;
            this.tabl.Size = new System.Drawing.Size(838, 442);
            this.tabl.TabIndex = 37;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 566);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(865, 249);
            this.tabControl1.TabIndex = 39;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(69)))));
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.a_box4);
            this.tabPage2.Controls.Add(this.a_cb2);
            this.tabPage2.Controls.Add(this.a_cb1);
            this.tabPage2.Controls.Add(this.butAdd);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.a_box3);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(857, 216);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Добавить материал";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(321, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "Стоимость / шт.";
            // 
            // a_box4
            // 
            this.a_box4.Location = new System.Drawing.Point(458, 107);
            this.a_box4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.a_box4.Name = "a_box4";
            this.a_box4.Size = new System.Drawing.Size(112, 26);
            this.a_box4.TabIndex = 28;
            // 
            // a_cb2
            // 
            this.a_cb2.FormattingEnabled = true;
            this.a_cb2.Location = new System.Drawing.Point(450, 31);
            this.a_cb2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.a_cb2.Name = "a_cb2";
            this.a_cb2.Size = new System.Drawing.Size(244, 28);
            this.a_cb2.TabIndex = 27;
            // 
            // a_cb1
            // 
            this.a_cb1.FormattingEnabled = true;
            this.a_cb1.Location = new System.Drawing.Point(126, 31);
            this.a_cb1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.a_cb1.Name = "a_cb1";
            this.a_cb1.Size = new System.Drawing.Size(145, 28);
            this.a_cb1.TabIndex = 26;
            // 
            // butAdd
            // 
            this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAdd.ForeColor = System.Drawing.SystemColors.Control;
            this.butAdd.Location = new System.Drawing.Point(723, 144);
            this.butAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(112, 46);
            this.butAdd.TabIndex = 10;
            this.butAdd.Text = "Добавить";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(25, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Время поставки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(324, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Название мат.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(25, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Поставщик";
            // 
            // a_box3
            // 
            this.a_box3.Location = new System.Drawing.Point(173, 107);
            this.a_box3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.a_box3.Name = "a_box3";
            this.a_box3.Size = new System.Drawing.Size(112, 26);
            this.a_box3.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(69)))));
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.e_box4);
            this.tabPage3.Controls.Add(this.e_cb2);
            this.tabPage3.Controls.Add(this.e_cb1);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.comboBox2);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.comboBox1);
            this.tabPage3.Controls.Add(this.butEdit);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.e_box3);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(857, 216);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Редактировать материал";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(365, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 20);
            this.label7.TabIndex = 32;
            this.label7.Text = "Стоимость / шт.";
            // 
            // e_box4
            // 
            this.e_box4.Location = new System.Drawing.Point(502, 79);
            this.e_box4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_box4.Name = "e_box4";
            this.e_box4.Size = new System.Drawing.Size(112, 26);
            this.e_box4.TabIndex = 31;
            // 
            // e_cb2
            // 
            this.e_cb2.FormattingEnabled = true;
            this.e_cb2.Location = new System.Drawing.Point(488, 31);
            this.e_cb2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_cb2.Name = "e_cb2";
            this.e_cb2.Size = new System.Drawing.Size(283, 28);
            this.e_cb2.TabIndex = 30;
            // 
            // e_cb1
            // 
            this.e_cb1.FormattingEnabled = true;
            this.e_cb1.Location = new System.Drawing.Point(129, 31);
            this.e_cb1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_cb1.Name = "e_cb1";
            this.e_cb1.Size = new System.Drawing.Size(145, 28);
            this.e_cb1.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(23, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 28;
            this.label5.Text = "и материал";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(142, 167);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(254, 28);
            this.comboBox2.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(24, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(328, 20);
            this.label4.TabIndex = 26;
            this.label4.Text = "Выберите назв. изменяемого поставщика";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(369, 123);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(169, 28);
            this.comboBox1.TabIndex = 25;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // butEdit
            // 
            this.butEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butEdit.ForeColor = System.Drawing.SystemColors.Control;
            this.butEdit.Location = new System.Drawing.Point(723, 144);
            this.butEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(112, 46);
            this.butEdit.TabIndex = 20;
            this.butEdit.Text = "Изменить";
            this.butEdit.UseVisualStyleBackColor = true;
            this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label8.Location = new System.Drawing.Point(26, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Время поставки";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label9.Location = new System.Drawing.Point(366, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Название мат.\r\n";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10.Location = new System.Drawing.Point(28, 34);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "Поставщик";
            // 
            // e_box3
            // 
            this.e_box3.Location = new System.Drawing.Point(162, 79);
            this.e_box3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_box3.Name = "e_box3";
            this.e_box3.Size = new System.Drawing.Size(112, 26);
            this.e_box3.TabIndex = 12;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(69)))));
            this.tabPage4.Controls.Add(this.comboBox4);
            this.tabPage4.Controls.Add(this.comboBox3);
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.butDel);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(857, 216);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Удалить материал";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(508, 69);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(247, 28);
            this.comboBox4.TabIndex = 36;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(118, 69);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(180, 28);
            this.comboBox3.TabIndex = 35;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(17, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(527, 20);
            this.label11.TabIndex = 32;
            this.label11.Text = "Введите поставщика и материал, запись о которых хотите удалить";
            // 
            // butDel
            // 
            this.butDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butDel.ForeColor = System.Drawing.SystemColors.Control;
            this.butDel.Location = new System.Drawing.Point(723, 144);
            this.butDel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butDel.Name = "butDel";
            this.butDel.Size = new System.Drawing.Size(112, 46);
            this.butDel.TabIndex = 31;
            this.butDel.Text = "Удалить";
            this.butDel.UseVisualStyleBackColor = true;
            this.butDel.Click += new System.EventHandler(this.butDel_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(382, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 20);
            this.label14.TabIndex = 27;
            this.label14.Text = "Название мат.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label15.Location = new System.Drawing.Point(17, 72);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 20);
            this.label15.TabIndex = 26;
            this.label15.Text = "Поставщик";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label12.Location = new System.Drawing.Point(277, 492);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(297, 25);
            this.label12.TabIndex = 43;
            this.label12.Text = "Таблица не содержит записей";
            this.label12.Visible = false;
            // 
            // prov_matForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(865, 815);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.refresh_but);
            this.Controls.Add(this.tabl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "prov_matForm";
            this.Text = "prov_matForm";
            this.Load += new System.EventHandler(this.prov_matForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabl)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refresh_but;
        private System.Windows.Forms.DataGridView tabl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox a_box4;
        private System.Windows.Forms.ComboBox a_cb2;
        private System.Windows.Forms.ComboBox a_cb1;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox a_box3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox e_box4;
        private System.Windows.Forms.ComboBox e_cb2;
        private System.Windows.Forms.ComboBox e_cb1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button butEdit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox e_box3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button butDel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
    }
}