using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCode;
using System.Data.SqlClient;
using System.Configuration;
using ClownFish;
namespace AnalyticalPlatform
{
    public partial class DownloadDataFromCenter : Form
    {
        public DownloadDataFromCenter()
        {
            InitializeComponent();
        }

        private void Task_Load(object sender, EventArgs e)
        {
            EntityList<Tasks> taskslist = Tasks.FindAll();
            foreach (Tasks item in taskslist)
            {
                lbxTasklist.Items.Add(item.Name);
            }
            //lbxTasklist.DataSource = taskslist;

            GetTasksFromDomain();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lbxTasklist.SelectedItem != null)
            {
                object obj = lbxTasklist.SelectedItem;
                if (!lbxAddTasklist.Items.Contains(obj) && !lbxDomainTaskList.Items.Contains(obj))
                {
                    lbxAddTasklist.Items.Add(obj);
                }
                //lbxTasklist.Items.Remove(obj);
            }
            ValidatingForm();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbxAddTasklist.SelectedItem != null)
            {
                object obj = lbxAddTasklist.SelectedItem;
                //lbxTasklist.Items.Add(obj);
                lbxAddTasklist.Items.Remove(obj);
            }
            ValidatingForm();
        }
        public void ValidatingForm()
        {
            if (lbxAddTasklist.Items.Count > 0)
            {
                btnImport.Enabled = true;
            }
            else
            {
                btnImport.Enabled = false;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            btnImport.Enabled = false;
            toolStripProgressBar1.Visible = true;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lbxAddTasklist.Items.Clear();
            if (e.Error == null)
            {
                MessageBox.Show("从中央数据库下载任务数据到"+GlobalParams.Databasename+"已经完成!!!");
            }
            else
            {
                MessageBox.Show("从中央数据库下载任务数据失败:"+e.Error.Message.ToString());
            }
            btnImport.Enabled = true;
            toolStripProgressBar1.Visible = false;
            GetTasksFromDomain();
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
            Result result = new Result();
            for (int i = 0; i < lbxAddTasklist.Items.Count; i++)
            {
                if (ImportDataFromCenter(lbxAddTasklist.Items[i].ToString()))
                {
                    result.Content += lbxAddTasklist.Items[i].ToString() + "\r\n";
                }
            }
            
            e.Result = result;
        }
        /// <summary>
        /// 更新当前库中的任务列表
        /// </summary>
        void GetTasksFromDomain()
        {
            lbxDomainTaskList.Items.Clear();
            //从paper中查找当前存在的任务，并显示在当前
            string sql = "with TasksId as";
            sql += "(select distinct TaskId from [" + GlobalParams.Databasename + "].[dbo].[Paper])";
            sql += "select Name from TasksId,[Tasks] T where TasksId.TaskId=T.Id";
            DataTable table = DbHelper.FillDataTable(sql, null, CommandKind.SqlTextNoParams);
            foreach (DataRow item in table.Rows)
            {
                lbxDomainTaskList.Items.Add(item[0].ToString());
            }
        }
        bool ImportDatafromFile(string taskname)
        {
            using (SqlConnection connectionMainBase = new SqlConnection(GlobalParams.ConnectionString))
            {

                IDataReader iRead;
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AnalyticalPlatform"].ConnectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                connection.Open();
                connectionMainBase.Open();




                SqlTransaction transaction = connectionMainBase.BeginTransaction();
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionMainBase, SqlBulkCopyOptions.Default, transaction);
                sqlBulkCopy.BatchSize = 10000;
                sqlBulkCopy.BulkCopyTimeout = 60 * 10;
                try
                {

                    #region Abstract
                    command.CommandText = string.Format("select * from [Abstract] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Abstract";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Author
                    command.CommandText = string.Format("select * from [Author] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Author";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region AuthorInstitute
                    command.CommandText = string.Format("select * from [AuthorInstitute] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "AuthorInstitute";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Category
                    command.CommandText = string.Format("select * from [Category] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Category";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Citation
                    command.CommandText = string.Format("select * from [Citation] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Citation";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region DocumentType
                    command.CommandText = string.Format("select * from [DocumentType] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "DocumentType";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Institute
                    command.CommandText = string.Format("select * from [Institute] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Institute";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Keyword
                    command.CommandText = string.Format("select * from [Keyword] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Keyword";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Paper
                    command.CommandText = string.Format("select * from [Paper] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Paper";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region Reference
                    command.CommandText = string.Format("select * from [Reference] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "Reference";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion

                    #region ReprintAuthor
                    command.CommandText = string.Format("select * from [ReprintAuthor] where Task='{0}'", taskname);
                    sqlBulkCopy.DestinationTableName = "ReprintAuthor";
                    iRead = command.ExecuteReader();
                    sqlBulkCopy.WriteToServer(iRead);
                    iRead.Close();
                    #endregion
                    #region Fund
                    command.CommandText = string.Format("select * from [Fund] where Task='{0}'", taskname);
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
                catch (System.InvalidOperationException)
                {
                    transaction.Rollback();
                    return false;
                }
                finally
                {
                    connection.Close();
                }

            }

        }
        bool ImportDataFromCenter(string taskname)
        {
                Tasks tasks = Tasks.Find("Name", taskname);
                if (tasks == null)
                    return false;
                using (SqlConnection connection = new SqlConnection(GlobalParams.MasterConnectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "DownloadDataFromCenter";
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 60 * 10;
                    command.Parameters.AddWithValue("taskid", tasks.id);
                    command.Parameters.AddWithValue("database", GlobalParams.Databasename);
                    command.ExecuteNonQuery();
                }
                return true;
        }

        private void lbxTasklist_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtbTaskInformation.Clear();

            string item = lbxTasklist.SelectedItem.ToString();


            Tasks tasks = Tasks.Find("Name", item);
            if (tasks != null)
            {

                rtbTaskInformation.AppendText("任务名：" + tasks.Name + "\r\n");
                rtbTaskInformation.AppendText("说明:" + tasks.Content + "\r\n");
                rtbTaskInformation.AppendText("时间:" + tasks.DateTime.ToLongDateString() + "\r\n");
                rtbTaskInformation.AppendText("负责人：" + tasks.UserName);
            }
            else
            {
                rtbTaskInformation.AppendText("该任务没有信息");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            string code = e.KeyCode.ToString();
            if (code.ToLower() == "return")
            {
                SearchTask(tbxTaskName.Text.Trim());
            }
        }
        void SearchTask(string keyword)
        {
            lbxTasklist.Items.Clear();
            //从paper中查找当前存在的任务，并显示在当前

            string sql = string.Format("select Name from [AnalyticalPlatform].[dbo].[Tasks] where Name='{0}'", keyword);
            DataTable table = DbHelper.FillDataTable(sql, null, CommandKind.SqlTextNoParams);
            foreach (DataRow item in table.Rows)
            {
                lbxTasklist.Items.Add(item[0].ToString());
            }
            sql = string.Format("select Name from [AnalyticalPlatform].[dbo].[Tasks] where Name!='{0}' And (Name like '%{0}%' or [Content] like '%{0}%')", keyword);
            table = DbHelper.FillDataTable(sql, null, CommandKind.SqlTextNoParams);
            foreach (DataRow item in table.Rows)
            {
                lbxTasklist.Items.Add(item[0].ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdvancedSearchTask advance = new AdvancedSearchTask();
            advance.MdiParent = this.MdiParent;
            advance.importTask = this;
            advance.Show();





        }
        public void AdvancedSearchTask(string sql, string name)
        {
            lbxTasklist.Items.Clear();
            tbxTaskName.Text = name;

            DataTable table = DbHelper.FillDataTable(sql, null, CommandKind.SqlTextNoParams);
            foreach (DataRow item in table.Rows)
            {
                lbxTasklist.Items.Add(item[0].ToString());
            }
        }
    }

}
