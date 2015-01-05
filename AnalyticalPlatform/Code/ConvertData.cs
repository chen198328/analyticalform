using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalPlatform
{
    public class ConvertData
    {
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="filenames">文件名</param>
        /// <returns>数据处理完成情况</returns>
        public delegate void ControlDataHandler(List<string> filenames);
        /// <summary>
        /// 导入数据
        /// </summary>
        public  event ControlDataHandler OnControlData;

        public delegate void CompleteControlDataHandler(AggregateException exception);
        /// <summary>
        /// 所有数据处理完成
        /// </summary>
        public event CompleteControlDataHandler OnComplete;

        private List<string> Filenames { set; get; }
        public int FileCount { set; get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames">所有文件名列表</param>
        /// <param name="filecount">一个任务处理的文件数</param>
        public ConvertData(List<string> filenames, int filecount)
        {
            Filenames = filenames;
            FileCount = filecount;
        }
        public void Start()
        {
            List<List<string>> filenameslist = SplitQueue(Filenames, FileCount);
            if (filenameslist != null)
            {
                Task[] tasklist = new Task[filenameslist.Count];
                for (int i = 0; i < filenameslist.Count; i++)
                {
                    List<string> filenames = filenameslist[i];
                    tasklist[i] = Task.Factory.StartNew(() =>
                    {
                        if (OnControlData != null)
                        {
                            OnControlData(filenames);
                        }
                    });
                }
                AggregateException Error = null;
                try
                {
                    Task.WaitAll(tasklist);
                }
                catch (AggregateException error)
                {
                    Error = error;
                }
                if (OnComplete != null)
                {

                    OnComplete(Error);
                }
            }
        }
        /// <summary>
        /// 生成多个任务
        /// </summary>
        /// <param name="filenames"></param>
        /// <param name="filecount"></param>
        /// <returns></returns>
        public List<List<string>> SplitQueue(List<string> filenames, int filecount)
        {
            if (filenames == null) return null;
            List<List<string>> filenameslist = new List<List<string>>();
            if (filecount == 0)
            {
                filenameslist.Add(filenames);

            }
            else
            {
                for (int i = 0; i < filenames.Count; i += filecount)
                {
                    int temp = filenames.Count - i;
                    if (temp < filecount)
                        filecount = temp;
                    filenameslist.Add(filenames.GetRange(i, filecount));
                }

            }
            return filenameslist;
        }
    }
}
