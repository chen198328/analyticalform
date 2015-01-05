namespace AnalyticalPlatform
{
    partial class AdvancedSearchTask
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
            this.tbxTaskName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxStartDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxEndDate = new System.Windows.Forms.TextBox();
            this.cbxUserList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxTaskName
            // 
            this.tbxTaskName.Location = new System.Drawing.Point(127, 25);
            this.tbxTaskName.Name = "tbxTaskName";
            this.tbxTaskName.Size = new System.Drawing.Size(229, 21);
            this.tbxTaskName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "任务名/说明";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "起始时间";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tbxStartDate
            // 
            this.tbxStartDate.Location = new System.Drawing.Point(97, 67);
            this.tbxStartDate.Name = "tbxStartDate";
            this.tbxStartDate.Size = new System.Drawing.Size(100, 21);
            this.tbxStartDate.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "结束时间";
            // 
            // tbxEndDate
            // 
            this.tbxEndDate.Location = new System.Drawing.Point(270, 67);
            this.tbxEndDate.Name = "tbxEndDate";
            this.tbxEndDate.Size = new System.Drawing.Size(86, 21);
            this.tbxEndDate.TabIndex = 4;
            this.tbxEndDate.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // cbxUserList
            // 
            this.cbxUserList.FormattingEnabled = true;
            this.cbxUserList.Location = new System.Drawing.Point(76, 106);
            this.cbxUserList.Name = "cbxUserList";
            this.cbxUserList.Size = new System.Drawing.Size(121, 20);
            this.cbxUserList.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "用户";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "检索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdvancedSearchTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 205);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxUserList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxEndDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxStartDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxTaskName);
            this.Name = "AdvancedSearchTask";
            this.Text = "任务高级搜索";
            this.Load += new System.EventHandler(this.AdvancedSearchTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxTaskName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxStartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxEndDate;
        private System.Windows.Forms.ComboBox cbxUserList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}