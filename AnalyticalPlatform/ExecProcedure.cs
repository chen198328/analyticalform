using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace AnalyticalPlatform
{
    public partial class ExecProcedure : Form
    {
        public ExecProcedure()
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
            if (e.Error != null)
            {
                MessageBox.Show("CustomProcedure执行失败:" + e.Error.Message.ToString());
            }
            else
            {
                MessageBox.Show("CustomProcedure执行完成");
            }

            progressBar1.Visible = false;
            UseWaitCursor = false;
            button1.UseWaitCursor = false;
            button1.Enabled = true;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecCustomerProcedure();
        }

        private void ExecProcedure_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            UseWaitCursor = false;
            button1.UseWaitCursor = false;
        }
        bool ExecCustomerProcedure()
        {
            using (SqlConnection connection = new SqlConnection(GlobalParams.MasterConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "CustomProcedure";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60 * 10 * 10;
                command.Parameters.AddWithValue("database", GlobalParams.Databasename);
                command.ExecuteNonQuery();
            }
            return true;
        }
    }
}
