using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace AnalyticalPlatform
{
    public partial class UploadDataToCenter : Form
    {
        public UploadDataToCenter()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbxTitle.TextLength == 0)
            {
                MessageBox.Show("任务名不能为空");
                return;
            }
            //数据库保存
            UseWaitCursor = true;
            Tasks tasks = new Tasks() { Name = tbxTitle.Text.Trim(), Content = tbxContent.Text.Trim(), DateTime = DateTime.Now, UserId = GlobalParams.User.id };
            if (tasks.Save() > 0)
            {
                toolStripProgressBar1.Visible = true;
                btnSave.Enabled = false;

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.RunWorkerAsync(tasks);

            }
            else
            {
                MessageBox.Show("任务名保存失败，请重新保存");
            }




        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //保存成功且要求删除数据库中所有数据
            if (e.Error==null)
            {
                if (cbxDeleteAllData.Checked)
                {
                    ClownFish.DbHelper.ExecuteNonQuery("DeleteUserData", new { database = GlobalParams.Databasename }, ClownFish.CommandKind.StoreProcedure);
                }
                MessageBox.Show("数据保存到中央数据库成功，任务名为"+tbxTitle.Text.Trim());
            }
            else
            {
                Tasks tasks = Tasks.Find("Name", tbxTitle.Text.Trim());
                if (tasks != null)
                {
                    tasks.Delete();
                }
                MessageBox.Show("数据保存到中央数据库成功失败：" + e.Error.Message.ToString());
            }
            toolStripProgressBar1.Visible = false;
            btnSave.Enabled = true;
            tbxTitle.Text = string.Empty;
            tbxContent.Text = string.Empty;
            UseWaitCursor = false;
            ActivateInformationForm();
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
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //数据保存
            Tasks tasks = (Tasks)e.Argument;

            Result result = new Result();
            TransferData(tasks.id);


        }

        private void tbxTitle_Leave(object sender, EventArgs e)
        {
            string name = tbxTitle.Text.Trim();
            Tasks tasks = Tasks.Find("Name", name);
            if (tasks != null)
            {
                MessageBox.Show("系统中已经存在该任务名，请重命名");
                tbxTitle.Text = string.Empty;
                tbxTitle.Focus();

                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }

        }

        private void SaveTask_Load(object sender, EventArgs e)
        {
            tbxTitle.Focus();
            toolStripProgressBar1.Width = Parent.Width;
            toolStripProgressBar1.Visible = false;
        }
        /// <summary>
        /// 修改库中数据的任务名为当前任务名
        /// </summary>
        /// <param name="taskname"></param>
        private bool ChangeTaskName(string taskname)
        {
            var parameters = new UpdateTaskNameParameters
            {
                TaskName = taskname
            };
            try
            {
                using (SqlConnection connection = new SqlConnection(GlobalParams.ConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "UpdateTaskName";
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60 * 10;
                    command.Parameters.AddWithValue("TaskName", taskname);

                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool TransferData1()
        {
            using (SqlConnection connectionMainBase = new SqlConnection(ConfigurationManager.ConnectionStrings["AnalyticalPlatform"].ConnectionString))
            {

                IDataReader iRead;
                SqlConnection connection = new SqlConnection(GlobalParams.ConnectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                connection.Open();
                connectionMainBase.Open();




                SqlTransaction transaction = connectionMainBase.BeginTransaction();
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionMainBase, SqlBulkCopyOptions.Default, transaction);
                sqlBulkCopy.BatchSize = 10000;
                try
                {

                    #region Abstract
                    command.CommandText = "select * from [Abstract]";
                    sqlBulkCopy.DestinationTableName = "Abstract";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [Author]";
                    sqlBulkCopy.DestinationTableName = "Author";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [AuthorInstitute]";
                    sqlBulkCopy.DestinationTableName = "AuthorInstitute";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [Category]";
                    sqlBulkCopy.DestinationTableName = "Category";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [Citation] ";
                    sqlBulkCopy.DestinationTableName = "Citation";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [DocumentType] ";
                    sqlBulkCopy.DestinationTableName = "DocumentType";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [Institute] ";
                    sqlBulkCopy.DestinationTableName = "Institute";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [Keyword]";
                    sqlBulkCopy.DestinationTableName = "Keyword";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [Paper]";
                    sqlBulkCopy.DestinationTableName = "Paper";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [Reference]";
                    sqlBulkCopy.DestinationTableName = "Reference";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = "select * from [ReprintAuthor] ";
                    sqlBulkCopy.DestinationTableName = "ReprintAuthor";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Fund
                    command.CommandText = "select * from [Fund] ";
                    sqlBulkCopy.DestinationTableName = "Fund";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion
                    transaction.Commit();
                    return true;


                }
                catch (SqlException)
                {
                    transaction.Rollback();
                    return false;
                }
                catch (System.InvalidOperationException ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }

        }
        private bool TransferData(int taskid)
        {
            using (SqlConnection connection = new SqlConnection(GlobalParams.MasterConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "UploadDataToCenter";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60 * 10 * 10;
                command.Parameters.AddWithValue("taskid", taskid);
                command.Parameters.AddWithValue("database", GlobalParams.Databasename);
                command.ExecuteNonQuery();

                return true;
            }
        }
    }
    public class UpdateTaskNameParameters
    {
        public string TaskName { get; set; }
    }
}
