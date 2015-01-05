namespace AnalyticalPlatform
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当前数据库ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.导入数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据去重ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登录ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.当前数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.执行自定存储过程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.任务ToolStripMenuItem,
            this.登录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(846, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.当前数据库ToolStripMenuItem1,
            this.导入数据ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 当前数据库ToolStripMenuItem1
            // 
            this.当前数据库ToolStripMenuItem1.Name = "当前数据库ToolStripMenuItem1";
            this.当前数据库ToolStripMenuItem1.Size = new System.Drawing.Size(208, 22);
            this.当前数据库ToolStripMenuItem1.Text = "当前数据库概况";
            this.当前数据库ToolStripMenuItem1.Click += new System.EventHandler(this.当前数据库ToolStripMenuItem1_Click);
            // 
            // 导入数据ToolStripMenuItem
            // 
            this.导入数据ToolStripMenuItem.Name = "导入数据ToolStripMenuItem";
            this.导入数据ToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.导入数据ToolStripMenuItem.Text = "从文件导入到当前数据库";
            this.导入数据ToolStripMenuItem.Click += new System.EventHandler(this.导入数据ToolStripMenuItem_Click);
            // 
            // 任务ToolStripMenuItem
            // 
            this.任务ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.数据去重ToolStripMenuItem,
            this.执行自定存储过程ToolStripMenuItem});
            this.任务ToolStripMenuItem.Name = "任务ToolStripMenuItem";
            this.任务ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.任务ToolStripMenuItem.Text = "任务";
            // 
            // 导入ToolStripMenuItem
            // 
            this.导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            this.导入ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.导入ToolStripMenuItem.Text = "从中央数据库下载";
            this.导入ToolStripMenuItem.Click += new System.EventHandler(this.导入ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.保存ToolStripMenuItem.Text = "保存到中央数据库";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // 数据去重ToolStripMenuItem
            // 
            this.数据去重ToolStripMenuItem.Name = "数据去重ToolStripMenuItem";
            this.数据去重ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.数据去重ToolStripMenuItem.Text = "数据去重";
            this.数据去重ToolStripMenuItem.Click += new System.EventHandler(this.数据去重ToolStripMenuItem_Click);
            // 
            // 登录ToolStripMenuItem
            // 
            this.登录ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登录ToolStripMenuItem1,
            this.退出ToolStripMenuItem,
            this.当前数据库ToolStripMenuItem});
            this.登录ToolStripMenuItem.Name = "登录ToolStripMenuItem";
            this.登录ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.登录ToolStripMenuItem.Text = "用户";
            // 
            // 登录ToolStripMenuItem1
            // 
            this.登录ToolStripMenuItem1.Name = "登录ToolStripMenuItem1";
            this.登录ToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.登录ToolStripMenuItem1.Text = "登录";
            this.登录ToolStripMenuItem1.Click += new System.EventHandler(this.登录ToolStripMenuItem1_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 当前数据库ToolStripMenuItem
            // 
            this.当前数据库ToolStripMenuItem.Name = "当前数据库ToolStripMenuItem";
            this.当前数据库ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.当前数据库ToolStripMenuItem.Text = "当前数据库";
            this.当前数据库ToolStripMenuItem.Click += new System.EventHandler(this.当前数据库ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 502);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(846, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // 执行自定存储过程ToolStripMenuItem
            // 
            this.执行自定存储过程ToolStripMenuItem.Name = "执行自定存储过程ToolStripMenuItem";
            this.执行自定存储过程ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.执行自定存储过程ToolStripMenuItem.Text = "执行自定存储过程";
            this.执行自定存储过程ToolStripMenuItem.Click += new System.EventHandler(this.执行自定存储过程ToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 524);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 任务ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登录ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当前数据库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 当前数据库ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 数据去重ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 执行自定存储过程ToolStripMenuItem;

    }
}