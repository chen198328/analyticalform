using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace AnalyticalPlatform
{
    public class ImportFiles
    {
        public Queue<string> Filenames { set; get; }
        /// <summary>
        /// 一次保存进入数据库的论文数量
        /// </summary>
        public int PaperCount { set; get; }
        //处理单个文件
        //完成单个文件通知
        //完成所有文件通知
        public delegate List<Paper> ImportFileHandler(StreamReader streamreader);
        public delegate void NoticeForImportFileHandler(string filename, bool success);
        public delegate bool CompleteHandler();
        public delegate bool StoreToDatabaseHandler(List<Paper> paperlist);
        public delegate void NoticeForStoreToDatabaseHandler(List<Paper> paperlist, bool success);
        public event ImportFileHandler OnImportFile;
        public event NoticeForImportFileHandler NoticeOnImportFile;
        public event CompleteHandler OnComplete;
        public event StoreToDatabaseHandler OnStoreToDatabase;
        public event NoticeForStoreToDatabaseHandler NoticeOnStoreToDatabase;

        #region
        public ImportFiles(List<string> filenames)
        {
            Filenames = new Queue<string>(filenames.Count);
            filenames.ForEach(p => { Filenames.Enqueue(p); });
            PaperCount = 40000;


        }
        public void ImportAllData()
        {
            int TaskCount = 0;
            if (Filenames.Count > 1000)
            {
                TaskCount = 4;
            }
            else if (Filenames.Count > 100)
            {
                TaskCount = 3;
            }
            else
            {
                TaskCount = 2;
            }
            Task[] importTasks = new Task[TaskCount];

            for (int i = 0; i < TaskCount; i++)
            {
                importTasks[i] = Task.Factory.StartNew(() =>
                {
                    FillPapersFromFile();
                });
            }



            Task.WaitAll(importTasks);

            StoreData();
            if (OnComplete != null)
            {
                OnComplete();
            }

        }
        static object obj = new object();
        private bool FillPapersFromFile()
        {
            while (Filenames.Count > 0)
            {
                string filename = string.Empty;
                lock (obj)
                {
                    filename = Filenames.Dequeue();
                }
                using (StreamReader streamreader = new StreamReader(filename, Encoding.Default))
                {
                    if (OnImportFile != null)
                    {
                        List<Paper> result = OnImportFile(streamreader);
                        if (result == null)
                        {
                            lock (obj)
                            {
                                Filenames.Enqueue(filename);
                            }
                        }
                        else
                        {
                            MemoryDatabase.AddRange(result);
                        }
                        if (NoticeOnImportFile != null)
                        {
                            NoticeOnImportFile(filename, result == null ? false : true);
                        }

                    }
                }
            }
            return true;
        }
        private bool StoreData()
        {
            if (OnStoreToDatabase != null)
            {
                bool result = OnStoreToDatabase(MemoryDatabase.GetPaperList());
                if (NoticeOnStoreToDatabase != null)
                {
                    NoticeOnStoreToDatabase(new List<Paper>(), result);
                }
            }

            return true;
        }
        #endregion

        public ImportFiles(List<string> filenames, int papercount)
        {

        }

    }
}
