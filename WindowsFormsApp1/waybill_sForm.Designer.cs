﻿namespace WindowsFormsApp1
{
    partial class waybill_sForm
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
            this.last_but = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tabl)).BeginInit();
            this.SuspendLayout();
            // 
            // refresh_but
            // 
            this.refresh_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refresh_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh_but.ForeColor = System.Drawing.SystemColors.Control;
            this.refresh_but.Location = new System.Drawing.Point(740, 752);
            this.refresh_but.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.refresh_but.Name = "refresh_but";
            this.refresh_but.Size = new System.Drawing.Size(112, 36);
            this.refresh_but.TabIndex = 35;
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
            this.tabl.Size = new System.Drawing.Size(838, 633);
            this.tabl.TabIndex = 34;
            // 
            // last_but
            // 
            this.last_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.last_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.last_but.ForeColor = System.Drawing.SystemColors.Control;
            this.last_but.Location = new System.Drawing.Point(14, 752);
            this.last_but.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.last_but.Name = "last_but";
            this.last_but.Size = new System.Drawing.Size(244, 36);
            this.last_but.TabIndex = 37;
            this.last_but.Text = "Показать выполненые заявки";
            this.last_but.UseVisualStyleBackColor = true;
            this.last_but.Click += new System.EventHandler(this.last_but_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(14, 690);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 36);
            this.button1.TabIndex = 44;
            this.button1.Text = "Обработать накладные";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(300, 694);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 25);
            this.label2.TabIndex = 43;
            this.label2.Text = "Таблица не содержит записей";
            this.label2.Visible = false;
            // 
            // waybill_sForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(865, 815);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.last_but);
            this.Controls.Add(this.refresh_but);
            this.Controls.Add(this.tabl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "waybill_sForm";
            this.Text = "waybill_sForm";
            this.Load += new System.EventHandler(this.waybill_sForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button refresh_but;
        private System.Windows.Forms.DataGridView tabl;
        private System.Windows.Forms.Button last_but;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}