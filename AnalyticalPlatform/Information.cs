using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ClownFish;
namespace AnalyticalPlatform
{
    public partial class Information : Form
    {
        public Information()
        {
            InitializeComponent();
        }

        private void btnDeleteAllData_Click(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            btnDeleteAllData.Enabled = false;
            worker.RunWorkerAsync();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Result result = (Result)e.Result;
            //MessageBox.Show(result.Content);
            if (e.Error != null)
            {
                MessageBox.Show("清除数据失败：" + e.Error.Message.ToString());
            }
            else
            {
                MessageBox.Show("成功清除当前全部数据");
            }
            toolStripProgressBar1.Visible = false;
            btnDeleteAllData.Enabled = true;
            GetInformation();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
           // Result result = new Result();

            //using (SqlConnection connection = new SqlConnection(GlobalParams.ConnectionString))
            //{
            //    try
            //    {
            //        connection.Open();
            //        SqlCommand command = new SqlCommand();
            //        command.Connection = connection;
            //        command.CommandText = "DeleteUserData";
            //        command.CommandType = CommandType.StoredProcedure;
            //        command.CommandTimeout = 60 * 10 * 10;
            //        command.ExecuteNonQuery();

            //        result.Success = true;
            //        result.Content = "成功清除当前全部数据";
            //    }
            //    catch (System.Data.SqlClient.SqlException ex)
            //    {
            //        result.Success = false;
            //        result.Content = "清除数据失败：" + ex.Message.ToString();
            //    }
            //}


                e.Result = DbHelper.FillDataTable("DeleteUserData", new { database = GlobalParams.Databasename }, CommandKind.StoreProcedure);

        }

        private void Information_Load(object sender, EventArgs e)
        {

            GetInformation();
        }
        void GetInformation()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_GetInformation;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted_GetInformation;
            worker.RunWorkerAsync();

            UseWaitCursor = true;
            toolStripProgressBar1.Visible = true;

        }
        void worker_GetInformation(object sender, DoWorkEventArgs e)
        {
            try
            {
                e.Result = DbHelper.FillDataTable("GetInformation", new { database = GlobalParams.Databasename }, CommandKind.StoreProcedure);
               
            }
            catch { }
        }
        void worker_RunWorkerCompleted_GetInformation(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                dataGridView1.DataSource = (DataTable)e.Result;
            }
            toolStripProgressBar1.Visible = false;
            UseWaitCursor = false;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetInformation();
        }

        private void Information_Activated(object sender, EventArgs e)
        {
            GetInformation();
        }
    }
}
