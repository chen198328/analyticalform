using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace AnalyticalPlatform
{
    public partial class DistinctRows : Form
    {
        public DistinctRows()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            button1.Enabled = false;
            this.UseWaitCursor = true;
            progressBar1.Visible = true;
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //bool result = (bool)e.Result;
            //string resultString = string.Empty;
            //if (result)
            //{
            //    resultString = "数据去重已完成";
            //}
            //else
            //{
            //    resultString = "数据去重失败";
            //}
            //MessageBox.Show(resultString);
            if (e.Error != null)
            {
                MessageBox.Show("数据去重失败：" + e.Error.Message.ToString());
            }
            else
            {
                MessageBox.Show("数据去重已完成!!!");
            }
            //激活Information窗体
            ActivateInformationForm();
            this.Close();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DistinctRow();
        }
        bool DistinctRow()
        {
            using (SqlConnection connection = new SqlConnection(GlobalParams.MasterConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "DistinctRows";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60 * 10 * 10;
                command.Parameters.AddWithValue("database", GlobalParams.Databasename);
                command.ExecuteNonQuery();
            }
            return true;
        }

        private void DistinctRows_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            UseWaitCursor = false;
            button1.UseWaitCursor = false;
        }
        private void ActivateInformationForm()
        {
            Form form = null;
            foreach (Form item in this.MdiParent.MdiChildren)
            {
                if (item.Name == "Information")
                {
                    form = item;

                }
            }
            if (form != null)
            {
                form.Activate();
            }
            else
            {
                Main main = (Main)this.MdiParent;
                main.ShowInformation();
            }
        }
    }
}
