using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using ClownFish;
namespace AnalyticalPlatform
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {


            Login();
            ShowInformation();



        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void Login()
        {
            Login login = new Login();
            login.ShowDialog();
            this.Show();
            if (GlobalParams.User != null)
            {
                Text = string.Format("当前账号：{0}——当前数据库：{1}", GlobalParams.User.Name, GlobalParams.Databasename);
                try
                {
                    ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["AnalyticalPlatform"];
                    DbContext.RegisterDbConnectionInfo("sqlserver", setting.ProviderName, "@",GlobalParams.MasterConnectionString);

                }
                catch (Exception ex)
                {
                    string ext = ex.Message.ToString();
                }
            }
        }

        private void 登录ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidateUser())
            {
                DownloadDataFromCenter task = new DownloadDataFromCenter();
                task.MdiParent = this;
                task.Show();
            }
            else
            {
                ShowNotOnline();
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidateUser())
            {
                UploadDataToCenter save = new UploadDataToCenter();
                save.MdiParent = this;
                save.Show();
            }
            else
            {
                ShowNotOnline();
            }
        }

        private void 当前数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidateUser())
            {
                MessageBox.Show("当前数据库：" + GlobalParams.Databasename);
            }
            else
            {
                ShowNotOnline();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalParams.User != null && GlobalParams.User.DataBase != null)
            {
                GlobalParams.User.DataBase.Online = 0;
                GlobalParams.User.DataBase.Save();
                GlobalParams.User.DataBaseId = 0;
                GlobalParams.User.Save();

                GlobalParams.ConnectionString = string.Empty;
                GlobalParams.User = new User();
                GlobalParams.Databasename = string.Empty;

                Text = "用户没有登录";

                Form[] children = MdiChildren;
                for (int i = 0; i < children.Length; i++)
                {
                    children[i].Close();
                }
            }
        }
        /// <summary>
        /// 验证用户是否在线
        /// </summary>
        /// <returns></returns>
        private bool ValidateUser()
        {
            if (GlobalParams.User != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ShowNotOnline()
        {
            MessageBox.Show("用户没有登录，请重新登录");
        }
        public void ShowInformation()
        {
            if (ValidateUser())
            {
                Information information = new Information();
                information.MdiParent = this;
                information.Show();
            }
            else
            {
                ShowNotOnline();
            }
        }
        private void 当前数据库ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowInformation();
        }

        private void 导入数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportDataFromFiles importdata = new ImportDataFromFiles();
            importdata.MdiParent = this;
            importdata.Show();
        }

        private void 数据去重ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ValidateUser())
            {
                DistinctRows distinctRow = new DistinctRows();
                distinctRow.MdiParent = this;
                distinctRow.Show();
            }
            else
            {
                ShowNotOnline();
            }


        }

        private void 执行自定存储过程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ValidateUser())
            {
                ExecProcedure execProcedure = new ExecProcedure();
                execProcedure.MdiParent = this;
                execProcedure.Show();
            }
            else
            {
                ShowNotOnline();
            }

        }
    }
}
