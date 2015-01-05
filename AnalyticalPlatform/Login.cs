using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewLife;
using XCode;
using System.Configuration;
using System.Data.SqlClient;
namespace AnalyticalPlatform
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public User user { set; get; }
        private void tbxPassword_Leave(object sender, EventArgs e)
        {
            if (tbxUsername.TextLength > 0 && tbxPassword.TextLength > 0)
            {
                //账号密码验证
                try
                {
                    user = User.Find(new string[] { "Name", "Password" }, new string[] { tbxUsername.Text.Trim(), tbxPassword.Text.Trim() });
                }
                catch (SqlException)
                {
                    MessageBox.Show("数据库连接错误，请确认数据库系统是否运行，并确认数据库配置正确");
                    return;
                }
                if (user != null)
                {
                    //获取数据库池中的数据
                    if (user.DataBase == null)
                    {
                        DataBase database = DataBase.Find("Online", 0);
                        if (database != null)
                        {
                            user.DataBaseId = database.id;
                            user.Save();

                            database.Online = 1;
                            database.Save();
                        }
                    }
                    if (user.DataBase != null)
                    {


                        lblDatabase.Text = "当前系统数据库:" + user.DataBase.Name;
                        btnLogin.Enabled = true;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("数据库池中没有可用数据库");
                        btnLogin.Enabled = false;
                        return;
                    }


                }
                else
                {
                    MessageBox.Show("账号密码错误");
                    return;
                }

            }
        }

        private void tbxUsername_TextChanged(object sender, EventArgs e)
        {
            tbxPassword.Text = string.Empty;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (user.DataBase != null)
            {
                //生成连接
                GlobalParams.ConnectionString = string.Format(ConfigurationManager.AppSettings["connectionString"], user.DataBase.Name);
                GlobalParams.MasterConnectionString = string.Format(ConfigurationManager.AppSettings["connectionString"], "AnalyticalPlatForm");
                GlobalParams.User = user;
                GlobalParams.Databasename = user.DataBaseName;
                Close();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
