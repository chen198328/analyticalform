using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
namespace CheckData
{
    public partial class Main : Form
    {
        //保存文件名
        public List<string> FileNames = new List<string>();
        public Main()
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
            Func<string, bool, bool> addmessage = (message, tocaret) =>
            {
                rtbLogList.AppendText(message + "\r\n");
                if (tocaret)
                {
                    rtbLogList.ScrollToCaret();
                }
                return true;
            };
            Func<int, bool> updateProgress = (value) =>
            {
                toolStripProgressBar1.Value = value;
                return true;
            };
            Func<bool, bool> changeStatue = (status) =>
            {
                if (status)
                {
                    toolStripProgressBar1.Visible = true;
                    btnImport.Enabled = false;
                    UseWaitCursor = true;

                }
                else
                {
                    toolStripProgressBar1.Visible = false;
                    btnImport.Enabled = true;
                    UseWaitCursor = false;
                }
                return true;
            };
            Func<bool> alertMessage = () =>
            {

                MessageBox.Show("数据检查完成");
                return true;
            };
            int index = 0;
            Dictionary<string, int> totalResult = new Dictionary<string, int>();
            Dictionary<string, int> documentResult = new Dictionary<string, int>();
            NameValueCollection distinctfile = new NameValueCollection();
            StringBuilder ErrorMessage = new StringBuilder();
            StringBuilder NotCountMessage = new StringBuilder();

            Task task = new Task(() =>
            {
                this.BeginInvoke(changeStatue, true);

                foreach (var filename in FileNames)
                {
                    distinctfile.Add(GetHashCode(filename).ToString(), filename);
                    Dictionary<string, int> result = new Dictionary<string, int>();
                    try
                    {
                        int value = index * 100 / FileNames.Count;
                        this.BeginInvoke(updateProgress, value);
                        result = CheckFile(filename);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Append(filename);
                        ErrorMessage.Append(ex.Message.ToString());
                        ErrorMessage.AppendLine();
                        continue;
                    }
                    documentResult = Combine(documentResult, CheckDocumentType(filename));
                    totalResult = Combine(totalResult, result);
                    index++;
                    int papercount = result.ContainsKey("ER") ? result["ER"] : 0;
                    string message = string.Format("[{0}]PaperCount:【{1}】--{2}", DateTime.Now, papercount, filename);
                    if (papercount != 500)
                    {
                        NotCountMessage.Append("【" + papercount.ToString().PadLeft(3, ' ') + "】-" + filename);
                        NotCountMessage.AppendLine();
                    }
                    this.BeginInvoke(addmessage, message, true);
                }
            });

            task.Start();
            task.ContinueWith((Task) =>
            {

                StringBuilder statisticResult = new StringBuilder();
                statisticResult.Append("---------------------字段统计数据----------------");
                statisticResult.AppendLine();
                foreach (var item in totalResult)
                {
                    statisticResult.AppendFormat("{0}:{1}", item.Key, item.Value);
                    statisticResult.AppendLine();
                }
                if (documentResult.Keys.Count > 0)
                {
                    statisticResult.Append("---------------DocumentType统计数据----------");
                    statisticResult.AppendLine();
                    foreach (var item in documentResult)
                    {
                        statisticResult.AppendFormat("{0}:{1}", item.Key, item.Value);
                        statisticResult.AppendLine();
                    }
                }
                if (ErrorMessage.Length > 0)
                {
                    statisticResult.Append("-----------------文件格式错误的文件列表------------");
                    statisticResult.AppendLine();

                    statisticResult.Append(ErrorMessage.ToString());
                    statisticResult.AppendLine();
                }
                if (NotCountMessage.Length > 0)
                {
                    statisticResult.Append("--------------记录数不是500的文件列表-----------");
                    statisticResult.AppendLine();
                    statisticResult.Append(NotCountMessage.ToString());
                }
                StringBuilder dupliate = new StringBuilder();
                if (distinctfile.Count > 0)
                {
                    foreach (var key in distinctfile.AllKeys)
                    {
                        if (distinctfile[key].Contains(","))
                        {
                            dupliate.AppendLine(distinctfile[key]);
                        }
                    }
                }
                if (dupliate.Length > 0)
                {
                    statisticResult.Append("--------------一内容相同的文件-----------");
                    statisticResult.AppendLine();
                    statisticResult.Append(dupliate.ToString());
                }
                string tips = "=====================【统计结果】=================";
                this.Invoke(addmessage, tips, true);
                this.BeginInvoke(addmessage, statisticResult.ToString(), false);
                this.BeginInvoke(changeStatue, false);
                this.BeginInvoke(alertMessage);

            });

        }
        private string GetStartChars(string line)
        {
            if (line.Length > 3 && line.Substring(2, 1) != " ")
            {
                throw new Exception("【格式错误】" + line);
            }
            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line) || line.Length < 2)
            {
                return line;
            }
            else
            {
                return line.Substring(0, 2);
            }
        }
        private Dictionary<string, int> Combine(Dictionary<string, int> totalresult, Dictionary<string, int> newresult)
        {
            foreach (var item in newresult)
            {
                if (totalresult.ContainsKey(item.Key))
                {
                    totalresult[item.Key] += item.Value;
                }
                else
                {
                    totalresult[item.Key] = item.Value;
                }
            }
            return totalresult;
        }
        private Dictionary<string, int> CheckFile(string filename)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            string line = string.Empty;
            string prefix = string.Empty;
            using (StreamReader reader = new StreamReader(filename))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    prefix = GetStartChars(line);
                    if (!string.IsNullOrWhiteSpace(prefix))
                    {
                        if (result.ContainsKey(prefix))
                        {
                            result[prefix] += 1;
                        }
                        else
                        {
                            result[prefix] = 1;
                        }
                    }
                }
            }
            return result;
        }
        private Dictionary<string, int> CheckDocumentType(string filename)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            string line = string.Empty;
            bool mark = false;
            string prefix = string.Empty;
            StringBuilder documentTypes = new StringBuilder();
            using (StreamReader reader = new StreamReader(filename))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    prefix = GetStartChars(line);
                    if (prefix == "DT" || (prefix == "" && mark))
                    {
                        documentTypes.AppendFormat("{0};", line.Substring(2).Trim());
                        mark = true;

                    }
                    else
                    {
                        mark = false;
                    }
                }
            }
            string[] types = documentTypes.ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in types)
            {

                if (result.ContainsKey(item.Trim()))
                {
                    result[item.Trim()] += 1;
                }
                else
                {
                    result[item.Trim()] = 1;
                }
            }


            return result;
        }
        private int GetHashCode(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string content = reader.ReadToEnd();
                return content.GetHashCode();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripProgressBar1.AutoSize = false;
            toolStripProgressBar1.Visible = false;
            toolStripProgressBar1.Width = statusStrip1.Width - toolStripProgressBar1.Bounds.Left - 20;
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            toolStripProgressBar1.Width = statusStrip1.Width - toolStripProgressBar1.Bounds.Left - 20;

        }
    }
}
