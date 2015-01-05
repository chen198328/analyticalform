using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XCode;
namespace AnalyticalPlatform
{
    public partial class AdvancedSearchTask : Form
    {
        public DownloadDataFromCenter importTask;
        /// <summary>
        /// 构建sql，传递给上级窗口
        /// </summary>
        public string Sql;
        public AdvancedSearchTask()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void AdvancedSearchTask_Load(object sender, EventArgs e)
        {
            //作者名
            EntityList<User> userlist = User.FindAll();
            if (userlist != null)
            {
                userlist.ForEach(u =>
                {
                    cbxUserList.Items.Add(u.Name);
                });
            }
            //日期
            tbxEndDate.Text = DateTime.Now.ToShortDateString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql ="select [Name] from [AnalyticalPlatform].[dbo].[Tasks] where";
            if (tbxTaskName.Text.Trim().Length > 0)
                sql += string.Format(" Name like '{0}' and ", tbxTaskName.Text.Trim());

            if (tbxStartDate.Text.Trim().Length > 0)
            {
                sql += string.Format(" [DateTime]>'{0}' and ", tbxStartDate.Text.Trim());
            }

            if (tbxEndDate.Text.Trim().Length > 0)
            {
                sql += string.Format("[DateTime]-1<'{0}' and ", tbxEndDate.Text.Trim());
            }

            if (cbxUserList.SelectedText != null)
            {
                User user = User.Find("Name", cbxUserList.SelectedText);
                if (user != null)
                {
                    sql += string.Format("[UserId]={0} and", user.id);
                }
            }
            sql += " 1=1";

            if (importTask != null)
            {
                importTask.AdvancedSearchTask(sql,tbxTaskName.Text.Trim());
            }
           
            this.Close();
        }
    }
}
