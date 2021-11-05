namespace WindowsFormsApp1
{
    partial class assortmentForm
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
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.a_box3 = new System.Windows.Forms.TextBox();
            this.a_box1 = new System.Windows.Forms.TextBox();
            this.e_box1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.e_box4 = new System.Windows.Forms.TextBox();
            this.e_box2 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.refresh_but = new System.Windows.Forms.Button();
            this.tabl = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.e_cb1 = new System.Windows.Forms.ComboBox();
            this.a_cb1 = new System.Windows.Forms.ComboBox();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.e_box1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabl)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.e_box1);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl2.Location = new System.Drawing.Point(0, 566);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(865, 249);
            this.tabControl2.TabIndex = 40;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(69)))));
            this.tabPage1.Controls.Add(this.a_cb1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.a_box3);
            this.tabPage1.Controls.Add(this.a_box1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(857, 216);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Добавление записей";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(723, 144);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 46);
            this.button1.TabIndex = 10;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(569, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 20);
            this.label14.TabIndex = 7;
            this.label14.Text = "Цена";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label16.Location = new System.Drawing.Point(311, 44);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 20);
            this.label16.TabIndex = 6;
            this.label16.Text = "№ товара";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label17.Location = new System.Drawing.Point(15, 44);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(141, 20);
            this.label17.TabIndex = 5;
            this.label17.Text = "Название товара";
            // 
            // a_box3
            // 
            this.a_box3.Location = new System.Drawing.Point(624, 44);
            this.a_box3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.a_box3.Name = "a_box3";
            this.a_box3.Size = new System.Drawing.Size(112, 26);
            this.a_box3.TabIndex = 4;
            // 
            // a_box1
            // 
            this.a_box1.Location = new System.Drawing.Point(159, 44);
            this.a_box1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.a_box1.Name = "a_box1";
            this.a_box1.Size = new System.Drawing.Size(112, 26);
            this.a_box1.TabIndex = 0;
            // 
            // e_box1
            // 
            this.e_box1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(69)))));
            this.e_box1.Controls.Add(this.e_cb1);
            this.e_box1.Controls.Add(this.label1);
            this.e_box1.Controls.Add(this.label2);
            this.e_box1.Controls.Add(this.label3);
            this.e_box1.Controls.Add(this.e_box4);
            this.e_box1.Controls.Add(this.e_box2);
            this.e_box1.Controls.Add(this.label22);
            this.e_box1.Controls.Add(this.comboBox1);
            this.e_box1.Controls.Add(this.button2);
            this.e_box1.Location = new System.Drawing.Point(4, 29);
            this.e_box1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_box1.Name = "e_box1";
            this.e_box1.Size = new System.Drawing.Size(857, 216);
            this.e_box1.TabIndex = 2;
            this.e_box1.Text = "Редактирование";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(569, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "Цена";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(311, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 29;
            this.label2.Text = "№ товара";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(15, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Название товара";
            // 
            // e_box4
            // 
            this.e_box4.Location = new System.Drawing.Point(624, 44);
            this.e_box4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_box4.Name = "e_box4";
            this.e_box4.Size = new System.Drawing.Size(112, 26);
            this.e_box4.TabIndex = 27;
            // 
            // e_box2
            // 
            this.e_box2.Location = new System.Drawing.Point(159, 44);
            this.e_box2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_box2.Name = "e_box2";
            this.e_box2.Size = new System.Drawing.Size(112, 26);
            this.e_box2.TabIndex = 25;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label22.Location = new System.Drawing.Point(15, 114);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(302, 20);
            this.label22.TabIndex = 24;
            this.label22.Text = "Выберите номер изменяемого  товара";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(344, 110);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(93, 28);
            this.comboBox1.TabIndex = 23;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(723, 144);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 46);
            this.button2.TabIndex = 20;
            this.button2.Text = "Изменить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(56)))), ((int)(((byte)(69)))));
            this.tabPage6.Controls.Add(this.label11);
            this.tabPage6.Controls.Add(this.comboBox2);
            this.tabPage6.Controls.Add(this.button3);
            this.tabPage6.Controls.Add(this.label24);
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(857, 216);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Удаление записей";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11.Location = new System.Drawing.Point(17, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(459, 20);
            this.label11.TabIndex = 39;
            this.label11.Text = "Выберите номер товара, запись о котором хотите удалить";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(130, 76);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(93, 28);
            this.comboBox2.TabIndex = 33;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.ForeColor = System.Drawing.SystemColors.Control;
            this.button3.Location = new System.Drawing.Point(723, 144);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 46);
            this.button3.TabIndex = 31;
            this.button3.Text = "Удалить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label24.Location = new System.Drawing.Point(17, 80);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(82, 20);
            this.label24.TabIndex = 26;
            this.label24.Text = "№ товара";
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
            this.refresh_but.TabIndex = 39;
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
            this.tabl.TabIndex = 38;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(370, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 26;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(555, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 27;
            // 
            // e_cb1
            // 
            this.e_cb1.FormattingEnabled = true;
            this.e_cb1.Location = new System.Drawing.Point(399, 41);
            this.e_cb1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.e_cb1.Name = "e_cb1";
            this.e_cb1.Size = new System.Drawing.Size(93, 28);
            this.e_cb1.TabIndex = 31;
            // 
            // a_cb1
            // 
            this.a_cb1.FormattingEnabled = true;
            this.a_cb1.Location = new System.Drawing.Point(399, 44);
            this.a_cb1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.a_cb1.Name = "a_cb1";
            this.a_cb1.Size = new System.Drawing.Size(93, 28);
            this.a_cb1.TabIndex = 24;
            // 
            // assortmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(865, 815);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.refresh_but);
            this.Controls.Add(this.tabl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "assortmentForm";
            this.Text = "assortmentForm";
            this.Load += new System.EventHandler(this.assortmentForm_Load);
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.e_box1.ResumeLayout(false);
            this.e_box1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox a_box3;
        private System.Windows.Forms.TextBox a_box1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button refresh_but;
        private System.Windows.Forms.DataGridView tabl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox e_box2;
        private System.Windows.Forms.TextBox e_box4;
        private System.Windows.Forms.TabPage e_box1;
        private System.Windows.Forms.ComboBox a_cb1;
        private System.Windows.Forms.ComboBox e_cb1;
    }
}