using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
namespace AnalyticalPlatform
{

    public partial class ImportDataFromFiles : Form
    {

        //保存文件名
        public List<string> FileNames = new List<string>();
        public ImportDataFromFiles()
        {
            InitializeComponent();
        }

        private void btnOpenFiles_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|Endnote(*.ciw)|*.ciw";
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                OpenFileDialog openFileDialog = (OpenFileDialog)sender;
                FileNames = openFileDialog.FileNames.ToList<string>();
                StringBuilder filenames = new StringBuilder();
                foreach (var filename in openFileDialog.FileNames)
                {
                    filenames.AppendFormat("{0};", filename);
                }
                rtbLogList.Clear();
                rtbLogList.AppendText(DateTime.Now.ToShortTimeString() + "总共打开:" + openFileDialog.FileNames.Length + "个文件\r\n");
                txtFileNames.Text = filenames.ToString().Trim(';');
                filenames.Clear();
                btnImport.Enabled = true;
            }
            else
            {
                FileNames.Clear();
                txtFileNames.Text = string.Empty;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            bool ImportResult = false;
            btnImport.Enabled = false;
            toolStripProgressBar1.Visible = true;
            UseWaitCursor = true;

            Task task = null;
            int paperCount = 0;
            StringBuilder ErrorMessage = new StringBuilder();
            if (radioButton1.Checked)
            {
                #region 小内存，速度相对较慢
                task = new Task(() =>
               {
                   using (SqlConnection connection = new SqlConnection(GlobalParams.ConnectionString))
                   {
                       try
                       {
                           connection.Open();

                           for (int i = 0; i < FileNames.Count; i++)
                           {
                               string p = FileNames[i];
                               using (StreamReader reader = new StreamReader(p, Encoding.Default))
                               {
                                   List<Paper> paperlist = new List<Paper>();
                                   this.Invoke(addmessage, "[" + DateTime.Now + "]" + "打开文件：" + p);
                                   try
                                   {
                                       paperlist = PaperManager.FillPaperFromStream(reader, p);
                                       //paperlist.ForEach(x =>
                                       //{
                                       //    if ((x.Keywords != null && x.Keywords.Length > 600) || (x.KeywordsPlus != null
                                       //        && x.KeywordsPlus.Length > 600)) { throw new Exception(x.AccessionNumber); }
                                       //});
                                       PaperManager.TransferData(paperlist, connection);
                                       paperCount += paperlist.Count;
                                   }
                                   catch (Exception ex)
                                   {
                                       ErrorMessage.Append("【" + DateTime.Now + "】" + ex.Message.ToString());
                                       //this.Invoke(addmessage, "【" + DateTime.Now + "】" + ex.Message.ToString());
                                       continue;
                                   }
                               }
                           }
                           ImportResult = paperCount != 0 ? true : false;
                           this.Invoke(addmessage, "[" + DateTime.Now + "]" + "已经上传文件数量:" + paperCount.ToString());

                       }
                       catch (SqlException ex)
                       {
                           ErrorMessage.Append("【" + DateTime.Now + "】" + "数据库连接失败" + ex.Message.ToString());
                       }

                   }


               });
                task.Start();
                #endregion
            }
            if (radioButton2.Checked)
            {
                #region 大内存，速度相对较快
                task = new Task(() =>
               {
                   List<Paper> paperlist = new List<Paper>();
                   for (int i = 0; i < FileNames.Count; i++)
                   {
                       string p = FileNames[i];
                       using (StreamReader reader = new StreamReader(p, Encoding.Default))
                       {
                           try
                           {
                               paperlist.AddRange(PaperManager.FillPaperFromStream(reader, p));

                               this.Invoke(addmessage, "[" + DateTime.Now + "]" + "打开文件：" + p);

                               if (paperlist.Count > 25000)
                               {
                                   PaperManager.TransferData(paperlist);
                                   paperCount += paperlist.Count;
                                   paperlist.Clear();
                               }
                           }
                           catch (Exception ex)
                           {
                               this.Invoke(addmessage, "【" + DateTime.Now + "】" + ex);
                               continue;

                           }
                       }
                   }
                   try
                   {
                       // PaperManager.TransferDataByPartion(paperlist);
                       PaperManager.TransferData(paperlist);
                       paperCount += paperlist.Count;
                       this.Invoke(addmessage, "[" + DateTime.Now + "]" + "已经上传文件数量:" + paperCount);
                       ImportResult = paperCount > 0 ? true : false;
                   }
                   catch (Exception ex)
                   {
                       //this.Invoke(addmessage, "【错误提示】" + ex.Message.ToString() + "\r\n请使用【上传1】查看具体出错文件");
                       ErrorMessage.Append("【" + DateTime.Now + "】" + ex.Message.ToString());
                   }


               });
                task.Start();
                #endregion

                #region
                //Thread thread = new Thread(new ThreadStart(() =>
                //{
                //    ConvertData convertData = new ConvertData(FileNames, 100);
                //    convertData.OnControlData += convertData_OnControlData;
                //    convertData.OnComplete += convertData_OnComplete;
                //    convertData.Start();


                //}));
                //thread.Start();

                //Task task = new Task(() =>
                //{
                //    ImportFiles im = new ImportFiles(FileNames);

                //    im.OnImportFile += im_OnImportFile;
                //    im.NoticeOnImportFile += im_NoticeOnImportFile;
                //    im.OnComplete += im_OnComplete;
                //    im.OnStoreToDatabase += im_OnStoreToDatabase;
                //    im.NoticeOnStoreToDatabase += im_NoticeOnStoreToDatabase;
                //    im.ImportAllData();
                //});
                //task.Start();
                #endregion
            }
            task.ContinueWith((Task) =>
            {
                //日志
                this.Invoke(addmessage, ErrorMessage.ToString());

                Func<bool> stateback = () =>
                {
                    StreamWriter logwriter = new StreamWriter("Log/Log.txt", true);
                    logwriter.Write(rtbLogList.Text);
                    logwriter.Flush();
                    logwriter.Close();

                    toolStripProgressBar1.Visible = false;
                    UseWaitCursor = false;
                    if (ImportResult)
                    {
                        DialogResult result = MessageBox.Show("数据导入完成，详细信息请查看信息列表。如果需要对数据去重，请按确定；否则，请按取消", "导入完成", MessageBoxButtons.YesNoCancel);
                        if (result == DialogResult.Yes)
                        {
                            DistinctRows distinctRow = new DistinctRows();
                            distinctRow.MdiParent = this.MdiParent;
                            distinctRow.Show();
                        }
                        else
                        {
                            ActivateInformationForm();
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有导入数据进入系统,请查看错误提示！！！");
                        ActivateInformationForm();
                    }


                    return true;
                };

                this.Invoke(stateback);

            });
        }

        private void AddMessage(string message)
        {
            rtbLogList.AppendText(message + "\r\n");
        }
        Func<string, bool> addmessage;
        private void Form1_Load(object sender, EventArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
            addmessage = (message) =>
            {
                AddMessage(message);
                return true;
            };
        }
        bool ImportFilesCallBack()
        {
            using (SqlConnection connection = new SqlConnection(GlobalParams.MasterConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "ImportFilesCallBack";
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 60 * 10;
                command.Parameters.AddWithValue("database", GlobalParams.Databasename);
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
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
